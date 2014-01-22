using DTS.SqlServer.DataAccess.Definition;

namespace DTS.SqlServer.DataAccess
{
    public class QueryColumn
    {
        public QueryColumn(Join join, ColumnDef columnDef, string alias, bool visible, bool isSelect, bool isWhere)
        {
            Join = @join;
            ColumnDef = columnDef;
            Alias = alias;
            Visible = visible;
            IsSelect = isSelect;
            IsWhere = isWhere;
        }
        
        public Join Join { get; set; }
        public ColumnDef ColumnDef { get; set; }
        public string Alias { get; set; }
        public bool Visible { get; set; }
        public bool IsSelect { get; set; }
        public bool IsWhere { get; set; }
    }
}