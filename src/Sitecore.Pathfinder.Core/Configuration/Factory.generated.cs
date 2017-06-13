//------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by a tool.
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591

namespace Sitecore.Pathfinder.Configuration
{
    #region Designer generated code

    public partial class Factory : IFactory
    {
        public virtual Sitecore.Pathfinder.Languages.BinFiles.BinFile BinFile(Sitecore.Pathfinder.Projects.IProjectBase project, Sitecore.Pathfinder.Snapshots.ISnapshot snapshot, string filePath) => new Sitecore.Pathfinder.Languages.BinFiles.BinFile(project, snapshot, filePath);

        public virtual Sitecore.Pathfinder.Tasks.Building.IBuildContext BuildContext() => new Sitecore.Pathfinder.Tasks.Building.BuildContext(Configuration, Console, Trace);

        public virtual Sitecore.Pathfinder.Checking.ICheckerContext CheckerContext(Sitecore.Pathfinder.Projects.IProjectBase project) => Resolve<Sitecore.Pathfinder.Checking.ICheckerContext>().With(project);

        public virtual Sitecore.Pathfinder.Compiling.Compilers.ICompileContext CompileContext(Sitecore.Pathfinder.Projects.IProject project) => Resolve<Sitecore.Pathfinder.Compiling.Compilers.ICompileContext>().With(project);

        public virtual Sitecore.Pathfinder.Languages.ConfigFiles.ConfigFile ConfigFile(Sitecore.Pathfinder.Projects.IProjectBase project, Sitecore.Pathfinder.Snapshots.ISnapshot snapshot, string filePath) => new Sitecore.Pathfinder.Languages.ConfigFiles.ConfigFile(project, snapshot, filePath);

        public virtual Sitecore.Pathfinder.Languages.Content.ContentFile ContentFile(Sitecore.Pathfinder.Projects.IProjectBase project, Sitecore.Pathfinder.Snapshots.ISnapshot snapshot, string filePath) => new Sitecore.Pathfinder.Languages.Content.ContentFile(project, snapshot, filePath);

        public virtual Sitecore.Pathfinder.Projects.Database Database(Sitecore.Pathfinder.Projects.IProjectBase project, string databaseName, System.Collections.Generic.IEnumerable<string> languageNames) => new Sitecore.Pathfinder.Projects.Database(this, project, databaseName, languageNames);

        public virtual Sitecore.Pathfinder.Projects.References.DeviceReference DeviceReference(Sitecore.Pathfinder.Projects.IProjectItem owner, Sitecore.Pathfinder.Projects.SourceProperty<string> sourceProperty, string referenceText, string databaseName) => new Sitecore.Pathfinder.Projects.References.DeviceReference(owner, sourceProperty, referenceText, databaseName);

        public virtual Sitecore.Pathfinder.Projects.Diagnostic Diagnostic(int msg, string fileName, Sitecore.Pathfinder.Snapshots.TextSpan span, Sitecore.Pathfinder.Diagnostics.Severity severity, string text) => new Sitecore.Pathfinder.Projects.Diagnostic(msg, fileName, span, severity, text);

        public virtual Sitecore.Pathfinder.ProjectTrees.DirectoryProjectTreeItem DirectoryProjectTreeItem(Sitecore.Pathfinder.ProjectTrees.IProjectTree projectTree, string directory) => new Sitecore.Pathfinder.ProjectTrees.DirectoryProjectTreeItem(projectTree, directory);

        public virtual Sitecore.Pathfinder.Emitting.IEmitContext EmitContext(Sitecore.Pathfinder.Emitting.IProjectEmitter projectEmitter, Sitecore.Pathfinder.Projects.IProjectBase project) => Resolve<Sitecore.Pathfinder.Emitting.IEmitContext>().With(projectEmitter, project);

        public virtual Sitecore.Pathfinder.Projects.Items.Field Field(Sitecore.Pathfinder.Projects.Items.Item item) => new Sitecore.Pathfinder.Projects.Items.Field(item);

        public virtual Sitecore.Pathfinder.Compiling.FieldCompilers.IFieldCompileContext FieldCompileContext() => Resolve<Sitecore.Pathfinder.Compiling.FieldCompilers.IFieldCompileContext>();

