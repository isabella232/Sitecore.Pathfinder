﻿// © 2015-2017 Sitecore Corporation A/S. All rights reserved.

using System;
using System.Collections.Generic;
using System.Composition;
using System.IO;
using Sitecore.Pathfinder.Configuration.ConfigurationModel;
using Sitecore.Pathfinder.Diagnostics;
using Sitecore.Pathfinder.Extensibility;
using Sitecore.Pathfinder.Extensions;
using Sitecore.Pathfinder.IO;
using Sitecore.Pathfinder.Languages.Json;
using Sitecore.Pathfinder.Languages.Serialization;
using Sitecore.Pathfinder.Languages.Yaml;
using Sitecore.Pathfinder.Projects;
using Sitecore.Pathfinder.Projects.Items;

namespace Sitecore.Pathfinder.Emitting.Emitters.DirectoryEmitter
{
    [Export(typeof(IProjectEmitter)), Shared]
    public class DirectoryProjectEmitter : ProjectEmitterBase
    {
        [ImportingConstructor]
        public DirectoryProjectEmitter([NotNull] IConfiguration configuration, [NotNull] ICompositionService compositionService, [NotNull] ITraceService traceService, [ItemNotNull, NotNull, ImportMany] IEnumerable<IEmitter> emitters, [NotNull] IFileSystemService fileSystem) : base(configuration, compositionService, traceService, emitters)
        {
            FileSystem = fileSystem;
        }

        [NotNull]
        public IFileSystemService FileSystem { get; }

        public override bool CanEmit(string format)
        {
            return string.Equals(format, "directory", StringComparison.OrdinalIgnoreCase);
        }

        public override void Emit(IEmitContext context, IProject project)
        {
            var outputDirectory = PathHelper.Combine(context.Configuration.GetProjectDirectory(), context.Configuration.GetString(Constants.Configuration.Output.Directory));
            FileSystem.DeleteDirectory(outputDirectory);

            base.Emit(context, project);
        }

        public void EmitFile([NotNull] IEmitContext context, [NotNull] string sourceFileAbsoluteFileName, [NotNull] string filePath)
        {
            var fileName = PathHelper.NormalizeFilePath(filePath);
            if (fileName.StartsWith("~\\"))
            {
                fileName = fileName.Mid(2);
            }

            context.Trace.TraceInformation(Msg.I1011, "Publishing", "~\\" + fileName);

            var forceUpdate = Configuration.GetBool(Constants.Configuration.BuildProject.ForceUpdate, true);
            var outputDirectory = PathHelper.Combine(context.Configuration.GetProjectDirectory(), context.Configuration.GetString(Constants.Configuration.Output.Directory));
            var destinationFileName = PathHelper.Combine(outputDirectory, fileName);

            FileSystem.CreateDirectoryFromFileName(destinationFileName);
            FileSystem.Copy(sourceFileAbsoluteFileName, destinationFileName, forceUpdate);
        }

        public void EmitItem([NotNull] IEmitContext context, [NotNull] Item item)
        {
            context.Trace.TraceInformation(Msg.I1011, "Publishing", item.ItemIdOrPath);

            var outputDirectory = PathHelper.Combine(context.Configuration.GetProjectDirectory(), context.Configuration.GetString(Constants.Configuration.Output.Directory));
            var destinationFileName = PathHelper.Combine(outputDirectory, PathHelper.NormalizeFilePath(item.ItemIdOrPath).TrimStart('\\'));

            switch (context.ItemFormat.ToLowerInvariant())
            {
                case "json":
                    destinationFileName += ".content.json";
                    break;

                case "serialization":
                    destinationFileName += ".item";
                    break;

                default:
                    destinationFileName += ".content.yaml";
                    break;
            }

            FileSystem.CreateDirectoryFromFileName(destinationFileName);

            using (var stream = new FileStream(destinationFileName, FileMode.Create))
            {
                using (var writer = new StreamWriter(stream))
                {
                    switch (context.ItemFormat.ToLowerInvariant())
                    {
                        case "json":
                            item.WriteAsContentJson(writer);
                            break;

                        case "serialization":
                            item.WriteAsSerialization(writer, WriteAsSerializationOptions.WriteCompiledFieldValues);
                            break;

                        default:
                            item.WriteAsContentYaml(writer);
                            break;
                    }
                }
            }
        }
    }
}
