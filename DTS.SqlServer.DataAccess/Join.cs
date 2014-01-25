using DTS.SqlServer.DataAccess.Definition;

namespace DTS.SqlServer.DataAccess
{
    public class Join
    {
        public Join(ObjectIdentifier objectIdentifier, ColumnIdentifier columnIdentifier)
        {
            ObjectIdentifier = objectIdentifier;
            ColumnIdentifier = columnIdentifier;
            Alias = objectIdentifier.Alias;
        }

        public ObjectIdentifier ObjectIdentifier { get; set; }

        public ColumnIdentifier ColumnIdentifier { get; set; }

        public Join ParentJoin { get; set; }

        public ColumnDef ParentColumnDef { get; set; }

        public ColumnDef ColumnDef { get; set; }

        public ObjectDef ObjectDef { get; set; }

        public string Alias { get; private set; }

        public string InternalAlias { get; internal set; }
    }
}