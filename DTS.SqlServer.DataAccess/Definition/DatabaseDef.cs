using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DTS.SqlServer.DataAccess.Definition
{
    public class DatabaseDef
    {
        public List<SchemaDef> SchemaDefs { get; private set; }

        public List<TypeDef> TypeDefs { get; private set; }

        protected List<ColumnDef> ColumnDefs { get; set; }

        public List<ObjectDef> ObjectDefs { get; set; }

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
    }
}