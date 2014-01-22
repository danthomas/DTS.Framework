using DTS.SqlServer.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DTS.Framework.Tests.SqlServer.DataAccess
{
    [TestClass]
    public partial class QueryTests
    {
        [TestMethod]
        public void SingleTable_AllColumns()
        {
            Query query = new Query(_databaseDef)
                .From("TaskType")
                .SelectAll();

            const string expected = @"
SELECT    tt.TaskTypeId
        , tt.TaskTypeName
FROM      testing.TaskType tt";
            string actual = query.GetSelectStatement();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SingleTable_SpecificColumns()
        {
            Query query = new Query(_databaseDef)
                .From("Task")
                .Select("Description")
                .Select("IsCompleted");

            const string expected = @"
SELECT    t.Description
        , t.IsCompleted
FROM      testing.Task t";
            string actual = query.GetSelectStatement();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SingleTable_SpecificColumnsWithAliases()
        {
            Query query = new Query(_databaseDef)
                .From("Task")
                .Select("Description Desc")
                .Select("IsCompleted Completed");

            const string expected = @"
SELECT    t.Description [Desc]
        , t.IsCompleted Completed
FROM      testing.Task t";
            string actual = query.GetSelectStatement();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SingleJoin_AllColumns()
        {
            Query query = new Query(_databaseDef)
                .From("Task")
                .Join("TaskType")
                .SelectAll();

            const string expected = @"
SELECT    t.TaskId
        , t.CreatorUserId
        , t.OwnerUserId
        , t.DeveloperUserId
        , t.TesterUserId
        , t.Description
        , t.IsCompleted
        , t.DateTimeCreated
        , t.DateTimeCompleted
        , tt.TaskTypeId
        , tt.TaskTypeName
FROM      testing.Task t
JOIN      testing.TaskType tt ON t.TaskTypeId = tt.TaskTypeId";
            string actual = query.GetSelectStatement();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MultipleJoin_AllColumns()
        {
            Query query = new Query(_databaseDef)
                .From("Task")
                .Join("TaskType")
                .Join("User", "CreatorUserId")
                .Join("User", "OwnerUserId")
                .SelectAll();

            const string expected = @"
SELECT    t.TaskId
        , t.DeveloperUserId
        , t.TesterUserId
        , t.Description
        , t.IsCompleted
        , t.DateTimeCreated
        , t.DateTimeCompleted
        , tt.TaskTypeId
        , tt.TaskTypeName
        , u1.UserId           CreatorUserId
        , u1.UserName         CreatorUserName
        , u2.UserId           OwnerUserId
        , u2.UserName         OwnerUserName
FROM      testing.Task t
JOIN      testing.TaskType tt ON t.TaskTypeId = tt.TaskTypeId
JOIN      testing.[User] u1 ON t.CreatorUserId = u1.UserId
LEFT JOIN testing.[User] u2 ON t.OwnerUserId = u2.UserId";
            string actual = query.GetSelectStatement();

            Assert.AreEqual(expected, actual);
        }

    }
}
