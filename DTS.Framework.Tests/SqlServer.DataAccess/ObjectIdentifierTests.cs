using DTS.SqlServer.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DTS.Framework.Tests.SqlServer.DataAccess
{
    [TestClass]
    public class ObjectIdentifierTests
    {
        [TestMethod]
        public void Construct_StringName()
        {
            ObjectIdentifier identifier = new ObjectIdentifier("[User]");

            Assert.AreEqual("", identifier.Schema);
            Assert.AreEqual("User", identifier.Name);
            Assert.AreEqual("", identifier.Alias);
            Assert.AreEqual(ObjectIdentiferType.Name, identifier.ObjectIdentiferType);
        }

        [TestMethod]
        public void Construct_StringNameAndAlias()
        {
            ObjectIdentifier identifier = new ObjectIdentifier("[User] u");

            Assert.AreEqual("", identifier.Schema);
            Assert.AreEqual("User", identifier.Name);
            Assert.AreEqual("u", identifier.Alias);
            Assert.AreEqual(ObjectIdentiferType.NameAlias, identifier.ObjectIdentiferType);
        }

        [TestMethod]
        public void Construct_StringSchemaNameAndAlias()
        {
            ObjectIdentifier identifier = new ObjectIdentifier("[security].[User] u");

            Assert.AreEqual("security", identifier.Schema);
            Assert.AreEqual("User", identifier.Name);
            Assert.AreEqual("u", identifier.Alias);
            Assert.AreEqual(ObjectIdentiferType.SchemaNameAlias, identifier.ObjectIdentiferType);
        }

        [TestMethod]
        public void Construct_StringSchemaAndName()
        {
            ObjectIdentifier identifier = new ObjectIdentifier("[security].[User]");

            Assert.AreEqual("security", identifier.Schema);
            Assert.AreEqual("User", identifier.Name);
            Assert.AreEqual("", identifier.Alias);
            Assert.AreEqual(ObjectIdentiferType.SchemaName, identifier.ObjectIdentiferType);
        }
    }
}
