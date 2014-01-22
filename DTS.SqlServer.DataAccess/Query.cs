using System;
using System.Collections.Generic;
using System.Linq;
using DTS.SqlServer.DataAccess.Definition;

namespace DTS.SqlServer.DataAccess
{
    public partial class Query
    {
        private readonly DatabaseDef _databaseDef;

        public Query(DatabaseDef databaseDef)
        {
            _databaseDef = databaseDef;
            QueryColumns = new List<QueryColumn>();
            Joins = new List<Join>();
        }

        public List<QueryColumn> QueryColumns { get; private set; }

        public List<Join> Joins { get; set; }

        public IEnumerable<QueryColumn> SelectColumns { get { return QueryColumns.Where(item => item.IsSelect); } }

        internal void SetJoinAlias(Join join)
        {
            if (join.Alias != "" && Joins.Any(item => item != join && item.Alias == join.Alias))
            {
                throw new DataAccessException(DataAccessExceptionType.AliasAlreadySpecified, "Alias {0} is already specified.", join.Alias);
            }

            if (join.Alias != "")
            {
                join.InternalAlias = join.Alias;
            }
            else
            {
                string internalAlias = new string(join.ObjectDef.Name.ToArray().Where(Char.IsUpper).ToArray()).ToLower();

                if (Joins.Any(item => item.InternalAlias == internalAlias))
                {
                    Joins.Single(item => item.InternalAlias == internalAlias).InternalAlias += "1";
                    internalAlias = internalAlias + "2";
                }

                join.InternalAlias = internalAlias;
            }
        }

        public Query From(string @object)
        {
            ObjectIdentifier objectIdentifier = new ObjectIdentifier(@object);

            ObjectDef objectDef = GetObjectDef(objectIdentifier);

            Join(null, null, objectDef, null, objectIdentifier);

            return this;
        }

        public Query SelectAll()
        {
            SelectAll(Joins[0]);

            return this;
        }

        public void SelectAll(Join join)
        {
            foreach (ColumnDef columnDef in join.ObjectDef.ColumnDefs)
            {
                if (Joins.All(item => item.ParentColumnDef != columnDef))
                {
                    QueryColumns.Add(new QueryColumn(join, columnDef, "", true, true, true));
                }
            }

            foreach (Join subJoin in Joins.Where(item => item.ParentJoin == join))
            {
                SelectAll(subJoin);
            }
        }

        public Query Select(string identifier, bool isVisible = true, bool isWhere = true)
        {
            ColumnIdentifier columnIdentifier = new ColumnIdentifier(identifier);

            string name = columnIdentifier.Alias == "" ? columnIdentifier.Name : columnIdentifier.Alias;

            if (SelectColumns.Any(item => (item.Alias == "" && item.ColumnDef.Name == name) || item.Alias == name))
            {
                throw new DataAccessException(DataAccessExceptionType.DuplicateSelectColumn, "SelectColumn {0} already exists.", name);
            }

            JoinAndColumnDef joinAndColumnDef = GetSelectJoinAndColumnDef(columnIdentifier);

            QueryColumns.Add(new QueryColumn(joinAndColumnDef.Join, joinAndColumnDef.ColumnDef, columnIdentifier.Alias, isVisible, true, isWhere));

            return this;
        }

