using DTS.SqlServer.DataAccess;
using DTS.SqlServer.DataAccess.Definition;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DTS.Framework.Tests.SqlServer.DataAccess
{
    [TestClass]
    public partial class QueryTests
    {
        [TestMethod]
        public void SingleTable_AllColumns()
        {
            Query query = _databaseDef.CreateQuery()
                  .From("TaskType");

            const string expected = @"
SELECT    tt.TaskTypeId
        , tt.TaskTypeName
FROM      static.TaskType tt";
            string actual = query.SelectStatement;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SingleTable_SpecificColumns()
        {
            Query query = _databaseDef.CreateQuery()
                .From("Task")
                .Select("Description")
                .Select("IsCompleted");

            const string expected = @"
SELECT    t.Description
        , t.IsCompleted
FROM      tasks.Task t";
            string actual = query.SelectStatement;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SingleTable_SpecificColumnsWithAliases()
        {
            Query query = _databaseDef.CreateQuery()
                .From("Task")
                .Select("Description Desc")
                .Select("IsCompleted Completed");

            const string expected = @"
SELECT    t.Description [Desc]
        , t.IsCompleted Completed
FROM      tasks.Task t";
            string actual = query.SelectStatement;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SingleJoin_AllColumns()
        {
            Query query = _databaseDef.CreateQuery()
                .From("Task")
                .Join("TaskType");

            const string expected = @"
SELECT    t.TaskId
        , t.ProjectId
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
FROM      tasks.Task t
JOIN      static.TaskType tt ON t.TaskTypeId = tt.TaskTypeId";
            string actual = query.SelectStatement;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MultipleJoin_AllColumns()
        {
            Query query = _databaseDef.CreateQuery()
                .From("Task")
                .Join("TaskType")
                .Join("User", "CreatorUserId")
                .Join("Project");

            const string expected = @"
SELECT    t.TaskId
        , t.OwnerUserId
        , t.DeveloperUserId
        , t.TesterUserId
        , t.Description
        , t.IsCompleted
        , t.DateTimeCreated
        , t.DateTimeCompleted
        , tt.TaskTypeId
        , tt.TaskTypeName
        , u.UserId
        , u.UserName
        , p.ProjectId
        , p.CustomerId
        , p.ProjectName
FROM      tasks.Task t
JOIN      static.TaskType tt ON t.TaskTypeId = tt.TaskTypeId
JOIN      security.[User] u ON t.CreatorUserId = u.UserId
JOIN      projects.Project p ON t.ProjectId = p.ProjectId";
            string actual = query.SelectStatement;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MultipleJoin_AllColumns_DuplicateNamesAliasedUsingForeignKeyName()
        {
            Query query = _databaseDef.CreateQuery()
                .From("Task")
                .Join("TaskType")
                .Join("User", "CreatorUserId")
                .Join("User", "OwnerUserId");

            const string expected = @"
SELECT    t.TaskId
        , t.ProjectId
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
FROM      tasks.Task t
JOIN      static.TaskType tt ON t.TaskTypeId = tt.TaskTypeId
JOIN      security.[User] u1 ON t.CreatorUserId = u1.UserId
LEFT JOIN security.[User] u2 ON t.OwnerUserId = u2.UserId";
            string actual = query.SelectStatement;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MultiJoin_MultiLevel_SelectAll()
        {
            /*
             * Setup:
             * Join Task to Project to Customer
             * Select all
             * Test:
             * Join to Project and Customer
             * Select all columns except for foreign keys tp Project and Customer
             */
            Query query = _databaseDef.CreateQuery()
                .From("Task")
                .Join("Project p")
                .Join("Customer", "p.CustomerId");

            const string expected = @"
SELECT    t.TaskId
        , t.TaskTypeId
        , t.CreatorUserId
        , t.OwnerUserId
        , t.DeveloperUserId
        , t.TesterUserId
        , t.Description
        , t.IsCompleted
        , t.DateTimeCreated
        , t.DateTimeCompleted
        , p.ProjectId
        , p.ProjectName
        , c.CustomerId
        , c.CustomerName
FROM      tasks.Task t
JOIN      projects.Project p ON t.ProjectId = p.ProjectId
JOIN      customers.Customer c ON p.CustomerId = c.CustomerId";
            string actual = query.SelectStatement;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MultiJoin_MultiLevel_SpecificRootColumnsOnly()
        {
            /*
             * Setup:
             * Join Task to Project to Customer
             * Select from Task only
             * Test:
             * No join to Project or Customer
             */
            Query query = _databaseDef.CreateQuery()
                .From("Task")
                .Join("Project p")
                .Join("Customer", "p.CustomerId")
                .Select("TaskId");

            const string expected = @"
SELECT    t.TaskId
FROM      tasks.Task t";
            string actual = query.SelectStatement;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MultiJoin_MultiLevel_SpecificRootAndLevel2Columns()
        {
            /*
             * Setup:
             * Join Task to Project to Customer
             * Select from Task and Customer
             * Test:
             * Join to Project and Customer
             */
            Query query = _databaseDef.CreateQuery()
                .From("Task")
                .Join("Project p")
                .Join("Customer c", "p.CustomerId")
                .Select("TaskId")
                .Select("c.CustomerId");

            const string expected = @"
SELECT    t.TaskId
        , c.CustomerId
FROM      tasks.Task t
JOIN      projects.Project p ON t.ProjectId = p.ProjectId
JOIN      customers.Customer c ON p.CustomerId = c.CustomerId";
            string actual = query.SelectStatement;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MultiJoin_MultiLevel_SpecificRootAndLevel2Columns_Level2NotVisible()
        {
            /*
             * Setup:
             * Join Task to Project to Customer
             * Select from Task and Customer but Customer Columns not visible
             * Test:
             * No join to Project or Customer
             */
            Query query = _databaseDef.CreateQuery()
                .From("Task")
                .Join("Project p")
                .Join("Customer c", "p.CustomerId")
                .Select("TaskId")
                .Select("c.CustomerId", isVisible: false);

            const string expected = @"
SELECT    t.TaskId
FROM      tasks.Task t";
            string actual = query.SelectStatement;

            Assert.AreEqual(expected, actual);
        }
    }
}