        public virtual Sitecore.Pathfinder.Projects.Files.File File(Sitecore.Pathfinder.Projects.IProjectBase project, Sitecore.Pathfinder.Snapshots.ISnapshot snapshot, string filePath) => new Sitecore.Pathfinder.Projects.Files.File(project, snapshot, filePath);

        public virtual Sitecore.Pathfinder.ProjectTrees.FileProjectTreeItem FileProjectTreeItem(Sitecore.Pathfinder.ProjectTrees.IProjectTree projectTree, string fileName) => new Sitecore.Pathfinder.ProjectTrees.FileProjectTreeItem(projectTree, fileName);

        public virtual Sitecore.Pathfinder.Projects.References.FileReference FileReference(Sitecore.Pathfinder.Projects.IProjectItem owner, Sitecore.Pathfinder.Projects.SourceProperty<string> sourceProperty, string referenceText) => new Sitecore.Pathfinder.Projects.References.FileReference(owner, sourceProperty, referenceText);

        public virtual Sitecore.Pathfinder.Projects.References.FileReference FileReference(Sitecore.Pathfinder.Projects.IProjectItem owner, Sitecore.Pathfinder.Snapshots.ITextNode textNode, string referenceText) => new Sitecore.Pathfinder.Projects.References.FileReference(owner, textNode, referenceText);

        public virtual Sitecore.Pathfinder.Projects.Items.Item Item(Sitecore.Pathfinder.Projects.Database database, System.Guid guid, string itemName, string itemIdOrPath, string templateIdOrPath) => new Sitecore.Pathfinder.Projects.Items.Item(database, guid, itemName, itemIdOrPath, templateIdOrPath);

        public virtual Sitecore.Pathfinder.Parsing.Items.ItemParseContext ItemParseContext(Sitecore.Pathfinder.Parsing.IParseContext parseContext, Sitecore.Pathfinder.Parsing.Items.ItemParser parser, Sitecore.Pathfinder.Projects.Database database, string parentItemPath, bool isImport) => new Sitecore.Pathfinder.Parsing.Items.ItemParseContext(parseContext, parser, database, parentItemPath, isImport);

        public virtual Sitecore.Pathfinder.Languages.Json.JsonTextSnapshot JsonTextSnapshot(Sitecore.Pathfinder.Snapshots.ISourceFile sourceFile, string contents) => Resolve<Sitecore.Pathfinder.Languages.Json.JsonTextSnapshot>().With(sourceFile, contents);

        public virtual Sitecore.Pathfinder.Projects.Items.Language Language(string languageName) => new Sitecore.Pathfinder.Projects.Items.Language(languageName);

        public virtual Sitecore.Pathfinder.Compiling.FieldCompilers.LayoutCompileContext LayoutCompileContext(Sitecore.Pathfinder.Projects.IProjectBase project, Sitecore.Pathfinder.Projects.Database database, Sitecore.Pathfinder.Snapshots.ITextSnapshot snapshot) => new Sitecore.Pathfinder.Compiling.FieldCompilers.LayoutCompileContext(project, database, snapshot);

        public virtual Sitecore.Pathfinder.Compiling.FieldCompilers.LayoutCompiler LayoutCompiler() => new Sitecore.Pathfinder.Compiling.FieldCompilers.LayoutCompiler(Trace, FileSystem);

        public virtual Sitecore.Pathfinder.Projects.References.LayoutReference LayoutReference(Sitecore.Pathfinder.Projects.IProjectItem owner, Sitecore.Pathfinder.Projects.SourceProperty<string> sourceProperty, string referenceText, string databaseName) => new Sitecore.Pathfinder.Projects.References.LayoutReference(owner, sourceProperty, referenceText, databaseName);

        public virtual Sitecore.Pathfinder.Projects.References.LayoutRenderingReference LayoutRenderingReference(Sitecore.Pathfinder.Projects.IProjectItem owner, Sitecore.Pathfinder.Projects.SourceProperty<string> sourceProperty, string referenceText, string databaseName) => new Sitecore.Pathfinder.Projects.References.LayoutRenderingReference(owner, sourceProperty, referenceText, databaseName);