        internal JoinAndColumnDef GetSelectJoinAndColumnDef(ColumnIdentifier columnIdentifier)
        {
            /*
             * select columns are specified as a ColumnIdentifier
             * 
             * SchemaObjectName: SchemaName.ObjectName.ColumnName
             * ObjectName: ObjectName.ColumnName or ObjectAlias.ColumnName
             * or
             * Name: ColumnName
             * 
             * Get the Join and ColumnDef from the ColumnIdentifier
             */

            List<JoinAndColumnDef> joinAndColumDefs = new List<JoinAndColumnDef>();

            if ((columnIdentifier.ColumnIdentiferType & ColumnIdentiferType.SchemaObjectName) == ColumnIdentiferType.SchemaObjectName)
            {
                //chemaName.ObjectName.ColumnName
                foreach (Join j in Joins.Where(item => item.ObjectDef.SchemaDef.Name == columnIdentifier.Schema
                    && item.ObjectDef.Name == columnIdentifier.Object))
                {
                    ColumnDef cd = j.ObjectDef.ColumnDefs.SingleOrDefault(item => item.Name == columnIdentifier.Name);

                    if (cd != null)
                    {
                        joinAndColumDefs.Add(new JoinAndColumnDef(j, cd));
                    }
                }
            }
            else if ((columnIdentifier.ColumnIdentiferType & ColumnIdentiferType.SchemaObjectName) == ColumnIdentiferType.ObjectName)
            {
                //ObjectName.ColumnName
                foreach (Join j in Joins.Where(item => item.ObjectDef.Name == columnIdentifier.Object))
                {
                    ColumnDef cd = j.ObjectDef.ColumnDefs.SingleOrDefault(item => item.Name == columnIdentifier.Name);

                    if (cd != null)
                    {
                        joinAndColumDefs.Add(new JoinAndColumnDef(j, cd));
                    }
                }

                //ObjectAlias.ColumnName
                foreach (Join j in Joins.Where(item => item.Alias == columnIdentifier.Object))
                {
                    ColumnDef cd = j.ObjectDef.ColumnDefs.SingleOrDefault(item => item.Name == columnIdentifier.Name);

                    if (cd != null)
                    {
                        joinAndColumDefs.Add(new JoinAndColumnDef(j, cd));
                    }
                }
            }
            else if ((columnIdentifier.ColumnIdentiferType & ColumnIdentiferType.SchemaObjectName) == ColumnIdentiferType.Name)
            {
                //ColumnName
                foreach (Join j in Joins)
                {
                    ColumnDef cd = j.ObjectDef.ColumnDefs.SingleOrDefault(item => item.Name == columnIdentifier.Name);

                    if (cd != null)
                    {
                        joinAndColumDefs.Add(new JoinAndColumnDef(j, cd));
                    }
                }
            }

            if (joinAndColumDefs.Count == 1)
            {
                return joinAndColumDefs[0];
            }
            else if (joinAndColumDefs.Count(item => item.Join == Joins[0]) == 1)
            {

            }
            throw new Exception(String.Format("Unable to identify Column '{0}'", columnIdentifier));
        }

        internal class JoinAndColumnDef
        {
            public JoinAndColumnDef(Join join, ColumnDef columnDef)
            {
                Join = join;
                ColumnDef = columnDef;
            }

            public Join Join { get; set; }
            public ColumnDef ColumnDef { get; set; }
        }



        public Query Join(string @object)
        {
            ObjectIdentifier objectIdentifier = new ObjectIdentifier(@object);

            ObjectDef objectDef = GetObjectDef(objectIdentifier);

            //No column specified so join to the root object column that refences the specified object
            Join parentJoin = Joins[0];

            List<ColumnDef> parentColumnDefs = parentJoin.ObjectDef.ColumnDefs.Where(item => item.ReferencedObjectDef == objectDef).ToList();

            if (parentColumnDefs.Count != 1)
            {
                throw new Exception(String.Format("Failed to identify root object column to join to {0} with", @object));
            }

            ColumnDef columnDef = objectDef.ColumnDefs.SingleOrDefault(item => item.IsPrimaryKey);

            if (columnDef == null)
            {
                throw new Exception(String.Format("Failed to identify root object column to join to {0} with", @object));
            }

            Join(parentJoin, parentColumnDefs[0], objectDef, columnDef, objectIdentifier);

            return this;
        }

