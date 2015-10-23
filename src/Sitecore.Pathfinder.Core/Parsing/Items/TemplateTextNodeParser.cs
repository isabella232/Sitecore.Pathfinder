// � 2015 Sitecore Corporation A/S. All rights reserved.

using System;
using System.ComponentModel.Composition;
using System.Linq;
using Sitecore.Pathfinder.Diagnostics;
using Sitecore.Pathfinder.IO;
using Sitecore.Pathfinder.Projects.Templates;
using Sitecore.Pathfinder.Snapshots;
using Sitecore.Pathfinder.Text;

namespace Sitecore.Pathfinder.Parsing.Items
{
    [Export(typeof(ITextNodeParser))]
    public class TemplateTextNodeParser : TextNodeParserBase
    {
        public TemplateTextNodeParser() : base(Constants.TextNodeParsers.Templates)
        {
        }

        public override bool CanParse(ItemParseContext context, ITextNode textNode)
        {
            return textNode.Key == "Template";
        }

        public override void Parse(ItemParseContext context, ITextNode textNode)
        {
            var itemNameTextNode = GetItemNameTextNode(context.ParseContext, textNode);
            var itemIdOrPath = PathHelper.CombineItemPath(context.ParentItemPath, itemNameTextNode.Value);
            var guid = StringHelper.GetGuid(context.ParseContext.Project, textNode.GetAttributeValue("Id", itemIdOrPath));
            var databaseName = textNode.GetAttributeValue("Database", context.DatabaseName);

            var template = context.ParseContext.Factory.Template(context.ParseContext.Project, guid, textNode, databaseName, itemNameTextNode.Value, itemIdOrPath);
            template.ItemNameProperty.AddSourceTextNode(itemNameTextNode);
            template.BaseTemplatesProperty.Parse(textNode, Constants.Templates.StandardTemplate);
            template.IconProperty.Parse(textNode);
            template.ShortHelpProperty.Parse(textNode);
            template.LongHelpProperty.Parse(textNode);
            template.IsEmittable = !string.Equals(textNode.GetAttributeValue("IsEmittable"), "False", StringComparison.OrdinalIgnoreCase);
            template.IsExtern = string.Equals(textNode.GetAttributeValue("IsExternalReference"), "True", StringComparison.OrdinalIgnoreCase);

            template.References.AddRange(context.ParseContext.ReferenceParser.ParseReferences(template, template.BaseTemplatesProperty));

            // create standard values item
            var standardValuesItemIdOrPath = itemIdOrPath + "/__Standard Values";
            var standardValuesGuid = StringHelper.GetGuid(context.ParseContext.Project, standardValuesItemIdOrPath);
            var standardValuesItem = context.ParseContext.Factory.Item(context.ParseContext.Project, standardValuesGuid, textNode, databaseName, "__Standard Values", standardValuesItemIdOrPath, itemIdOrPath);

            // todo: should be Uri
            template.StandardValuesItem = standardValuesItem;

            // parse fields and sections
            var sections = textNode.GetSnapshotFormatSpecificChildNode("Sections");
            if (sections != null)
            {
                foreach (var sectionTreeNode in sections.ChildNodes)
                {
                    ParseSection(context, template, sectionTreeNode);
                }
            }

            // setup HtmlTemplate
            var htmlFileTextNode = textNode.GetAttribute("Layout.HtmlFile");
            if (htmlFileTextNode != null && !string.IsNullOrEmpty(htmlFileTextNode.Value))
            {
                var field = context.ParseContext.Factory.Field(template.StandardValuesItem, htmlFileTextNode);
                field.FieldNameProperty.SetValue("__Renderings");
                field.ValueProperty.SetValue(htmlFileTextNode.Value);
                field.ValueHintProperty.SetValue("HtmlTemplate");
                template.StandardValuesItem.Fields.Add(field);
            }

            context.ParseContext.Project.AddOrMerge(template);
            context.ParseContext.Project.AddOrMerge(standardValuesItem);
        }

