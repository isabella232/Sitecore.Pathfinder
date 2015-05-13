﻿namespace Sitecore.Pathfinder.Projects
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.Composition;
  using System.IO;
  using System.Linq;
  using Microsoft.Framework.ConfigurationModel;
  using Sitecore.Pathfinder.Diagnostics;
  using Sitecore.Pathfinder.Documents;
  using Sitecore.Pathfinder.Extensions.ConfigurationExtensions;
  using Sitecore.Pathfinder.IO;
  using Sitecore.Pathfinder.Parsing;
  using Sitecore.Pathfinder.Projects.Items;

  [Export(typeof(IProjectService))]
  public class ProjectService : IProjectService
  {
    [ImportingConstructor]
    public ProjectService([NotNull] ICompositionService compositionService, [NotNull] IConfiguration configuration, [NotNull] ITraceService trace, [NotNull] IFileSystemService fileSystem, [NotNull] IParseService parseService)
    {
      this.CompositionService = compositionService;
      this.Configuration = configuration;
      this.Trace = trace;
      this.FileSystem = fileSystem;
      this.ParseService = parseService;
    }

    [NotNull]
    protected ICompositionService CompositionService { get; }

    [NotNull]
    protected IConfiguration Configuration { get; }

    [NotNull]
    protected IFileSystemService FileSystem { get; }

    [NotNull]
    protected IParseService ParseService { get; }

    [NotNull]
    protected ITraceService Trace { get; set; }

    public IProject LoadProject()
    {
      // todo: refactor this
      var projectDirectory = PathHelper.Combine(this.Configuration.GetString(Pathfinder.Constants.SolutionDirectory), this.Configuration.GetString(Pathfinder.Constants.ProjectDirectory));
      var databaseName = this.Configuration.GetString(Pathfinder.Constants.Database);

      var ignoreFileNames = this.Configuration.GetList(Pathfinder.Constants.IgnoreFileNames).ToList();
      var ignoreDirectories = this.Configuration.GetList(Pathfinder.Constants.IgnoreDirectories).ToList();
      ignoreDirectories.Add(Path.GetFileName(this.Configuration.GetString(Pathfinder.Constants.ToolsDirectory)));

      // todo: consider caching project on disk
      var project = new Project(this.CompositionService, this.Trace, this.FileSystem, this.ParseService).Load(projectDirectory, databaseName);

      this.LoadExternalReferences(project);
      this.LoadProjectItems(project, ignoreDirectories, ignoreFileNames);

      return project;
    }

    protected virtual void LoadExternalReferences([NotNull] Project project)
    {
      foreach (var pair in this.Configuration.GetSubKeys("external-references"))
      {
        var external = new ExternalReferenceItem(project, TreeNode.Empty);
        project.Items.Add(external);
        external.ItemIdOrPath = pair.Key;
        external.ItemName = Path.GetFileName(pair.Key) ?? string.Empty;

        var value = this.Configuration.Get("external-references:" + pair.Key);
        if (string.IsNullOrEmpty(value))
        {
          continue;
        }

        external = new ExternalReferenceItem(project, TreeNode.Empty);
        project.Items.Add(external);
        external.ItemIdOrPath = value;
        external.ItemName = Path.GetFileName(value);
      }
    }

    protected virtual void LoadProjectItems([NotNull] Project project, [NotNull] IEnumerable<string> ignoreDirectories, [NotNull] IEnumerable<string> ignoreFileNames)
    {
      var visitor = new ProjectDirectoryVisitor(this.FileSystem).Load(ignoreDirectories, ignoreFileNames);
      visitor.Visit(project);
    }
  }
}