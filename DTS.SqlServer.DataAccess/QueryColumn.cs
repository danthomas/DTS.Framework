using DTS.SqlServer.DataAccess.Definition;

namespace DTS.SqlServer.DataAccess
{
    public class QueryColumn
    {
        public QueryColumn(Join join, ColumnDef columnDef, string alias, bool isVisible, bool isSelect, bool isWhere)
        {
            Join = @join;
            ColumnDef = columnDef;
            Alias = alias;
            IsVisible = isVisible;
            IsSelect = isSelect;
            IsWhere = isWhere;
        }
        
        public Join Join { get; set; }
        public ColumnDef ColumnDef { get; set; }
        public string Alias { get; set; }
        internal string InternalAlias { get; set; }
        public bool IsVisible { get; set; }
        public bool IsSelect { get; set; }
        public bool IsWhere { get; set; }
    }
}