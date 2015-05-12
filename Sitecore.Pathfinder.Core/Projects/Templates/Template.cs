namespace Sitecore.Pathfinder.Projects.Templates
{
  using System;
  using System.Collections.Generic;
  using Sitecore.Pathfinder.Diagnostics;
  using Sitecore.Pathfinder.Projects.Items;
  using Sitecore.Pathfinder.TreeNodes;

  public class Template : ItemBase
  {
    public Template([NotNull] IProject project, [NotNull] ITextSpan textSpan) : base(project, textSpan)
    {
      this.BaseTemplates = string.Empty;
      this.Sections = new List<TemplateSection>();
    }

    [NotNull]
    public string BaseTemplates { get; set; }

    [NotNull]
    public IList<TemplateSection> Sections { get; }

    public override void Analyze()
    {
      base.Analyze();

      if (string.IsNullOrEmpty(this.BaseTemplates))
      {
        return;
      }

      var templates = this.BaseTemplates.Split(Constants.Pipe, StringSplitOptions.RemoveEmptyEntries);
      foreach (var template in templates)
      {
        this.References.AddTemplateReference(template);
      }
    }
  }
}