        protected virtual void ParseField([NotNull] ItemParseContext context, [NotNull] Template template, [NotNull] TemplateSection templateSection, [NotNull] ITextNode templateFieldTextNode, ref int nextSortOrder)
        {
            var fieldName = templateFieldTextNode.GetAttribute("Name");
            if (fieldName == null)
            {
                context.ParseContext.Trace.TraceError(Texts._Field__element_must_have_a__Name__attribute, templateFieldTextNode.Snapshot.SourceFile.AbsoluteFileName, templateFieldTextNode.TextSpan);
                return;
            }

            var templateField = templateSection.Fields.FirstOrDefault(f => string.Equals(f.FieldName, fieldName.Value, StringComparison.OrdinalIgnoreCase));
            if (templateField == null)
            {
                templateField = context.ParseContext.Factory.TemplateField(template, templateFieldTextNode);
                templateSection.Fields.Add(templateField);
                templateField.FieldNameProperty.SetValue(fieldName);
            }

            templateField.TypeProperty.Parse(templateFieldTextNode, "Single-Line Text");
            templateField.Shared = string.Equals(templateFieldTextNode.GetAttributeValue("Sharing"), "Shared", StringComparison.OrdinalIgnoreCase);
            templateField.Unversioned = string.Equals(templateFieldTextNode.GetAttributeValue("Sharing"), "Unversioned", StringComparison.OrdinalIgnoreCase);
            templateField.SourceProperty.Parse(templateFieldTextNode);
            templateField.ShortHelpProperty.Parse(templateFieldTextNode);
            templateField.LongHelpProperty.Parse(templateFieldTextNode);
            templateField.SortOrderProperty.Parse(templateFieldTextNode, nextSortOrder);

            nextSortOrder = templateField.SortOrder + 100;

            var standardValueTextNode = templateFieldTextNode.GetAttribute("StandardValue");
            if (standardValueTextNode != null && !string.IsNullOrEmpty(standardValueTextNode.Value))
            {
                if (template.StandardValuesItem == null)
                {
                    context.ParseContext.Trace.TraceError(Texts.Template_does_not_a_standard_values_item, standardValueTextNode);
                }
                else
                {
                    // todo: support language and version
                    var field = context.ParseContext.Factory.Field(template.StandardValuesItem, standardValueTextNode);
                    field.FieldNameProperty.SetValue(fieldName);
                    field.ValueProperty.SetValue(standardValueTextNode);

                    template.StandardValuesItem.Fields.Add(field);
                }
            }

              template.References.AddRange(context.ParseContext.ReferenceParser.ParseReferences(template, templateField.SourceProperty));
        }

        protected virtual void ParseSection([NotNull] ItemParseContext context, [NotNull] Template template, [NotNull] ITextNode templateSectionTextNode)
        {
            var sectionName = templateSectionTextNode.GetAttribute("Name");
            if (sectionName == null)
            {
                context.ParseContext.Trace.TraceError(Texts._Section__element_must_have_a__Name__attribute, templateSectionTextNode);
                return;
            }

            var templateSection = template.Sections.FirstOrDefault(s => string.Equals(s.SectionName, sectionName.Value, StringComparison.OrdinalIgnoreCase));
            if (templateSection == null)
            {
                templateSection = context.ParseContext.Factory.TemplateSection(templateSectionTextNode);
                templateSection.SectionNameProperty.SetValue(sectionName);

                template.Sections.Add(templateSection);
            }

            templateSection.IconProperty.Parse(templateSectionTextNode);

            var fieldsTextNode = templateSectionTextNode.GetSnapshotFormatSpecificChildNode("Fields");
            if (fieldsTextNode == null)
            {
                return;
            }

            var nextSortOrder = 0;
            foreach (var fieldTextNode in fieldsTextNode.ChildNodes)
            {
                ParseField(context, template, templateSection, fieldTextNode, ref nextSortOrder);
            }
        }
    }
}
