using DTS.SqlServer.DataAccess.Definition;

namespace DTS.SqlServer.DataAccess
{
    public class QueryColumn
    {
        public QueryColumn(SelectColumnIdentifier selectColumnIdentifier, bool isSelect, bool isWhere, bool isVisible)
        {
            SelectColumnIdentifier = selectColumnIdentifier;
            IsSelect = isSelect;
            IsWhere = isWhere;
            IsVisible = isVisible;
            Alias = selectColumnIdentifier.Alias;
        }

        public QueryColumn(Join join, ColumnDef columnDef)
        {
            Join = join;
            ColumnDef = columnDef;
            IsWhere = true;
            IsVisible = true;
            Alias = "";
        }

        public SelectColumnIdentifier SelectColumnIdentifier { get; set; }
        public Join Join { get; set; }
        public ColumnDef ColumnDef { get; set; }
        public string Alias { get; private set; }
        internal string InternalAlias { get; set; }
        public bool IsVisible { get; set; }
        public bool IsSelect { get; set; }
        public bool IsWhere { get; set; }
    }
}