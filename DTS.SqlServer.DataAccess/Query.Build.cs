using System;
using System.Collections.Generic;
using System.Linq;
using DTS.SqlServer.DataAccess.Definition;

namespace DTS.SqlServer.DataAccess
{
    public partial class Query
    {
        public bool Build()
        {
            _selectStatement = "";
            _validationErrors = new List<string>();

            if (SetJoinObjectDefinitions()
                && CheckAliases()
                && SetJoinAliases()
                && SetQueryColumnJoinsAndColumnDefs()
                && SetColumnAliases())
            {
                _selectStatement = GetSelect() + GetFrom() + GetWhere() + GetOrderBy();
                _isDirty = false;
            }

            return _validationErrors.Count == 0;
        }

        internal bool SetJoinAliases()
        {
            //set all internal aliases to aliases
            Joins.ForEach(join =>
            {
                join.InternalAlias = join.Alias;
            });

            Joins.Where(item => item.InternalAlias == "").ToList().ForEach(join =>
            {
                string internalAlias = new string(join.ObjectDef.Name.ToArray().Where(Char.IsUpper).ToArray()).ToLower();

                int i = 0;

                while (true)
                {
                    string s = String.Format("{0}{1}", internalAlias, ++i);
                    if (Joins.All(item => item.InternalAlias != s))
                    {
                        break;
                    }
                }

                if (i > 1)
                {
                    //existing internal aliases with numeric suffix so append count
                    //note that there will be at least 2
                    internalAlias = String.Format("{0}{1}", internalAlias, i);
                }
                else if (Joins.Any(item => item.InternalAlias == internalAlias))
                {
                    //no existing internal aliases with numeric suffix but internal alias already exists
                    //append 1 to existing internal alias
                    //append 2 to internal alias
                    Joins.Single(item => item.InternalAlias == internalAlias).InternalAlias = String.Format("{0}1", internalAlias);
                    internalAlias = String.Format("{0}2", internalAlias);
                }

                join.InternalAlias = internalAlias;
            });

            return true;
        }

        private bool CheckAliases()
        {
            var duplicateJoinAliases = Joins.Where(item => item.Alias != "")
                    .GroupBy(item => item.Alias,
                        item => item.Alias,
                        (alias, values) => new { Alias = alias, Count = values.Count() })
                        .Where(item => item.Count > 1)
                        .ToList();

            if (duplicateJoinAliases.Any())
            {
                AddValidationError("Duplicate Object Aliases found: {0}", duplicateJoinAliases.Select(item => item.Alias).Aggregate((a, b) => a + ", " + b));
            }

            var duplicateSelectAliases = SelectColumns.Where(item => item.Alias != "")
                   .GroupBy(item => item.Alias,
                       item => item.Alias,
                       (alias, values) => new { Alias = alias, Count = values.Count() })
                        .Where(item => item.Count > 1)
                        .ToList();

            if (duplicateSelectAliases.Any())
            {
                AddValidationError("Duplicate Select Column Aliases found: {0}", duplicateSelectAliases.Select(item => item.Alias).Aggregate((a, b) => a + ", " + b));
            }

            return duplicateSelectAliases.Count == 0 && duplicateJoinAliases.Count == 0;
        }

        private bool SetJoinObjectDefinitions()
        {
            foreach (Join join in Joins)
            {
                @join.ObjectDef = GetObjectDef(@join.ObjectIdentifier);
            }

            return Joins.All(item => item.ObjectDef != null);
        }

        internal bool SetColumnAliases()
        {
            SelectColumns.ToList().ForEach(item =>
            {
                item.InternalAlias = item.Alias;
            });

            //for each non aliased select column
            foreach (var duplicateNames in SelectColumns.Where(item => item.Alias == "")
                .GroupBy(item => item.ColumnDef.Name,
                        item => item,
                        (name, selectColumns) => new { Name = name, SelectColumns = selectColumns })
                .Where(item => item.SelectColumns.Count() > 1))
            {
                foreach (QueryColumn queryColumn in duplicateNames.SelectColumns)
                {
                    if (queryColumn.Join.ParentColumnDef.Name.EndsWith(queryColumn.Join.ColumnDef.Name)
                        && queryColumn.Join.ParentColumnDef.Name.Length > queryColumn.Join.ColumnDef.Name.Length)
                    {
                        queryColumn.InternalAlias = queryColumn.Join.ParentColumnDef.Name.Substring(0, queryColumn.Join.ParentColumnDef.Name.Length - queryColumn.Join.ColumnDef.Name.Length) + duplicateNames.Name;
                    }
                }
            }

            return true;
        }

