using DTS.SqlServer.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DTS.Framework.Tests.SqlServer.DataAccess
{
    [TestClass]
    public class ColumnIdentifierTests
    {
        [TestMethod]
        public void Construct_StringName()
        {
            ColumnIdentifier identifier = new ColumnIdentifier("[Name]");

            Assert.AreEqual("", identifier.Schema);
            Assert.AreEqual("", identifier.Object);
            Assert.AreEqual("Name", identifier.Name);
            Assert.AreEqual("", identifier.Alias);
            Assert.AreEqual(ColumnIdentiferType.Name, identifier.ColumnIdentiferType);
        }

        [TestMethod]
        public void Construct_StringNameAndAlias()
        {
            ColumnIdentifier identifier = new ColumnIdentifier("[Name] UserName");

            Assert.AreEqual("", identifier.Schema);
            Assert.AreEqual("", identifier.Object);
            Assert.AreEqual("Name", identifier.Name);
            Assert.AreEqual("UserName", identifier.Alias);
            Assert.AreEqual(ColumnIdentiferType.NameAlias, identifier.ColumnIdentiferType);
        }

        [TestMethod]
        public void Construct_StringObjectAndName()
        {
            ColumnIdentifier identifier = new ColumnIdentifier("[User].[Name]");

            Assert.AreEqual("", identifier.Schema);
            Assert.AreEqual("User", identifier.Object);
            Assert.AreEqual("Name", identifier.Name);
            Assert.AreEqual("", identifier.Alias);
            Assert.AreEqual(ColumnIdentiferType.ObjectName, identifier.ColumnIdentiferType);
        }

        [TestMethod]
        public void Construct_StringObjectNameAndAlias()
        {
            ColumnIdentifier identifier = new ColumnIdentifier("[User].[Name] UserName");

            Assert.AreEqual("", identifier.Schema);
            Assert.AreEqual("User", identifier.Object);
            Assert.AreEqual("Name", identifier.Name);
            Assert.AreEqual("UserName", identifier.Alias);
            Assert.AreEqual(ColumnIdentiferType.ObjectNameAlias, identifier.ColumnIdentiferType);
        }

        [TestMethod]
        public void Construct_StringSchemaObjectAndName()
        {
            ColumnIdentifier identifier = new ColumnIdentifier("[security].[User].Name");

            Assert.AreEqual("security", identifier.Schema);
            Assert.AreEqual("User", identifier.Object);
            Assert.AreEqual("Name", identifier.Name);
            Assert.AreEqual("", identifier.Alias);
            Assert.AreEqual(ColumnIdentiferType.SchemaObjectName, identifier.ColumnIdentiferType);
        }

        [TestMethod]
        public void Construct_StringSchemaObjectNameAndAlias()
        {
            ColumnIdentifier identifier = new ColumnIdentifier("[security].[User].Name UserName");

            Assert.AreEqual("security", identifier.Schema);
            Assert.AreEqual("User", identifier.Object);
            Assert.AreEqual("Name", identifier.Name);
            Assert.AreEqual("UserName", identifier.Alias);
            Assert.AreEqual(ColumnIdentiferType.SchemaObjectNameAlias, identifier.ColumnIdentiferType);
        }
    }
}