        public virtual Sitecore.Pathfinder.Languages.Media.MediaFile MediaFile(Sitecore.Pathfinder.Projects.Database database, Sitecore.Pathfinder.Snapshots.ISnapshot snapshot, string itemName, string itemPath, string filePath) => new Sitecore.Pathfinder.Languages.Media.MediaFile(database, snapshot, itemName, itemPath, filePath);

        public virtual Sitecore.Pathfinder.Tasks.Building.OutputFile OutputFile(string fileName) => new Sitecore.Pathfinder.Tasks.Building.OutputFile(fileName);

        public virtual Sitecore.Pathfinder.Parsing.IParseContext ParseContext(Sitecore.Pathfinder.Projects.IProject project, Sitecore.Pathfinder.Snapshots.ISnapshot snapshot, Sitecore.Pathfinder.Parsing.PathMappingContext pathMappingContext) => new Sitecore.Pathfinder.Parsing.ParseContext(Configuration, project, snapshot, pathMappingContext);

        public virtual Sitecore.Pathfinder.Parsing.PathMappingContext PathMappingContext(Sitecore.Pathfinder.IO.IPathMapperService pathMapper) => new Sitecore.Pathfinder.Parsing.PathMappingContext(pathMapper);

        public virtual Sitecore.Pathfinder.IO.PathMatcher PathMatcher(string include, string exclude) => new Sitecore.Pathfinder.IO.PathMatcher(include, exclude);

        public virtual Sitecore.Pathfinder.Projects.IProject Project(Sitecore.Pathfinder.Projects.ProjectOptions projectOptions, System.Collections.Generic.IEnumerable<string> sourceFileNames) => Resolve<Sitecore.Pathfinder.Projects.IProject>().With(projectOptions, sourceFileNames);

        public virtual Sitecore.Pathfinder.Projects.ProjectContext ProjectContext() => new Sitecore.Pathfinder.Projects.ProjectContext();

        public virtual Sitecore.Pathfinder.Projects.ProjectImportsService ProjectImportsService() => Resolve<Sitecore.Pathfinder.Projects.ProjectImportsService>();

        public virtual Sitecore.Pathfinder.Projects.ProjectIndexes.ProjectIndexes ProjectIndexes(Sitecore.Pathfinder.Projects.IProjectBase project) => new Sitecore.Pathfinder.Projects.ProjectIndexes.ProjectIndexes(project);

        public virtual Sitecore.Pathfinder.Projects.ProjectOptions ProjectOptions(string databaseName) => new Sitecore.Pathfinder.Projects.ProjectOptions(databaseName);

        public virtual Sitecore.Pathfinder.Projects.IProjectService ProjectService() => Resolve<Sitecore.Pathfinder.Projects.IProjectService>();

        public virtual Sitecore.Pathfinder.ProjectTrees.IProjectTree ProjectTree(string toolsDirectory, string projectDirectory) => Resolve<Sitecore.Pathfinder.ProjectTrees.IProjectTree>().With(toolsDirectory, projectDirectory);

        public virtual Sitecore.Pathfinder.ProjectTrees.ProjectTreeVisitor ProjectTreeVisitor() => new Sitecore.Pathfinder.ProjectTrees.ProjectTreeVisitor();

        public virtual Sitecore.Pathfinder.Projects.References.Reference Reference(Sitecore.Pathfinder.Projects.IProjectItem owner, Sitecore.Pathfinder.Projects.SourceProperty<string> sourceProperty, string referenceText, string databaseName) => new Sitecore.Pathfinder.Projects.References.Reference(owner, sourceProperty, referenceText, databaseName);

        public virtual Sitecore.Pathfinder.Projects.References.Reference Reference(Sitecore.Pathfinder.Projects.IProjectItem owner, Sitecore.Pathfinder.Snapshots.ITextNode textNode, string referenceText, string databaseName) => new Sitecore.Pathfinder.Projects.References.Reference(owner, textNode, referenceText, databaseName);