        private bool SetQueryColumnJoinsAndColumnDefs()
        {
            foreach (QueryColumn queryColumn in SelectColumns)
            {
                JoinAndColumnDef joinAndColumnDef = GetSelectJoinAndColumnDef(queryColumn.ColumnIdentifier);

                queryColumn.Join = joinAndColumnDef.Join;
                queryColumn.ColumnDef = joinAndColumnDef.ColumnDef;
            }

            return true;
        }

        private string GetSelect()
        {
            bool first = true;

            string select = "";

            List<QueryColumn> selectColumns = SelectColumns.ToList();

            if (selectColumns.Count == 0)
            {
                foreach (Join join in Joins)
                {
                    foreach (ColumnDef columnDef in join.ObjectDef.ColumnDefs)
                    {
                        if (Joins.All(item => item.ColumnDef != columnDef))
                        {
                            selectColumns.Add(new QueryColumn(join, columnDef));       
                        }
                    }
                }
            }

            int maxLength = selectColumns.Where(item => item.IsVisible)
                .Select(item => String.Format("{0}.{1}", item.Join.InternalAlias, item.ColumnDef.Name))
                .Max(item => item.Length);

            if (selectColumns.Any(item => item.IsVisible))
            {
                foreach (QueryColumn queryColumn in selectColumns)
                {
                    select += String.Format("{0}{1}.{2}{3}{4}", first ? @"
SELECT    " : @"
        , ",
                    MakeSafe(queryColumn.Join.InternalAlias),
                    MakeSafe(queryColumn.ColumnDef.Name),
                    queryColumn.InternalAlias == "" ? "" : new String(' ', maxLength - String.Format("{0}.{1}", MakeSafe(queryColumn.Join.InternalAlias), MakeSafe(queryColumn.ColumnDef.Name)).Length + 1),
                    queryColumn.InternalAlias == "" ? "" : MakeSafe(queryColumn.InternalAlias));

                    first = false;
                }

            }
            else
            {
                select = @"
SELECT    *";
            }

            return select;
        }

        internal string GetFrom()
        {
            bool first = true;

            string from = "";

            foreach (Join join in GetRequiredJoins())
            {
                if (first)
                {
                    from = String.Format(@"
FROM      {0}.{1} {2}", join.ObjectDef.SchemaDef.Name, join.ObjectDef.Name, join.InternalAlias);
                }
                else
                {
                    from += @"
" + String.Format("{0}{1}.{2} {3} ON {4}.{5} = {6}.{7}",
                        join.ParentColumnDef.Nullable ? "LEFT JOIN " : "JOIN      ",
                        MakeSafe(join.ObjectDef.SchemaDef.Name),
                        MakeSafe(join.ObjectDef.Name),
                        MakeSafe(join.InternalAlias),
                        MakeSafe(join.ParentJoin.InternalAlias),
                        MakeSafe(join.ParentColumnDef.Name),
                        MakeSafe(join.InternalAlias),
                        MakeSafe(join.ColumnDef.Name));
                }

                first = false;
            }

            return from;
        }

        private IEnumerable<Join> GetRequiredJoins()
        {
            if (!SelectColumns.Any())
            {
                return Joins;
            }

            return Joins.Where(IsJoinRequired);
        }

        private bool IsJoinRequired(Join join)
        {
            //            if (_queryOptions != null && _queryOptions.ForceJoins)
            //            {
            //                return true;
            //            }

            //are any of the select query columns for this join isVisible
            bool selectColumnVisible = QueryColumns
                .Any(item => item.Join == join && item.IsSelect && item.IsVisible);

            //are any of the sub joins required
            bool subJoinsRequired = Joins
                .Where(item => item.ParentJoin == join)
                .Any(IsJoinRequired);

            return selectColumnVisible || subJoinsRequired;
        }

        private string GetWhere()
        {
            return null;
        }

        private string GetOrderBy()
        {
            return null;
        }
    }
}
