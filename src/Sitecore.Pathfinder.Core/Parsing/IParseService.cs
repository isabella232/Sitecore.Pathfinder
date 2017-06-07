﻿// © 2015-2017 Sitecore Corporation A/S. All rights reserved.

using Sitecore.Pathfinder.Diagnostics;
using Sitecore.Pathfinder.Projects;
using Sitecore.Pathfinder.Snapshots;

namespace Sitecore.Pathfinder.Parsing
{
    public interface IParseService
    {
        void Parse([NotNull] IProject project, [NotNull] ISourceFile sourceFile);
    }
}
