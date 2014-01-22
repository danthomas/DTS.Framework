using System.Collections.Generic;

namespace DTS.SqlServer.DataAccess.Definition
{
    public class ObjectDef
    {
        public ObjectDef(int objectId, string name)
        {
            ObjectId = objectId;
            Name = name;
            ColumnDefs = new List<ColumnDef>();
        }

        public int ObjectId { get; set; }

        public string Name { get; set; }

        public SchemaDef SchemaDef { get; set; }

        public List<ColumnDef> ColumnDefs { get; set; }
    }
}