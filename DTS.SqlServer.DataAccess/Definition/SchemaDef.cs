using System.Collections.Generic;

namespace DTS.SqlServer.DataAccess.Definition
{
    public class SchemaDef
    {
        public SchemaDef(int schemaId, string name)
        {
            SchemaId = schemaId;
            Name = name;
            ObjectDefs = new List<ObjectDef>();
        }

        public int SchemaId { get; set; }

        public string Name { get; set; }

        public List<ObjectDef> ObjectDefs { get; set; }
    }
}