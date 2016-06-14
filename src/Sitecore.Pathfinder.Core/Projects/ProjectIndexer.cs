﻿// © 2016 Sitecore Corporation A/S. All rights reserved.

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Sitecore.Pathfinder.Diagnostics;
using Sitecore.Pathfinder.Extensions;
using Sitecore.Pathfinder.Projects.Items;
using Sitecore.Pathfinder.Projects.References;
using Sitecore.Pathfinder.Projects.Templates;
using Sitecore.Pathfinder.Snapshots;

namespace Sitecore.Pathfinder.Projects
{
    [Export(typeof(IProjectIndexer))]
    public class ProjectIndexer : IProjectIndexer
    {
        [NotNull]
        protected ProjectIndex<DatabaseProjectItem> DatabaseGuidIndex { get; } = new ProjectIndex<DatabaseProjectItem>(item => item.DatabaseName.ToUpperInvariant() + ":" + item.Uri.Guid.Format());

        [NotNull]
        protected ProjectIndex<DatabaseProjectItem> DatabaseQualifiedNameIndex { get; } = new ProjectIndex<DatabaseProjectItem>(item => item.DatabaseName.ToUpperInvariant() + ":" + item.QualifiedName.ToUpperInvariant());

        [NotNull]
        protected ProjectIndex<IProjectItem> GuidIndex { get; } = new ProjectIndex<IProjectItem>(projectItem => projectItem.Uri.Guid.Format());

        public ICollection<Item> Items { get; } = new List<Item>();

        [NotNull]
        protected ProjectIndex<IProjectItem> QualifiedNameIndex { get; } = new ProjectIndex<IProjectItem>(projectItem => projectItem.QualifiedName.ToUpperInvariant());

        [NotNull]
        protected ProjectIndex<IProjectItem> SourceFileIndex { get; } = new ProjectIndex<IProjectItem>();

        public ICollection<Template> Templates { get; } = new List<Template>();

        [NotNull]
        protected ProjectIndex<IProjectItem> UriIndex { get; } = new ProjectIndex<IProjectItem>(projectItem => projectItem.Uri.ToString());

        [NotNull]
        protected ProjectIndex<Item> ChildIndex { get; } = new ProjectIndex<Item>(item => item.DatabaseName.ToUpperInvariant() + ":" + item.ParentItemPath.ToUpperInvariant());

        [NotNull]
        protected ProjectIndex<DatabaseProjectItem> DatabaseShortNameIndex { get; } = new ProjectIndex<DatabaseProjectItem>(item => item.DatabaseName.ToUpperInvariant() + ":" + item.ShortName.ToUpperInvariant());

        public IEnumerable<IReference> FindUsages(string qualifiedName)
        {
            var target = FirstOrDefault<IProjectItem>(qualifiedName);
            if (target == null)
            {
                yield break;
            }

            foreach (var item in Items)
            {
                foreach (var reference in item.References)
                {
                    var i = reference.Resolve();
                    if (i == target)
                    {
                        yield return reference;
                    }
                }
            }

            foreach (var item in Templates)
            {
                foreach (var reference in item.References)
                {
                    var i = reference.Resolve();
                    if (i == target)
                    {
                        yield return reference;
                    }
                }
            }
        }


        public virtual void Add(IProjectItem projectItem)
        {
            lock (this)
            {
                GuidIndex.Add(projectItem);
                QualifiedNameIndex.Add(projectItem);
                UriIndex.Add(projectItem);

                foreach (var snapshot in projectItem.Snapshots)
                {
                    SourceFileIndex.Add(snapshot.SourceFile.GetFileNameWithoutExtensions().ToUpperInvariant(), projectItem);
                }

                var databaseProjectItem = projectItem as DatabaseProjectItem;
                if (databaseProjectItem != null)
                {
                    DatabaseGuidIndex.Add(databaseProjectItem);
                    DatabaseQualifiedNameIndex.Add(databaseProjectItem);
                    DatabaseShortNameIndex.Add(databaseProjectItem);
                }

                var item = projectItem as Item;
                if (item != null && !item.IsImport)
                {
                    Items.Add(item);
                }

                if (item != null)
                {
                    ChildIndex.Add(item);
                }

                var template = projectItem as Template;
                if (template != null && !template.IsImport)
                {
                    Templates.Add(template);
                }
            }
        }

        public virtual T FirstOrDefault<T>(Guid guid) where T : class, IProjectItem
        {
            return GuidIndex.FirstOrDefault<T>(guid.Format());
        }

        public virtual T FirstOrDefault<T>(Database database, Guid guid) where T : DatabaseProjectItem
        {
            return DatabaseGuidIndex.FirstOrDefault<T>(database.DatabaseName.ToUpperInvariant() + ":" + guid.Format());
        }

        public virtual T FirstOrDefault<T>(string qualifiedName) where T : class, IProjectItem
        {
            return QualifiedNameIndex.FirstOrDefault<T>(qualifiedName.ToUpperInvariant());
        }

        public virtual T FirstOrDefault<T>(Database database, string qualifiedName) where T : DatabaseProjectItem
        {
            return DatabaseQualifiedNameIndex.FirstOrDefault<T>(database.DatabaseName.ToUpperInvariant() + ":" + qualifiedName.ToUpperInvariant());
        }

        public virtual T FirstOrDefault<T>(ProjectItemUri uri) where T : class, IProjectItem
        {
            return UriIndex.FirstOrDefault<T>(uri.ToString());
        }

        public IEnumerable<Item> WhereChildOf(Item item)
        {
            return ChildIndex.Where<Item>(item.DatabaseName.ToUpperInvariant() + ":" + item.ItemIdOrPath.ToUpperInvariant());
        }

        public virtual void Remove(IProjectItem projectItem)
        {
            lock (this)
            {
                GuidIndex.Remove(projectItem);
                UriIndex.Remove(projectItem);
                QualifiedNameIndex.Remove(projectItem);

                foreach (var snapshot in projectItem.Snapshots)
                {
                    SourceFileIndex.Remove(snapshot.SourceFile.GetFileNameWithoutExtensions().ToUpperInvariant());
                }

                var databaseProjectItem = projectItem as DatabaseProjectItem;
                if (databaseProjectItem != null)
                {
                    DatabaseGuidIndex.Remove(databaseProjectItem);
                    DatabaseQualifiedNameIndex.Remove(databaseProjectItem);
                    DatabaseShortNameIndex.Remove(databaseProjectItem);
                }

                var item = projectItem as Item;
                if (item != null && !item.IsImport)
                {
                    Items.Remove(item);
                }

                if (item != null)
                {
                    ChildIndex.Remove(item);
                }

                var template = projectItem as Template;
                if (template != null && !template.IsImport)
                {
                    Templates.Remove(template);
                }
            }
        }

        public IEnumerable<T> WhereQualifiedName<T>(Database database, string qualifiedName) where T : DatabaseProjectItem
        {
            return DatabaseQualifiedNameIndex.Where<T>(database.DatabaseName.ToUpperInvariant() + ":" + qualifiedName.ToUpperInvariant());
        }

        public IEnumerable<T> WhereShortName<T>(Database database, string shortName) where T : DatabaseProjectItem
        {
            return DatabaseShortNameIndex.Where<T>(database.DatabaseName.ToUpperInvariant() + ":" + shortName.ToUpperInvariant());
        }

        public virtual IEnumerable<T> Where<T>(ISourceFile sourceFile) where T : class, IProjectItem
        {
            return SourceFileIndex.Where<T>(sourceFile.GetFileNameWithoutExtensions().ToUpperInvariant());
        }
    }
}