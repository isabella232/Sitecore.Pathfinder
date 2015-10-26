// � 2015 Sitecore Corporation A/S. All rights reserved.

using System;
using System.Linq;
using Sitecore.Pathfinder.Extensibility.Pipelines;
using Sitecore.Pathfinder.Projects.Items;

namespace Sitecore.Pathfinder.Building.Testing.UnitTests.Pipelines
{
    public class GenerateItemUnitTests : PipelineProcessorBase<GenerateUnitTestsPipeline>
    {
        public GenerateItemUnitTests() : base(4000)
        {
        }

        protected override void Process(GenerateUnitTestsPipeline pipeline)
        {
            foreach (var item in pipeline.Context.Project.Items.OfType<Item>())
            {
                if (!item.IsEmittable || item.IsExtern)
                {
                    continue;
                }

                var testName = pipeline.GetTestName(item.ItemIdOrPath, pipeline.Index, "Item");
                pipeline.Tests.Add(testName);

                pipeline.Stream.WriteLine("        [Test]");
                pipeline.Stream.WriteLine("        public void " + testName + "()");
                pipeline.Stream.WriteLine("        {");
                pipeline.Stream.WriteLine("            var database = Factory.GetDatabase(\"" + item.DatabaseName + "\");");
                pipeline.Stream.WriteLine("            var item = database.GetItem(\"" + item.ItemIdOrPath + "\");");
                pipeline.Stream.WriteLine("            Assert.IsNotNull(item);");
                pipeline.Stream.WriteLine("            Assert.AreEqual(\"" + item.ItemName + "\", item.Name);");

                if (!string.IsNullOrEmpty(item.TemplateIdOrPath))
                {
                    Guid guid;
                    if (Guid.TryParse(item.TemplateIdOrPath, out guid))
                    {
                        pipeline.Stream.WriteLine("            Assert.AreEqual(\"" + item.TemplateIdOrPath + "\", item.TemplateID.ToString());");
                    }
                    else
                    {
                        pipeline.Stream.WriteLine("            Assert.AreEqual(\"" + item.TemplateIdOrPath + "\", database.GetItem(item.TemplateID).Paths.Path);");
                    }
                }

                if (!string.IsNullOrEmpty(item.Icon))
                {
                    pipeline.Stream.WriteLine("            Assert.AreEqual(\"" + item.Icon + "\", item.Appearance.Icon);");
                }

                var sharedFields = item.Fields.Where(f => f.IsTestable && string.IsNullOrEmpty(f.Language) && f.Version == 0).ToList();
                var versionedFields = item.Fields.Where(f => f.IsTestable && (!string.IsNullOrEmpty(f.Language) || f.Version != 0)).ToList();

                foreach (var field in sharedFields)
                {
                    pipeline.Stream.WriteLine("            Assert.AreEqual(\"" + field.Value + "\", item[\"" + field.FieldName + "\"]);");
                }

                if (versionedFields.Any())
                {
                    pipeline.Stream.WriteLine("            var versions = item.Versions.GetVersions(true);");

                    foreach (var field in versionedFields)
                    {
                        pipeline.Stream.Write("            Assert.AreEqual(\"" + field.Value + "\", versions.First(v => ");

                        if (!string.IsNullOrEmpty(field.Language) && field.Version != 0)
                        {
                            pipeline.Stream.Write("v.Language == \"" + field.Language + "\" && v.Version == " + field.Version);
                        }
                        else if (!string.IsNullOrEmpty(field.Language))
                        {
                            pipeline.Stream.Write("v.Language == \"" + field.Language + "\"");
                        }
                        else if (field.Version != 0)
                        {
                            pipeline.Stream.Write("v.Version == " + field.Version);
                        }

                        pipeline.Stream.WriteLine(")[\"" + field.FieldName + "\"]);");
                    }
                }

                pipeline.Stream.WriteLine("        }");
                pipeline.Stream.WriteLine();

                pipeline.Index++;
            }
        }
    }
}