using DTS.SqlServer.DataAccess.Definition;

namespace DTS.SqlServer.DataAccess
{
    public class Join
    {
        public Join(ObjectIdentifier objectIdentifier, SelectColumnIdentifier selectColumnIdentifier)
        {
            ObjectIdentifier = objectIdentifier;
            SelectColumnIdentifier = selectColumnIdentifier;
            Alias = objectIdentifier.Alias;
        }

        public ObjectIdentifier ObjectIdentifier { get; set; }

        public SelectColumnIdentifier SelectColumnIdentifier { get; set; }

        public Join ParentJoin { get; set; }

        public ColumnDef ParentColumnDef { get; set; }

        public ColumnDef ColumnDef { get; set; }

        public ObjectDef ObjectDef { get; set; }

        public string Alias { get; private set; }

        public string InternalAlias { get; internal set; }
    }
}