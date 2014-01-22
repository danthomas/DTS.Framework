using System;
using System.Data;
using System.Linq;
using System.Xml.Linq;
using DTS.SqlServer.DataAccess.Definition;
using DTS.Utilities;

namespace DTS.Framework.Tests.SqlServer.DataAccess
{
    public class TestBase : SqlServerConnection
    {
        private const string LocalServerName = ".\\sql2012";
        private const string TestDatabaseName = "DTS.SqlServer.DataAccess.Tests";

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

            dataSet.Tables[0].Rows.Add(new object[] { 1, "int" });
            dataSet.Tables[0].Rows.Add(new object[] { 2, "varchar" });


            dataSet.Tables[2].Rows.Add(new object[] { 1, 2, "Role" });
            dataSet.Tables[2].Rows.Add(new object[] { 2, 2, "User" });
            dataSet.Tables[2].Rows.Add(new object[] { 3, 3, "Customer" });
            dataSet.Tables[2].Rows.Add(new object[] { 4, 4, "Project" });
            dataSet.Tables[2].Rows.Add(new object[] { 5, 4, "Task" });
            dataSet.Tables[2].Rows.Add(new object[] { 6, 1, "TaskType" });

            dataSet.Tables[3].Rows.Add(new object[] { 1, 1, "Id", 1, null, false, true, null });
            dataSet.Tables[3].Rows.Add(new object[] { 2, 1, "Name", 2, 20, false, false, null });

            dataSet.Tables[3].Rows.Add(new object[] { 3, 2, "Id", 1, null, false, true, null });
            dataSet.Tables[3].Rows.Add(new object[] { 4, 2, "Name", 2, 20, false, false, null });

            dataSet.Tables[3].Rows.Add(new object[] { 5, 3, "Id", 1, null, false, true, null });
            dataSet.Tables[3].Rows.Add(new object[] { 6, 3, "Name", 2, 20, false, false, null });

            dataSet.Tables[3].Rows.Add(new object[] { 7, 4, "Id", 1, null, false, true, null });
            dataSet.Tables[3].Rows.Add(new object[] { 8, 4, "CustomerId", 1, null, false, false, 3 });
            dataSet.Tables[3].Rows.Add(new object[] { 9, 4, "Name", 2, 20, false, false, null });

            dataSet.Tables[3].Rows.Add(new object[] { 10, 5, "Id", 1, null, false, true, null });
            dataSet.Tables[3].Rows.Add(new object[] { 11, 5, "ProjectId", 1, null, false, false, 4 });
            dataSet.Tables[3].Rows.Add(new object[] { 12, 5, "UserId", 1, null, false, false, 2 });
            dataSet.Tables[3].Rows.Add(new object[] { 12, 5, "TaskTypeId", 1, null, false, false, 6 });
            dataSet.Tables[3].Rows.Add(new object[] { 13, 5, "Description", 2, 20, false, false, null });

            dataSet.Tables[3].Rows.Add(new object[] { 14, 6, "Id", 1, null, false, true, null });
            dataSet.Tables[3].Rows.Add(new object[] { 15, 6, "Name", 2, 20, false, false, null });

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
            int typeId = 0;
            int objectId = 0;
            int columnId = 0;
            
            foreach (XElement schemaElement in xDocument.Root.Elements("schema"))
            {
                dataSet.Tables[0].Rows.Add(new object[]
                    {
                        ++schemaId,
                        schemaElement.Attribute("name").Value
                    });
            }

            foreach (XElement typeElement in xDocument.Root.Elements("type"))
            {
                dataSet.Tables[1].Rows.Add(new object[]
                {
                    ++typeId,
                    typeElement.Attribute("name").Value
                });
            }

            foreach (XElement objectElement in xDocument.Root.Elements("object"))
            {
                schemaId = (int)dataSet.Tables[0].Select(String.Format("name = '{0}'", objectElement.Attribute("schema").Value))[0][0];

                dataSet.Tables[2].Rows.Add(new object[]
                        {
                            ++objectId,
                            schemaId,
                            objectElement.Attribute("name").Value
                        });
            }
            
            foreach (XElement objectElement in xDocument.Root.Elements("object"))
            {
                objectId = (int)dataSet.Tables[2].Select(String.Format("name = '{0}'", objectElement.Attribute("name").Value))[0][0];
              
                foreach (XElement columnElement in objectElement.Elements("column"))
                {
                    typeId = (int)dataSet.Tables[1].Select(String.Format("name = '{0}'", columnElement.Attribute("type").Value))[0][0];

                    bool isPrimaryKey = columnElement.Attribute("isPrimaryKey") != null && Boolean.Parse(columnElement.Attribute("isPrimaryKey").Value);

                    int? referencedObjectId = null;

                    if (columnElement.Attribute("referencedObject") != null)
                    {
                        referencedObjectId = (int)dataSet.Tables[2].Select(String.Format("name = '{0}'", columnElement.Attribute("referencedObject").Value))[0][0];
                    }

                    int? length = columnElement.Attribute("length") != null ? new int?(Int32.Parse(columnElement.Attribute("length").Value)) : null;

                    bool nullable = columnElement.Attribute("nullable") != null && Boolean.Parse(columnElement.Attribute("nullable").Value);

                    dataSet.Tables[3].Rows.Add(new object[]
                            {
                                ++columnId,
                                objectId,
                                columnElement.Attribute("name").Value,
                                typeId,
                                length,
                                nullable,   
                                isPrimaryKey,
                                referencedObjectId
                            });
                }

            }
            return dataSet;
        }

        protected static DataSet CreateDataSet()
        {
            DataSet dataSet = new DataSet();

            DataTable schemaTable = new DataTable();
            dataSet.Tables.Add(schemaTable);

            schemaTable.Columns.Add("SchemaId", typeof(Int32));
            schemaTable.Columns.Add("Name", typeof(String));

            DataTable typeTable = new DataTable();
            dataSet.Tables.Add(typeTable);

            typeTable.Columns.Add("TypeId", typeof(Int32));
            typeTable.Columns.Add("Name", typeof(String));

            DataTable objectTable = new DataTable();
            dataSet.Tables.Add(objectTable);

            objectTable.Columns.Add("ObjectId", typeof(Int32));
            objectTable.Columns.Add("SchemaId", typeof(Int32));
            objectTable.Columns.Add("Name", typeof(String));

            DataTable columnTable = new DataTable();
            dataSet.Tables.Add(columnTable);

            columnTable.Columns.Add("ColumnId", typeof(Int32));
            columnTable.Columns.Add("ObjectId", typeof(Int32));
            columnTable.Columns.Add("Name", typeof(String));
            columnTable.Columns.Add("TypeId", typeof(Int32));
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

            Execute(TestDatabaseName, sql);
        }

        protected static DatabaseDef CreateDatabase(string document)
        {
            DatabaseDef databaseDef = CreateDatabaseDef(document);

            Execute("master", @"
IF EXISTS (SELECT * FROM sys.databases WHERE name = '{0}')
BEGIN
    ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [{0}]
END;", TestDatabaseName);

            Execute("master", @"
CREATE DATABASE [{0}]", TestDatabaseName);

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

            Execute(TestDatabaseName, sql);

            return databaseDef;
        }

        private static void Execute(string datbase, string format, params object[] args)
        {
            ExecuteNonQuery(LocalServerName, datbase, format, args);
        }
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