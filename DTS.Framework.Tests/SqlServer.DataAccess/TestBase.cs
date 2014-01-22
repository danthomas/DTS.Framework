using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml.Linq;
using DTS.SqlServer.DataAccess.Definition;
using DTS.Utilities;

namespace DTS.Framework.Tests.SqlServer.DataAccess
{
    public class TestBase
    {
        protected static DatabaseDef CreateDatabaseDef(string document)
        {
            DataSet dataSet = CreateDataDetFromDocument(document);

            DatabaseDef databaseDef = new DatabaseDef();

            databaseDef.Populate(dataSet);

            return databaseDef;
        }

        protected static DatabaseDef CreateDatabaseDef()
        {
            DataSet dataSet = CreateDataSet();

            dataSet.Tables[0].Rows.Add(new object[] { 1, "static" });
            dataSet.Tables[0].Rows.Add(new object[] { 2, "security" });
            dataSet.Tables[0].Rows.Add(new object[] { 3, "customers" });
            dataSet.Tables[0].Rows.Add(new object[] { 4, "projects" });

            dataSet.Tables[1].Rows.Add(new object[] { 1, 2, "Role" });
            dataSet.Tables[1].Rows.Add(new object[] { 2, 2, "User" });
            dataSet.Tables[1].Rows.Add(new object[] { 3, 3, "Customer" });
            dataSet.Tables[1].Rows.Add(new object[] { 4, 4, "Project" });
            dataSet.Tables[1].Rows.Add(new object[] { 5, 4, "Task" });
            dataSet.Tables[1].Rows.Add(new object[] { 6, 1, "TaskType" });

            dataSet.Tables[2].Rows.Add(new object[] { 1, 1, "Id", "int", null, false, true, null });
            dataSet.Tables[2].Rows.Add(new object[] { 2, 1, "Name", "varchar", 20, false, false, null });

            dataSet.Tables[2].Rows.Add(new object[] { 3, 2, "Id", "int", null, false, true, null });
            dataSet.Tables[2].Rows.Add(new object[] { 4, 2, "Name", "varchar", 20, false, false, null });

            dataSet.Tables[2].Rows.Add(new object[] { 5, 3, "Id", "int", null, false, true, null });
            dataSet.Tables[2].Rows.Add(new object[] { 6, 3, "Name", "varchar", 20, false, false, null });

            dataSet.Tables[2].Rows.Add(new object[] { 7, 4, "Id", "int", null, false, true, null });
            dataSet.Tables[2].Rows.Add(new object[] { 8, 4, "CustomerId", "int", null, false, false, 3 });
            dataSet.Tables[2].Rows.Add(new object[] { 9, 4, "Name", "varchar", 20, false, false, null });

            dataSet.Tables[2].Rows.Add(new object[] { 10, 5, "Id", "int", null, false, true, null });
            dataSet.Tables[2].Rows.Add(new object[] { 11, 5, "ProjectId", "int", null, false, false, 4 });
            dataSet.Tables[2].Rows.Add(new object[] { 12, 5, "UserId", "int", null, false, false, 2 });
            dataSet.Tables[2].Rows.Add(new object[] { 12, 5, "TaskTypeId", "int", null, false, false, 6 });
            dataSet.Tables[2].Rows.Add(new object[] { 13, 5, "Description", "varchar", 20, false, false, null });

            dataSet.Tables[2].Rows.Add(new object[] { 14, 6, "Id", "int", null, false, true, null });
            dataSet.Tables[2].Rows.Add(new object[] { 15, 6, "Name", "varchar", 20, false, false, null });

            DatabaseDef databaseDef = new DatabaseDef();

            databaseDef.Populate(dataSet);

            return databaseDef;
        }

