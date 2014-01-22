using System;

namespace DTS.SqlServer.DataAccess.Definition
{
    public class ColumnDef
    {
        public ColumnDef(int columnId, string name, short? length, bool nullable, bool isPrimaryKey, int? referencedObjectId)
        {
            ColumnId = columnId;
            Name = name;
            Length = length;
            Nullable = nullable;
            IsPrimaryKey = isPrimaryKey;
            ReferencedObjectId = referencedObjectId;
        }

        public int ColumnId { get; set; }

        public string Name { get; set; }

        public short? Length { get; set; }

        public bool Nullable { get; set; }

        public bool IsPrimaryKey { get; set; }

        public int? ReferencedObjectId { get; set; }

        public ObjectDef ObjectDef { get; set; }

        public ObjectDef ReferencedObjectDef { get; set; }

        public TypeDef TypeDef { get; set; }

        public Type Type { get; set; }
    }
}
