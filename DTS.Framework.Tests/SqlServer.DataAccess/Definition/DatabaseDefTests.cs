using System;
using System.Data;
using DTS.SqlServer.DataAccess.Definition;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DTS.Framework.Tests.SqlServer.DataAccess.Definition
{
    [TestClass]
    public class DatabaseDefTests
    {
        [TestMethod]
        public void Populate()
        {
            DataSet definitionDataSet = CreateDefinitionDataSet();

            DatabaseDef databaseDef = new DatabaseDef();

            databaseDef.Populate(definitionDataSet);

            Assert.AreEqual(@"
1 security.Role
  1 Id int
  2 Name varchar(20)
2 security.User
  3 Id int
  4 Name varchar(20)
3 customers.Customer
  5 Id int
  6 Name varchar(20)
4 projects.Project
  7 Id int
  8 CustomerId int customers.Customer
  9 Name varchar(20)
5 projects.Task
  10 Id int
  11 ProjectId int projects.Project
  12 UserId int security.User
  12 TaskTypeId int static.TaskType
  13 Description varchar(20)
6 static.TaskType
  14 Id int
  15 Name varchar(20)", databaseDef.ToString());
        }

        protected static DataSet CreateDefinitionDataSet()
        {
            DataSet dataSet = CreateEmptyDefinitionDataSet();

            //schemas
            dataSet.Tables[0].Rows.Add(new object[] { 1, "static" });
            dataSet.Tables[0].Rows.Add(new object[] { 2, "security" });
            dataSet.Tables[0].Rows.Add(new object[] { 3, "customers" });
            dataSet.Tables[0].Rows.Add(new object[] { 4, "projects" });

            //types
            dataSet.Tables[1].Rows.Add(new object[] { 127, "bigint", 8, 19, 0 });
            dataSet.Tables[1].Rows.Add(new object[] { 173, "binary", 8000, 0, 0 });
            dataSet.Tables[1].Rows.Add(new object[] { 104, "bit", 1, 1, 0 });
            dataSet.Tables[1].Rows.Add(new object[] { 175, "char", 8000, 0, 0 });
            dataSet.Tables[1].Rows.Add(new object[] { 40, "date", 3, 10, 0 });
            dataSet.Tables[1].Rows.Add(new object[] { 61, "datetime", 8, 23, 3 });
            dataSet.Tables[1].Rows.Add(new object[] { 42, "datetime2", 8, 27, 7 });
            dataSet.Tables[1].Rows.Add(new object[] { 43, "datetimeoffset", 10, 34, 7 });
            dataSet.Tables[1].Rows.Add(new object[] { 106, "decimal", 17, 38, 38 });
            dataSet.Tables[1].Rows.Add(new object[] { 62, "float", 8, 53, 0 });
            dataSet.Tables[1].Rows.Add(new object[] { 130, "geography", -1, 0, 0 });
            dataSet.Tables[1].Rows.Add(new object[] { 129, "geometry", -1, 0, 0 });
            dataSet.Tables[1].Rows.Add(new object[] { 128, "hierarchyid", 892, 0, 0 });
            dataSet.Tables[1].Rows.Add(new object[] { 34, "image", 16, 0, 0 });
            dataSet.Tables[1].Rows.Add(new object[] { 56, "int", 4, 10, 0 });
            dataSet.Tables[1].Rows.Add(new object[] { 60, "money", 8, 19, 4 });
            dataSet.Tables[1].Rows.Add(new object[] { 239, "nchar", 8000, 0, 0 });
            dataSet.Tables[1].Rows.Add(new object[] { 99, "ntext", 16, 0, 0 });
            dataSet.Tables[1].Rows.Add(new object[] { 108, "numeric", 17, 38, 38 });
            dataSet.Tables[1].Rows.Add(new object[] { 231, "nvarchar", 8000, 0, 0 });
            dataSet.Tables[1].Rows.Add(new object[] { 59, "real", 4, 24, 0 });
            dataSet.Tables[1].Rows.Add(new object[] { 58, "smalldatetime", 4, 16, 0 });
            dataSet.Tables[1].Rows.Add(new object[] { 52, "smallint", 2, 5, 0 });
            dataSet.Tables[1].Rows.Add(new object[] { 122, "smallmoney", 4, 10, 4 });
            dataSet.Tables[1].Rows.Add(new object[] { 98, "sql_variant", 8016, 0, 0 });
            dataSet.Tables[1].Rows.Add(new object[] { 256, "sysname", 256, 0, 0 });
            dataSet.Tables[1].Rows.Add(new object[] { 35, "text", 16, 0, 0 });
            dataSet.Tables[1].Rows.Add(new object[] { 41, "time", 5, 16, 7 });
            dataSet.Tables[1].Rows.Add(new object[] { 189, "timestamp", 8, 0, 0 });
            dataSet.Tables[1].Rows.Add(new object[] { 48, "tinyint", 1, 3, 0 });
            dataSet.Tables[1].Rows.Add(new object[] { 36, "uniqueidentifier", 16, 0, 0 });
            dataSet.Tables[1].Rows.Add(new object[] { 165, "varbinary", 8000, 0, 0 });
            dataSet.Tables[1].Rows.Add(new object[] { 167, "varchar", 8000, 0, 0 });
            dataSet.Tables[1].Rows.Add(new object[] { 241, "xml", -1, 0, 0 });

            //objects
            dataSet.Tables[2].Rows.Add(new object[] { 1, 2, "Role" });
            dataSet.Tables[2].Rows.Add(new object[] { 2, 2, "User" });
            dataSet.Tables[2].Rows.Add(new object[] { 3, 3, "Customer" });
            dataSet.Tables[2].Rows.Add(new object[] { 4, 4, "Project" });
            dataSet.Tables[2].Rows.Add(new object[] { 5, 4, "Task" });
            dataSet.Tables[2].Rows.Add(new object[] { 6, 1, "TaskType" });

            //columns
            dataSet.Tables[3].Rows.Add(new object[] { 1, 1, "Id", 56, null, false, true, null });
            dataSet.Tables[3].Rows.Add(new object[] { 2, 1, "Name", 167, 20, false, false, null });

            dataSet.Tables[3].Rows.Add(new object[] { 3, 2, "Id", 56, null, false, true, null });
            dataSet.Tables[3].Rows.Add(new object[] { 4, 2, "Name", 167, 20, false, false, null });

            dataSet.Tables[3].Rows.Add(new object[] { 5, 3, "Id", 56, null, false, true, null });
            dataSet.Tables[3].Rows.Add(new object[] { 6, 3, "Name", 167, 20, false, false, null });

            dataSet.Tables[3].Rows.Add(new object[] { 7, 4, "Id", 56, null, false, true, null });
            dataSet.Tables[3].Rows.Add(new object[] { 8, 4, "CustomerId", 56, null, false, false, 3 });
            dataSet.Tables[3].Rows.Add(new object[] { 9, 4, "Name", 167, 20, false, false, null });

            dataSet.Tables[3].Rows.Add(new object[] { 10, 5, "Id", 56, null, false, true, null });
            dataSet.Tables[3].Rows.Add(new object[] { 11, 5, "ProjectId", 56, null, false, false, 4 });
            dataSet.Tables[3].Rows.Add(new object[] { 12, 5, "UserId", 56, null, false, false, 2 });
            dataSet.Tables[3].Rows.Add(new object[] { 12, 5, "TaskTypeId", 56, null, false, false, 6 });
            dataSet.Tables[3].Rows.Add(new object[] { 13, 5, "Description", 167, 20, false, false, null });

            dataSet.Tables[3].Rows.Add(new object[] { 14, 6, "Id", 56, null, false, true, null });
            dataSet.Tables[3].Rows.Add(new object[] { 15, 6, "Name", 167, 20, false, false, null });

            return dataSet;
        }

        protected static DataSet CreateEmptyDefinitionDataSet()
        {
            DataSet dataSet = new DataSet();

            //schemas
            DataTable schemaTable = new DataTable();
            dataSet.Tables.Add(schemaTable);

            schemaTable.Columns.Add("SchemaId", typeof(Int32));
            schemaTable.Columns.Add("Name", typeof(String));

            //types
            DataTable typeTable = new DataTable();
            dataSet.Tables.Add(typeTable);

            typeTable.Columns.Add("TypeId", typeof(Int32));
            typeTable.Columns.Add("Name", typeof(String));
            typeTable.Columns.Add("MaxLength", typeof(String));
            typeTable.Columns.Add("Precision", typeof(String));
            typeTable.Columns.Add("Scale", typeof(String));

            //objects
            DataTable objectTable = new DataTable();
            dataSet.Tables.Add(objectTable);

            objectTable.Columns.Add("ObjectId", typeof(Int32));
            objectTable.Columns.Add("SchemaId", typeof(Int32));
            objectTable.Columns.Add("Name", typeof(String));

            //columns
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
    }
}

/*
--types
SELECT 'dataSet.Tables[1].Rows.Add(new object[] { ' 
+ CAST(user_type_id as varchar(10)) 
+ ', "' + name + '"'
+ ', ' + CAST(max_length as varchar(10)) 
+ ', ' + CAST(precision as varchar(10))
+ ', ' + CAST(scale as varchar(10))
+ ' });
' from sys.types order by name

 */