        public Query Join(string @object, string column)
        {
            ObjectIdentifier objectIdentifier = new ObjectIdentifier(@object);

            ColumnIdentifier columnIdentifier = new ColumnIdentifier(column);

            ObjectDef objectDef = GetObjectDef(objectIdentifier);

            List<Join> parentJoins = new List<Join>();

            if ((columnIdentifier.ColumnIdentiferType & ColumnIdentiferType.SchemaObjectName) == ColumnIdentiferType.Name)
            {
                parentJoins.Add(Joins[0]);
            }
            else if ((columnIdentifier.ColumnIdentiferType & ColumnIdentiferType.SchemaObjectName) == ColumnIdentiferType.ObjectName)
            {
                parentJoins.AddRange(Joins.Where(item => item.ObjectDef.Name == columnIdentifier.Object));

                parentJoins.AddRange(Joins.Where(item => item.Alias == columnIdentifier.Object));
            }
            else if ((columnIdentifier.ColumnIdentiferType & ColumnIdentiferType.SchemaObjectName) == ColumnIdentiferType.SchemaObjectName)
            {
                parentJoins.AddRange(Joins.Where(item => item.ObjectDef.SchemaDef.Name == columnIdentifier.Schema &&
                    item.ObjectDef.Name == columnIdentifier.Object));
            }

            if (parentJoins.Count != 1)
            {
                throw new Exception(String.Format("Failed to identify parent object join to with for {0}", column));
            }

            Join parentJoin = parentJoins[0];

            List<ColumnDef> parentColumnDefs = new List<ColumnDef>(parentJoin.ObjectDef.ColumnDefs.Where(item => item.Name == columnIdentifier.Name));

            if (parentColumnDefs.Count != 1)
            {
                throw new Exception(String.Format("Failed to identify root object column to join to {0} with", @object));
            }

            //No parent object specified so join to the specofied column on the root object 
            ColumnDef columnDef = objectDef.ColumnDefs.SingleOrDefault(item => item.IsPrimaryKey);

            if (columnDef == null)
            {
                throw new Exception(String.Format("Failed to identify root object column to join to {0} with", @object));
            }

            Join(parentJoin, parentColumnDefs[0], objectDef, columnDef, objectIdentifier);

            return this;
        }

        private void Join(Join parentJoin, ColumnDef parentColumnDef, ObjectDef objectDef, ColumnDef columnDef, ObjectIdentifier objectIdentifier)
        {
            Join join = new Join(parentJoin, parentColumnDef, objectDef, columnDef, objectIdentifier.Alias);

            SetJoinAlias(join);

            Joins.Add(join);
        }

        public string GetSelectStatement()
        {
            return GetSelect() + GetFrom() + GetWhere() + GetOrderBy();
        }

        private string GetSelect()
        {
            bool first = true;

            string select = "";

            int maxLength = SelectColumns.Where(item => item.IsVisible)
                .Select(item => String.Format("{0}.{1}", item.Join.InternalAlias, item.ColumnDef.Name))
                .Max(item => item.Length);

            foreach (QueryColumn queryColumn in SelectColumns)
            {
                queryColumn.InternalAlias = queryColumn.Alias;
            }

            foreach (var duplicateNames in SelectColumns.Where(item => item.Alias == "")
                .GroupBy(item => item.ColumnDef.Name, 
                        item => item.ColumnDef, 
                        (name, columnDefs) => new { Name = name, ColumnDefs = columnDefs })
                .Where(item => item.ColumnDefs.Count() > 1))
            {
                foreach (ColumnDef columnDef in duplicateNames.ColumnDefs)
                {
                    
                }
              


            }






            if (SelectColumns.Any(item => item.IsVisible))
            {
                foreach (QueryColumn queryColumn in QueryColumns.Where(item => item.IsVisible))
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

        private string MakeSafe(string value)
        {
            return _reserverdKeywords.Contains(value, StringComparer.OrdinalIgnoreCase) ? String.Format("[{0}]", value) : value;
        }

        internal ObjectDef GetObjectDef(ObjectIdentifier objectIdentifier)
        {
            return _databaseDef.ObjectDefs.Single(item => item.Name == objectIdentifier.Name
                   && (item.SchemaDef.Name == objectIdentifier.Schema || objectIdentifier.Schema == ""));
        }

        public IEnumerable<object> Execute()
        {
            //            string sql = GetSelectStatement();
            //
            //            DataSet dataSet = new DataSet();
            //
            //            using (SqlConnection connection = new SqlConnection(_databaseDef.Connection.ConnectonString))
            //            {
            //                connection.Open();
            //
            //                using (SqlCommand command = new SqlCommand(sql, connection))
            //                {
            //                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            //                    {
            //                        adapter.Fill(dataSet);
            //                    }
            //                }
            //            }
            //
            //            return dataSet.ToEnumerable();

            return null;
        }

    }

    public class Query<T> : Query
    {
        public Query(DatabaseDef databaseDef)
            : base(databaseDef)
        {
        }
    }
}