        public virtual Sitecore.Pathfinder.Languages.Renderings.Rendering Rendering(Sitecore.Pathfinder.Projects.Database database, Sitecore.Pathfinder.Snapshots.ISnapshot snapshot, string itemPath, string itemName, string filePath, string templateIdOrPath) => new Sitecore.Pathfinder.Languages.Renderings.Rendering(database, snapshot, itemPath, itemName, filePath, templateIdOrPath);

        public virtual Sitecore.Pathfinder.Languages.Serialization.SerializationFile SerializationFile(Sitecore.Pathfinder.Projects.IProjectBase project, Sitecore.Pathfinder.Snapshots.ISnapshot snapshot, string filePath) => new Sitecore.Pathfinder.Languages.Serialization.SerializationFile(project, snapshot, filePath);

        public virtual Sitecore.Pathfinder.Snapshots.ISnapshot SerializationTextSnapshot(Sitecore.Pathfinder.Snapshots.ISourceFile sourceFile) => Resolve<Sitecore.Pathfinder.Languages.Serialization.SerializationTextSnapshot>().With(sourceFile);

        public virtual Sitecore.Pathfinder.Snapshots.ISnapshot Snapshot(Sitecore.Pathfinder.Snapshots.ISourceFile sourceFile) => Resolve<Sitecore.Pathfinder.Snapshots.Snapshot>().With(sourceFile);

        public virtual Sitecore.Pathfinder.Snapshots.SnapshotParseContext SnapshotParseContext(Sitecore.Pathfinder.Projects.IProjectBase project, System.Collections.Generic.IDictionary<string, string> tokens) => new Sitecore.Pathfinder.Snapshots.SnapshotParseContext(project, tokens);

        public virtual Sitecore.Pathfinder.Snapshots.SnapshotTextNode SnapshotTextNode(Sitecore.Pathfinder.Snapshots.ISnapshot snapshot) => new Sitecore.Pathfinder.Snapshots.SnapshotTextNode(snapshot);

        public virtual Sitecore.Pathfinder.Snapshots.SourceFile SourceFile(string absoluteFileName) => new Sitecore.Pathfinder.Snapshots.SourceFile(Configuration, FileSystem, absoluteFileName);

        public virtual Sitecore.Pathfinder.Projects.Templates.Template Template(Sitecore.Pathfinder.Projects.Database database, System.Guid guid, string itemName, string itemIdOrPath) => new Sitecore.Pathfinder.Projects.Templates.Template(database, guid, itemName, itemIdOrPath);

        public virtual Sitecore.Pathfinder.Projects.Templates.TemplateField TemplateField(Sitecore.Pathfinder.Projects.Templates.Template template, System.Guid guid) => new Sitecore.Pathfinder.Projects.Templates.TemplateField(template, guid);

        public virtual Sitecore.Pathfinder.Projects.Templates.TemplateSection TemplateSection(Sitecore.Pathfinder.Projects.Templates.Template template, System.Guid guid) => new Sitecore.Pathfinder.Projects.Templates.TemplateSection(template, guid);

        public virtual Sitecore.Pathfinder.Snapshots.TextNode TextNode(Sitecore.Pathfinder.Snapshots.ISnapshot snapshot, string key, string value, Sitecore.Pathfinder.Snapshots.TextSpan textSpan) => new Sitecore.Pathfinder.Snapshots.TextNode(snapshot, key, value, textSpan);

        public virtual Sitecore.Pathfinder.Projects.Items.Version Version(int number) => new Sitecore.Pathfinder.Projects.Items.Version(number);

        public virtual Sitecore.Pathfinder.Languages.Xml.XmlTextSnapshot XmlTextSnapshot(Sitecore.Pathfinder.Snapshots.ISourceFile sourceFile, string contents, string schemaNamespace, string schemaFileName) => Resolve<Sitecore.Pathfinder.Languages.Xml.XmlTextSnapshot>().With(sourceFile, contents, schemaNamespace, schemaFileName);

        public virtual Sitecore.Pathfinder.Languages.Yaml.YamlTextSnapshot YamlTextSnapshot(Sitecore.Pathfinder.Snapshots.ISourceFile sourceFile, string contents) => Resolve<Sitecore.Pathfinder.Languages.Yaml.YamlTextSnapshot>().With(sourceFile, contents);

    }

    #endregion
}

#pragma warning restore 1591