﻿// © 2015 Sitecore Corporation A/S. All rights reserved.

using System.Collections.Generic;
using Sitecore.Pathfinder.Diagnostics;
using Sitecore.Pathfinder.Extensions;

namespace Sitecore.Pathfinder.Snapshots
{
    public class AttributeNameTextNode : ITextNode, IMutableTextNode
    {
        [NotNull]
        private string _key;

        public AttributeNameTextNode([NotNull] ITextNode textNode)
        {
            TextNode = textNode;
            _key = textNode.Key.UnescapeXmlElementName();
        }

        public IEnumerable<ITextNode> Attributes => TextNode.Attributes;

        public IEnumerable<ITextNode> ChildNodes => TextNode.ChildNodes;

        public string Key => _key;

        public ISnapshot Snapshot => TextNode.Snapshot;

        public TextSpan TextSpan => TextNode.TextSpan;

        public string Value => _key;

        [NotNull]
        protected ITextNode TextNode { get; }

        IList<ITextNode> IMutableTextNode.AttributeList => (IList<ITextNode>)TextNode.Attributes;

        IList<ITextNode> IMutableTextNode.ChildNodeCollection => (IList<ITextNode>)TextNode.ChildNodes;

        public ITextNode GetAttribute(string attributeName)
        {
            return TextNode.GetAttribute(attributeName);
        }

        public string GetAttributeValue(string attributeName, string defaultValue = "")
        {
            return TextNode.GetAttributeValue(attributeName, defaultValue);
        }

        public ITextNode GetSnapshotFormatSpecificChildNode(string name)
        {
            return null;
        }

        public ITextNode GetInnerTextNode()
        {
            return null;
        }

        bool IMutableTextNode.SetKey(string newKey)
        {
            _key = newKey.UnescapeXmlElementName();
            return ((IMutableTextNode)TextNode).SetKey(newKey);
        }

        bool IMutableTextNode.SetValue(string newValue)
        {
            _key = newValue.UnescapeXmlElementName();
            return ((IMutableTextNode)TextNode).SetKey(newValue);
        }
    }
}
