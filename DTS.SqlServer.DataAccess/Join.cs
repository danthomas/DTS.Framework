using DTS.SqlServer.DataAccess.Definition;

namespace DTS.SqlServer.DataAccess
{
    public class Join
    {
        public Join(Join parentJoin, ColumnDef parentColumnDef, ObjectDef objectDef, ColumnDef columnDef, string alias)
        {
            ParentJoin = parentJoin;
            ParentColumnDef = parentColumnDef;
            ObjectDef = objectDef;
            ColumnDef = columnDef;
            Alias = alias;
        }

        public Join ParentJoin { get; private set; }

        public ColumnDef ParentColumnDef { get; private set; }

        public ColumnDef ColumnDef { get; private set; }

        public ObjectDef ObjectDef { get; private set; }

        public string Alias { get; private set; }

        public string InternalAlias { get; internal set; }
    }
}