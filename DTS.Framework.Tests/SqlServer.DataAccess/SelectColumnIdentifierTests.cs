using DTS.SqlServer.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DTS.Framework.Tests.SqlServer.DataAccess
{
    [TestClass]
    public class SelectColumnIdentifierTests
    {
        [TestMethod]
        public void Construct_StringName()
        {
            SelectColumnIdentifier identifier = new SelectColumnIdentifier("[Name]");

            Assert.AreEqual("", identifier.Schema);
            Assert.AreEqual("", identifier.Object);
            Assert.AreEqual("Name", identifier.Name);
            Assert.AreEqual("", identifier.Alias);
            Assert.AreEqual(SelectColumnIdentiferType.Name, identifier.IdentiferType);
        }

        [TestMethod]
        public void Construct_StringNameAndAlias()
        {
            SelectColumnIdentifier identifier = new SelectColumnIdentifier("[Name] UserName");

            Assert.AreEqual("", identifier.Schema);
            Assert.AreEqual("", identifier.Object);
            Assert.AreEqual("Name", identifier.Name);
            Assert.AreEqual("UserName", identifier.Alias);
            Assert.AreEqual(SelectColumnIdentiferType.NameAlias, identifier.IdentiferType);
        }

        [TestMethod]
        public void Construct_StringObjectAndName()
        {
            SelectColumnIdentifier identifier = new SelectColumnIdentifier("[User].[Name]");

            Assert.AreEqual("", identifier.Schema);
            Assert.AreEqual("User", identifier.Object);
            Assert.AreEqual("Name", identifier.Name);
            Assert.AreEqual("", identifier.Alias);
            Assert.AreEqual(SelectColumnIdentiferType.ObjectName, identifier.IdentiferType);
        }

        [TestMethod]
        public void Construct_StringObjectNameAndAlias()
        {
            SelectColumnIdentifier identifier = new SelectColumnIdentifier("[User].[Name] UserName");

            Assert.AreEqual("", identifier.Schema);
            Assert.AreEqual("User", identifier.Object);
            Assert.AreEqual("Name", identifier.Name);
            Assert.AreEqual("UserName", identifier.Alias);
            Assert.AreEqual(SelectColumnIdentiferType.ObjectNameAlias, identifier.IdentiferType);
        }

        [TestMethod]
        public void Construct_StringSchemaObjectAndName()
        {
            SelectColumnIdentifier identifier = new SelectColumnIdentifier("[security].[User].Name");

            Assert.AreEqual("security", identifier.Schema);
            Assert.AreEqual("User", identifier.Object);
            Assert.AreEqual("Name", identifier.Name);
            Assert.AreEqual("", identifier.Alias);
            Assert.AreEqual(SelectColumnIdentiferType.SchemaObjectName, identifier.IdentiferType);
        }

        [TestMethod]
        public void Construct_StringSchemaObjectNameAndAlias()
        {
            SelectColumnIdentifier identifier = new SelectColumnIdentifier("[security].[User].Name UserName");

            Assert.AreEqual("security", identifier.Schema);
            Assert.AreEqual("User", identifier.Object);
            Assert.AreEqual("Name", identifier.Name);
            Assert.AreEqual("UserName", identifier.Alias);
            Assert.AreEqual(SelectColumnIdentiferType.SchemaObjectNameAlias, identifier.IdentiferType);
        }
    }
}
