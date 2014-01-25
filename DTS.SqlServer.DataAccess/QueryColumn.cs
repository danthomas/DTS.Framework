using DTS.SqlServer.DataAccess.Definition;

namespace DTS.SqlServer.DataAccess
{
    public class QueryColumn
    {
        public QueryColumn(ColumnIdentifier columnIdentifier, bool isSelect, bool isWhere, bool isVisible)
        {
            ColumnIdentifier = columnIdentifier;
            IsSelect = isSelect;
            IsWhere = isWhere;
            IsVisible = isVisible;
            Alias = columnIdentifier.Alias;
        }

        public QueryColumn(Join join, ColumnDef columnDef)
        {
            Join = join;
            ColumnDef = columnDef;
            IsWhere = true;
            IsVisible = true;
            Alias = "";
        }

        public ColumnIdentifier ColumnIdentifier { get; set; }
        public Join Join { get; set; }
        public ColumnDef ColumnDef { get; set; }
        public string Alias { get; private set; }
        internal string InternalAlias { get; set; }
        public bool IsVisible { get; set; }
        public bool IsSelect { get; set; }
        public bool IsWhere { get; set; }
    }
}