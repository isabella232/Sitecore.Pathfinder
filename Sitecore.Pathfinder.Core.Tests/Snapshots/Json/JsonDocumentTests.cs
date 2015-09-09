﻿// © 2015 Sitecore Corporation A/S. All rights reserved.

using System.Linq;
using NUnit.Framework;

namespace Sitecore.Pathfinder.Snapshots.Json
{
    [TestFixture]
    public class JsonDocumentTests : Tests
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            Start();
        }

        [Test]
        public void GetNestedTextNodeTests()
        {
            var sourceFile = new SourceFile(Services.FileSystem, "test.txt", "test.txt");

            var doc = new JsonTextSnapshot(sourceFile, "{ \"Item\": { \"Fields\": [ { \"Name\": \"Text\", \"Value\": \"123\" } ] } }");
            var root = doc.Root;

            var fields = doc.GetJsonChildTextNode(root, "Fields");
            Assert.IsNotNull(fields);

            var field = fields.ChildNodes.FirstOrDefault();
            Assert.IsNotNull(field);
            Assert.AreEqual("Text", field.GetAttributeValue("Name"));
        }

        [Test]
        public void InvalidJsonTests()
        {
            var sourceFile = new SourceFile(Services.FileSystem, "test.txt", "test.txt");

            var doc = new JsonTextSnapshot(sourceFile, "\"Item\": { }");
            Assert.AreEqual(TextNode.Empty, doc.Root);

            doc = new JsonTextSnapshot(sourceFile, string.Empty);
            Assert.AreEqual(TextNode.Empty, doc.Root);
        }

        [Test]
        public void ItemTests()
        {
            var sourceFile = new SourceFile(Services.FileSystem, "test.txt", "test.txt");

            var doc = new JsonTextSnapshot(sourceFile, "{ \"Item\": { \"Fields\": [ { \"Name\": \"Text\", \"Value\": \"123\" } ] } }");
            var root = doc.Root;
            Assert.IsNotNull(root);
            Assert.AreEqual("Item", root.Name);
            Assert.AreEqual(1, root.ChildNodes.Count());

            var fields = root.ChildNodes.First();
            Assert.AreEqual(1, fields.ChildNodes.Count());

            var field = fields.ChildNodes.First();
            Assert.AreEqual("Text", field.GetAttributeValue("Name"));
            Assert.AreEqual("123", field.GetAttributeValue("Value"));
            Assert.AreEqual(0, field.ChildNodes.Count());

            var attribute = field.GetAttributeTextNode("Name");
            Assert.IsNotNull(attribute);
            Assert.AreEqual("Text", attribute.Value);
            Assert.AreEqual(0, attribute.Attributes.Count());
            Assert.AreEqual(0, attribute.ChildNodes.Count());
            Assert.AreEqual(field, attribute.Parent);
            Assert.AreEqual(field.Snapshot, attribute.Snapshot);
            Assert.AreEqual(doc, attribute.Snapshot);
        }
    }
}