        protected static DataSet CreateDataDetFromDocument(string document)
        {
            XDocument xDocument = XDocument.Parse(document);

            DataSet dataSet = CreateDataSet();

            dataSet.DataSetName = xDocument.Root.Attribute("name").Value;

            int schemaId = 0;
            int objectId = 0;
            int columnId = 0;

            foreach (XElement schemaElement in xDocument.Root.Elements("schema"))
            {
                dataSet.Tables[0].Rows.Add(new object[]
                    {
                        ++schemaId,
                        schemaElement.Attribute("name").Value
                    });

                foreach (XElement objectElement in schemaElement.Elements("object"))
                {
                    dataSet.Tables[1].Rows.Add(new object[]
                        {
                            ++objectId,
                            schemaId,
                            objectElement.Attribute("name").Value
                        });
                }
            }

            foreach (XElement schemaElement in xDocument.Root.Elements("schema"))
            {
                foreach (XElement objectElement in schemaElement.Elements("object"))
                {
                    objectId = (int)dataSet.Tables[1].Select(String.Format("name = '{0}'", objectElement.Attribute("name").Value))[0][0];

                    foreach (XElement columnElement in objectElement.Elements("column"))
                    {
                        bool isPrimaryKey = columnElement.Attribute("isPrimaryKey") != null && Boolean.Parse(columnElement.Attribute("isPrimaryKey").Value);

                        int? referencedObjectId = null;

                        if (columnElement.Attribute("referencedObject") != null)
                        {
                            referencedObjectId = (int)dataSet.Tables[1].Select(String.Format("name = '{0}'", columnElement.Attribute("referencedObject").Value))[0][0];
                        }

                        int? length = columnElement.Attribute("length") != null ? new int?(Int32.Parse(columnElement.Attribute("length").Value)) : null;

                        bool nullable = columnElement.Attribute("nullable") != null && Boolean.Parse(columnElement.Attribute("nullable").Value);

                        dataSet.Tables[2].Rows.Add(new object[]
                            {
                                ++columnId,
                                objectId,
                                columnElement.Attribute("name").Value,
                                columnElement.Attribute("type").Value,
                                length,
                                nullable,   
                                isPrimaryKey,
                                referencedObjectId
                            });
                    }
                }
            }
            return dataSet;
        }

        protected static DataSet CreateDataSet()
        {
            DataSet dataSet = new DataSet();

            DataTable schemaTable = new DataTable();
            dataSet.Tables.Add(schemaTable);

            schemaTable.Columns.Add("FormatExId", typeof(Int32));
            schemaTable.Columns.Add("Name", typeof(String));

            DataTable objectTable = new DataTable();
            dataSet.Tables.Add(objectTable);

            objectTable.Columns.Add("ObjectId", typeof(Int32));
            objectTable.Columns.Add("FormatExId", typeof(Int32));
            objectTable.Columns.Add("Name", typeof(String));

            DataTable columnTable = new DataTable();
            dataSet.Tables.Add(columnTable);

            columnTable.Columns.Add("ColumnId", typeof(Int32));
            columnTable.Columns.Add("ObjectId", typeof(Int32));
            columnTable.Columns.Add("Name", typeof(String));
            columnTable.Columns.Add("Type", typeof(String));
            columnTable.Columns.Add("Length", typeof(Int16));
            columnTable.Columns.Add("Nullable", typeof(Boolean));
            columnTable.Columns.Add("IsPrimaryKey", typeof(Boolean));
            columnTable.Columns.Add("ReferencedObjectId", typeof(Int32));

            return dataSet;
        }

