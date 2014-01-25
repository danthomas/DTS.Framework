using System.Collections.Generic;
using System.Linq;
using DTS.SqlServer.DataAccess;
using DTS.SqlServer.DataAccess.Definition;
using DTS.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DTS.Framework.Tests.SqlServer.DataAccess
{
    public partial class QueryTests : TestBase
    {
        private static DatabaseDef _databaseDef;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            _databaseDef = CreateDatabase(@"
<database name='DTS.SqlServer.DataAccess.Tests'>
    
    <schema name='static' />
    <schema name='security' />
    <schema name='projects' />
    <schema name='customers' />
    <schema name='tasks' />

    <type name='bigint' />
    <type name='binary' />
    <type name='bit' />
    <type name='char' />
    <type name='date' />
    <type name='datetime' />
    <type name='datetime2' />
    <type name='datetimeoffset' />
    <type name='decimal' />
    <type name='float' />
    <type name='geography' />
    <type name='geometry' />
    <type name='hierarchyid' />
    <type name='image' />
    <type name='int' />
    <type name='money' />
    <type name='nchar' />
    <type name='ntext' />
    <type name='numeric' />
    <type name='nvarchar' />
    <type name='real' />
    <type name='smalldatetime' />
    <type name='smallint' />
    <type name='smallmoney' />
    <type name='sql_variant' />
    <type name='sysname' />
    <type name='text' />
    <type name='time' />
    <type name='timestamp' />
    <type name='tinyint' />
    <type name='uniqueidentifier' />
    <type name='varbinary' />
    <type name='varchar' />
    <type name='xml' />

    <object name='TaskType' schema='static'>
        <column name='TaskTypeId' isPrimaryKey='true' type='tinyint' />
        <column name='TaskTypeName' type='varchar' length='30' />
    </object>
    <object name='TaskTimeType' schema='static'>
        <column name='TaskTimeTypeId' isPrimaryKey='true' type='tinyint' />
        <column name='TaskTimeTypeName' type='varchar' length='30' />
    </object>
    <object name='User' schema='security'>
        <column name='UserId' isPrimaryKey='true' type='smallint' />
        <column name='UserName' type='varchar' length='50' />
    </object>
    <object name='Customer' schema='customers'>
        <column name='CustomerId' isPrimaryKey='true' type='smallint' />
        <column name='CustomerName' type='varchar' length='50' />
    </object>
    <object name='Project' schema='projects'>
        <column name='ProjectId' isPrimaryKey='true' type='smallint' />
        <column name='CustomerId' type='smallint' referencedObject='Customer' />
        <column name='ProjectName' type='varchar' length='50' />
    </object>
    <object name='Task' schema='tasks'>
        <column name='TaskId' isPrimaryKey='true' type='int' />
        <column name='TaskTypeId' type='tinyint' referencedObject='TaskType' />
        <column name='ProjectId' type='smallint' referencedObject='Project' />
        <column name='CreatorUserId' type='smallint' referencedObject='User' />
        <column name='OwnerUserId' type='smallint' referencedObject='User' nullable='true' />
        <column name='DeveloperUserId' type='smallint' referencedObject='User' nullable='true' />
        <column name='TesterUserId' type='smallint' referencedObject='User' nullable='true' />
        <column name='Description' type='varchar' length='200' />
        <column name='IsCompleted' type='bit' />
        <column name='DateTimeCreated' type='datetime'/>
        <column name='DateTimeCompleted' type='datetime' nullable='true' />
    </object>
    <object name='TaskTime' schema='tasks'>
        <column name='TaskTimeId' isPrimaryKey='true' type='bigint' />
        <column name='TaskTimeTypeId' type='tinyint' />
        <column name='TaskId' type='int' referencedObject='Task' />
        <column name='UserId' type='smallint' referencedObject='User' />
        <column name='Description' type='varchar' length='400' />
    </object>

</database>");

//            PopulateDatabase(_databaseDef, @"<data>
//
//    <testing_TaskType taskTypeId='1' TaskTypeName='Task' />
//    <testing_TaskType taskTypeId='2' TaskTypeName='Bug' />
//    <testing_TaskType taskTypeId='3' TaskTypeName='Question' />
//    <testing_TaskType taskTypeId='4' TaskTypeName='Design' />
//
//    <testing_TaskTimeType taskTimeTypeId='1' TaskTimeTypeName='Research' />
//    <testing_TaskTimeType taskTimeTypeId='2' TaskTimeTypeName='Development' />
//    <testing_TaskTimeType taskTimeTypeId='3' TaskTimeTypeName='Testing' />
//    <testing_TaskTimeType taskTimeTypeId='4' TaskTimeTypeName='Meeting' />
//    <testing_TaskTimeType taskTimeTypeId='5' TaskTimeTypeName='Other' />
//
//    <testing_User userId='1' userName='thomasd' />
//    <testing_User userId='2' userName='barnetp' />
//    <testing_User userId='3' userName='daviesr' />
//    <testing_User userId='4' userName='birdyk' />
//
//    <testing_Task taskId='34' taskTypeId='1' creatorUserId='3' description='Create Database' isCompleted='1' dateTimeCreated='2013-11-04 12:34:56' dateTimeCompleted='2013-12-20 17:34:56' />
//    <testing_Task taskId='56' taskTypeId='1' creatorUserId='2' testerUserId='4' description='Populate Database' isCompleted='0' dateTimeCreated='2013-11-28 13:22:31' />
//    <testing_Task taskId='89' taskTypeId='2' creatorUserId='2' description='Setup Dev Environment' isCompleted='1' dateTimeCreated='2013-11-17 09:34:23' dateTimeCompleted='2013-11-30 10:48:11' />
//    <testing_Task taskId='145' taskTypeId='1' creatorUserId='4' developerUserId='1' description='Implement Business Layer' isCompleted='0' dateTimeCreated='2013-11-24 16:55:56' />
//    <testing_Task taskId='345' taskTypeId='3' creatorUserId='1' developerUserId='1' description='Construct UI' isCompleted='0' dateTimeCreated='2013-12-01 14:26:56' />
//
//    <testing_TaskTime taskTimeId='54374' taskId='34' taskTimeTypeId='4' userId='3' description='Dev' />
//    <testing_TaskTime taskTimeId='6582635' taskId='89' taskTimeTypeId='1' userId='2' description='Dev' />
//    <testing_TaskTime taskTimeId='26294' taskId='34' taskTimeTypeId='4' userId='2' description='Research' />
//    <testing_TaskTime taskTimeId='213' taskId='145' taskTimeTypeId='2' userId='3' description='Dev' />
//    <testing_TaskTime taskTimeId='852' taskId='34' taskTimeTypeId='2' userId='4' description='Dev Testing' />
//    <testing_TaskTime taskTimeId='8838628' taskId='145' taskTimeTypeId='3' userId='1' description='Testing' />
//</data>");
//
//            _databaseDef = new DatabaseDef();
//
//            _databaseDef.Populate(".\\sql2012", "DTS.SqlServer.DataAccess.Tests");
        }

        private static string ExecuteAndLayout(Query query)
        {
            List<List<object>> values = new List<List<object>>
                {
                    query.QueryColumns.Select(selectColumn => selectColumn.Alias).Cast<object>().ToList()
                };

//            values.AddRange(query.Execute()
//                .Select(@object => query.QueryColumns.Select<QueryColumn>(selectColumn => @object.GetValue(selectColumn.Alias)).ToList()));

            return values.Layout();
        }
    }
}
