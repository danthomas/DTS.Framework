using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DTS.SqlServer.DataAccess.Definition
{
    public class DatabaseDef : Utilities.SqlServerConnection
    {
        public List<SchemaDef> SchemaDefs { get; private set; }

        public List<TypeDef> TypeDefs { get; private set; }

        protected List<ColumnDef> ColumnDefs { get; set; }

        public List<ObjectDef> ObjectDefs { get; set; }

        public void Populate(string server, string database)
        {
            const string sql = @"
select		s.schema_id SchemaId
			, s.name Name
from		sys.tables t
join		sys.schemas s on t.schema_id = s.schema_id 
group by	s.schema_id
			, s.name
			
select      t.user_type_id TypeId
			,t.name Name
from		sys.types t
			
select		t.object_id ObjectId
			, t.schema_id SchemaId
			, t.name Name
from		sys.tables t
join		sys.schemas s on t.schema_id = s.schema_id 

select		c.column_id ColumnId
			, c.object_id ObjectId
			, c.name Name
			, t2.user_type_id TypeId
			, case when t2.name in('char', 'varchar', 'nchar', 'nvarchar') then c.max_length end Length
			, c.is_nullable Nullable
			, cast(case when pk.name is null then 0 else 1 end as bit) IsPrimaryKey
			, fk.referenced_object_id ReferencedObjectId
from		sys.columns c
join		sys.tables t1 on c.object_id = t1.object_id	
join		sys.types t2 on c.user_type_id = t2.user_type_id
left join	(
			select	c.object_id
					, c.name
			from	sys.key_constraints kc
			join    sys.index_columns ic ON kc.parent_object_id = ic.object_id  and kc.unique_index_id = ic.index_id
			join    sys.columns c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
			where	kc.type = 'PK' ) pk on c.object_id = pk.object_id and c.name = pk.name
left join	(
			select	parent_object_id object_id,
					c.name,
					referenced_object_id
					, cref.name referenced_object_name
			from	sys.foreign_key_columns fkc
			join	sys.columns c on fkc.parent_column_id = c.column_id and fkc.parent_object_id = c.object_id
			join	sys.columns cref on fkc.referenced_column_id = cref.column_id and fkc.referenced_object_id = cref.object_id) fk on c.object_id = fk.object_id and c.name = fk.name";


            Populate(ExecuteDataSet(server, database, sql));

        }

        public void Populate(DataSet dataSet)
        {
            SchemaDefs = new List<SchemaDef>();
            TypeDefs = new List<TypeDef>();
            ObjectDefs = new List<ObjectDef>();
            ColumnDefs = new List<ColumnDef>();

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                SchemaDefs.Add(new SchemaDef(dataRow.Field<int>("SchemaId"),
                    dataRow.Field<string>("Name")));
            }

            foreach (DataRow dataRow in dataSet.Tables[1].Rows)
            {
                TypeDefs.Add(new TypeDef(dataRow.Field<int>("TypeId"),
                    dataRow.Field<string>("Name")));
            }

            foreach (DataRow dataRow in dataSet.Tables[2].Rows)
            {
                ObjectDef objectDef = new ObjectDef(dataRow.Field<int>("ObjectId"),
                    dataRow.Field<string>("Name"));

                objectDef.SchemaDef = SchemaDefs.Single(item => item.SchemaId == dataRow.Field<int>("SchemaId"));
                objectDef.SchemaDef.ObjectDefs.Add(objectDef);

                ObjectDefs.Add(objectDef);
            }

            foreach (DataRow dataRow in dataSet.Tables[3].Rows)
            {
                ColumnDef columnDef = new ColumnDef(dataRow.Field<int>("ColumnId"),
                    dataRow.Field<string>("Name"),
                    dataRow.Field<short?>("Length"),
                    dataRow.Field<bool>("Nullable"),
                    dataRow.Field<bool>("IsPrimaryKey"),
                    dataRow.Field<int?>("ReferencedObjectId"));

                columnDef.TypeDef = TypeDefs.Single(item => item.TypeId == dataRow.Field<int>("TypeId"));
                columnDef.ObjectDef = ObjectDefs.Single(item => item.ObjectId == dataRow.Field<int>("ObjectId"));
                columnDef.ObjectDef.ColumnDefs.Add(columnDef);

                if (columnDef.ReferencedObjectId != null)
                {
                    columnDef.ReferencedObjectDef = ObjectDefs.Single(item => item.ObjectId == columnDef.ReferencedObjectId);
                }

                ColumnDefs.Add(columnDef);
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (ObjectDef objectDef in ObjectDefs)
            {
                stringBuilder.AppendLine();
                stringBuilder.AppendFormat("{0} {1}.{2}", objectDef.ObjectId, objectDef.SchemaDef.Name, objectDef.Name);

                foreach (ColumnDef columnDef in objectDef.ColumnDefs)
                {
                    stringBuilder.AppendLine();
                    stringBuilder.AppendFormat("  {0} {1} {2}", columnDef.ColumnId, columnDef.Name, columnDef.TypeDef.Name);

                    if (columnDef.Length != null)
                    {
                        stringBuilder.AppendFormat("({0})", columnDef.Length);
                    }

                    if (columnDef.ReferencedObjectDef != null)
                    {
                        stringBuilder.AppendFormat(" {0}.{1}", columnDef.ReferencedObjectDef.SchemaDef.Name, columnDef.ReferencedObjectDef.Name);
                    }
                }
            }

            return stringBuilder.ToString();
        }

        public ObjectDef GetObject(string text)
        {
            ObjectDef objectDef = null;

            ObjectIdentifier objectIdentifier = new ObjectIdentifier(text);

            List<ObjectDef> objectDefs = ObjectDefs.Where(item => (item.SchemaDef.Name == objectIdentifier.Name || objectIdentifier.Schema == "")
                    && item.SchemaDef.Name == objectIdentifier.Name).ToList();
            
            if (objectDefs.Count == 1)
            {
                objectDef = objectDefs[0];
            }

            return objectDef;
        }
    }
}