        protected static void PopulateDatabase(DatabaseDef databaseDef, string document)
        {
            XDocument xDocument = XDocument.Parse(document);

            string sql = "";

            foreach (XElement element in xDocument.Root.Elements())
            {
                ObjectDef objectDef = databaseDef.ObjectDefs.Single(item => String.Format("{0}_{1}", item.SchemaDef.Name, item.Name) == element.Name);
                sql += @"
insert into [{0}].[{1}] (".FormatEx(objectDef.SchemaDef.Name, objectDef.Name);

                bool first = true;

                foreach (XAttribute attribute in element.Attributes())
                {
                    sql += @"{0}{1}".FormatEx(first ? "" : ",", attribute.Name);

                    first = false;
                }

                sql += @")
values(";
                first = true;

                foreach (XAttribute attribute in element.Attributes())
                {
                    ColumnDef columnDef = objectDef.ColumnDefs.Single(item => String.Compare(item.Name, attribute.Name.LocalName, StringComparison.OrdinalIgnoreCase) == 0);

                    string value = attribute.Value;

                    if (columnDef.Type == typeof(String) || columnDef.Type == typeof(DateTime))
                    {
                        value = "'{0}'".FormatEx(value);
                    }

                    sql += @"{0}{1}".FormatEx(first ? "" : ",", value);

                    first = false;
                }

                sql += @");";

            }

            ExecuteNonQuery(sql);
        }

        protected static DatabaseDef CreateDatabase(string document)
        {
            DatabaseDef databaseDef = CreateDatabaseDef(document);

            ExecuteNonQuery(@"
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'DTS.DataAccess.Tests')
BEGIN
    ALTER DATABASE [DTS.DataAccess.Tests] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [DTS.DataAccess.Tests]
END;", "master");

            ExecuteNonQuery(@"
CREATE DATABASE [DTS.DataAccess.Tests]", "master");

            string sql = @"";

            foreach (SchemaDef schemaDef in databaseDef.SchemaDefs)
            {
                sql += String.Format(@"
CREATE SCHEMA [{0}]", schemaDef.Name);
            }

            foreach (ObjectDef objectDef in databaseDef.ObjectDefs)
            {
                sql += String.Format(@"
CREATE TABLE [{0}].[{1}]
(", objectDef.SchemaDef.Name, objectDef.Name);

                bool first = true;

                foreach (ColumnDef columnDef in objectDef.ColumnDefs)
                {
                    sql += String.Format(@"
{0}[{1}] {2}{3} {4}{5}", first ? "    " : "  , ",
                               columnDef.Name,
                               columnDef.TypeDef.Name,
                               columnDef.Length == null ? "" : String.Format("({0})", columnDef.Length),
                               columnDef.Nullable ? "NULL" : "NOT NULL",
                               columnDef.IsPrimaryKey ? " PRIMARY KEY" : "");

                    first = false;
                }

                foreach (ColumnDef columnDef in objectDef.ColumnDefs.Where(item => item.ReferencedObjectDef != null))
                {
                    sql += String.Format(@"
  , CONSTRAINT FK_{0}_{1}_{2} FOREIGN KEY ([{2}]) REFERENCES [{3}].[{4}] ({5})",
                                                  objectDef.SchemaDef.Name,
                                                  objectDef.Name,
                                                  columnDef.Name,
                                                  columnDef.ReferencedObjectDef.SchemaDef.Name,
                                                  columnDef.ReferencedObjectDef.Name,
                                                  columnDef.ReferencedObjectDef.ColumnDefs.Single(item => item.IsPrimaryKey).Name);
                }

                sql += @"
)";
            }

            ExecuteNonQuery(sql);

            return databaseDef;
        }

        private static void ExecuteNonQuery(string sql, string database = null)
        {
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);

            if (database != null)
            {
                connectionStringBuilder.InitialCatalog = database;
            }

            using (SqlConnection connection = new SqlConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        //        private void ExecuteDataSet(string database, string sql)
        //        {
        //            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString)
        //            {
        //                InitialCatalog = database
        //            };
        //
        //            using (SqlConnection connection = new SqlConnection(connectionStringBuilder.ConnectionString))
        //            {
        //                connection.Open();
        //
        //                using (SqlCommand command = new SqlCommand(sql, connection))
        //                {
        //                    command.ExecuteNonQuery();
        //                }
        //            }
        //        }

    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Project
    {
        public Project(int id, string name, string customer)
        {
            Id = id;
            Name = name;
            Customer = customer;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Customer { get; set; }
    }

    public class User
    {
        public User(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class Task
    {
        public Task(int id, string project, string customer, string user, string taskType, string description)
        {
            Id = id;
            Project = project;
            Customer = customer;
            User = user;
            TaskType = taskType;
            Description = description;
        }

        public int Id { get; set; }

        public string Project { get; set; }

        public string Customer { get; set; }

        public string User { get; set; }

        public string TaskType { get; set; }

        public string Description { get; set; }
    }
}