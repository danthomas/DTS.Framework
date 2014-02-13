using System;
using System.Collections.Generic;
using System.Linq;
using DTS.SqlServer.DataAccess.Definition;

namespace DTS.SqlServer.DataAccess
{
    public partial class Query
    {
        private readonly DatabaseDef _databaseDef;
        private bool _isDirty;
        private string _selectStatement;
        private IList<string> _validationErrors;

        public Query(DatabaseDef databaseDef)
        {
            _databaseDef = databaseDef;
            QueryColumns = new List<QueryColumn>();
            Joins = new List<Join>();
        }

        public string SelectStatement
        {
            get
            {
                if (_isDirty)
                {
                    Build();
                }

                return _selectStatement;
            }
        }

        public IEnumerable<string> ValidationErrors { get { return _validationErrors; } }

        public List<QueryColumn> QueryColumns { get; private set; }

        public List<Join> Joins { get; set; }

        public IEnumerable<QueryColumn> SelectColumns { get { return QueryColumns.Where(item => item.IsSelect); } }

        public Query From(string @object)
        {
            ObjectIdentifier objectIdentifier = new ObjectIdentifier(@object);

            Joins.Add(new Join(objectIdentifier, null));

            _isDirty = true;

            return this;
        }

        public Query Select(string identifier, bool isWhere = true, bool isVisible = true)
        {
            SelectColumnIdentifier selectColumnIdentifier = new SelectColumnIdentifier(identifier);

            QueryColumns.Add(new QueryColumn(selectColumnIdentifier, true, isWhere, isVisible));

            _isDirty = true;

            return this;

            //            string name = SelectColumnIdentifier.Alias == "" ? SelectColumnIdentifier.Name : SelectColumnIdentifier.Alias;
            //
            //            if (SelectColumns.Any(item => (item.Alias == "" && item.ColumnDef.Name == name) || item.Alias == name))
            //            {
            //                throw new DataAccessException(DataAccessExceptionType.DuplicateSelectColumn, "SelectColumn {0} already exists.", name);
            //            }
            //
            //            JoinAndColumnDef joinAndColumnDef = GetJoinAndColumnDef(SelectColumnIdentifier);
            //
            //            QueryColumns.Add(new QueryColumn(joinAndColumnDef.Join, joinAndColumnDef.ColumnDef, SelectColumnIdentifier.Alias, isWhere, true, isVisible));

        }

        internal JoinAndColumnDef GetJoinAndColumnDef(Join join)
        {
            List<JoinAndColumnDef> joinAndColumDefs = new List<JoinAndColumnDef>();

            List<ColumnDef> columnDefs = Joins[0].ObjectDef.ColumnDefs.Where(item => item.ReferencedObjectDef == join.ObjectDef).ToList();

            if (columnDefs.Count == 1)
            {
                return new JoinAndColumnDef(Joins[0], columnDefs[0]);
            }
            
            return null;
        }

        internal JoinAndColumnDef GetJoinAndColumnDef(SelectColumnIdentifier selectColumnIdentifier)
        {
            /*
             * select columns are specified as a SelectColumnIdentifier
             * 
             * SchemaObjectName: SchemaName.ObjectName.ColumnName
             * ObjectName: ObjectName.ColumnName or ObjectAlias.ColumnName
             * or
             * Name: ColumnName
             * 
             * Get the Join and ColumnDef from the SelectColumnIdentifier
             */

            List<JoinAndColumnDef> joinAndColumDefs = new List<JoinAndColumnDef>();

            if ((selectColumnIdentifier.IdentiferType & SelectColumnIdentiferType.SchemaObjectName) == SelectColumnIdentiferType.SchemaObjectName)
            {
                //chemaName.ObjectName.ColumnName
                foreach (Join j in Joins.Where(item => item.ObjectDef.SchemaDef.Name == selectColumnIdentifier.Schema
                    && item.ObjectDef.Name == selectColumnIdentifier.Object))
                {
                    ColumnDef cd = j.ObjectDef.ColumnDefs.SingleOrDefault(item => item.Name == selectColumnIdentifier.Name);

                    if (cd != null)
                    {
                        joinAndColumDefs.Add(new JoinAndColumnDef(j, cd));
                    }
                }
            }
            else if ((selectColumnIdentifier.IdentiferType & SelectColumnIdentiferType.SchemaObjectName) == SelectColumnIdentiferType.ObjectName)
            {
                //ObjectName.ColumnName
                foreach (Join j in Joins.Where(item => item.ObjectDef.Name == selectColumnIdentifier.Object))
                {
                    ColumnDef cd = j.ObjectDef.ColumnDefs.SingleOrDefault(item => item.Name == selectColumnIdentifier.Name);

                    if (cd != null)
                    {
                        joinAndColumDefs.Add(new JoinAndColumnDef(j, cd));
                    }
                }

                //ObjectAlias.ColumnName
                foreach (Join j in Joins.Where(item => item.Alias == selectColumnIdentifier.Object))
                {
                    ColumnDef cd = j.ObjectDef.ColumnDefs.SingleOrDefault(item => item.Name == selectColumnIdentifier.Name);

                    if (cd != null)
                    {
                        joinAndColumDefs.Add(new JoinAndColumnDef(j, cd));
                    }
                }
            }
            else if ((selectColumnIdentifier.IdentiferType & SelectColumnIdentiferType.SchemaObjectName) == SelectColumnIdentiferType.Name)
            {
                //ColumnName
                foreach (Join j in Joins)
                {
                    ColumnDef cd = j.ObjectDef.ColumnDefs.SingleOrDefault(item => item.Name == selectColumnIdentifier.Name);

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

            throw new Exception(String.Format("Unable to identify Column '{0}'", selectColumnIdentifier));
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

            Joins.Add(new Join(objectIdentifier, null));

            _isDirty = true;

            return this;

            //            ObjectDef objectDef = GetObjectDef(ObjectIdentifier);
            //
            //
            //            //No column specified so join to the root object column that refences the specified object
            //            Join parentJoin = Joins[0];
            //
            //            List<ColumnDef> parentColumnDefs = parentJoin.ObjectDef.ColumnDefs.Where(item => item.ReferencedObjectDef == objectDef).ToList();
            //
            //            if (parentColumnDefs.Count != 1)
            //            {
            //                throw new Exception(String.Format("Failed to identify root object column to join to {0} with", @object));
            //            }
            //
            //            ColumnDef columnDef = objectDef.ColumnDefs.SingleOrDefault(item => item.IsPrimaryKey);
            //
            //            if (columnDef == null)
            //            {
            //                throw new Exception(String.Format("Failed to identify root object column to join to {0} with", @object));
            //            }
            //
            //            Join(parentJoin, parentColumnDefs[0], objectDef, columnDef, ObjectIdentifier);
        }

        public Query Join(string @object, string column)
        {
            ObjectIdentifier objectIdentifier = new ObjectIdentifier(@object);

            SelectColumnIdentifier selectColumnIdentifier = new SelectColumnIdentifier(column);

            Joins.Add(new Join(objectIdentifier, selectColumnIdentifier));

            _isDirty = true;

            return this;
            //
            //            ObjectDef objectDef = GetObjectDef(ObjectIdentifier);
            //
            //            List<Join> parentJoins = new List<Join>();
            //
            //            if ((SelectColumnIdentifier.SelectColumnIdentiferType & SelectColumnIdentiferType.SchemaObjectName) == SelectColumnIdentiferType.Name)
            //            {
            //                parentJoins.Add(Joins[0]);
            //            }
            //            else if ((SelectColumnIdentifier.SelectColumnIdentiferType & SelectColumnIdentiferType.SchemaObjectName) == SelectColumnIdentiferType.ObjectName)
            //            {
            //                parentJoins.AddRange(Joins.Where(item => item.ObjectDef.Name == SelectColumnIdentifier.Object));
            //
            //                parentJoins.AddRange(Joins.Where(item => item.Alias == SelectColumnIdentifier.Object));
            //            }
            //            else if ((SelectColumnIdentifier.SelectColumnIdentiferType & SelectColumnIdentiferType.SchemaObjectName) == SelectColumnIdentiferType.SchemaObjectName)
            //            {
            //                parentJoins.AddRange(Joins.Where(item => item.ObjectDef.SchemaDef.Name == SelectColumnIdentifier.Schema &&
            //                    item.ObjectDef.Name == SelectColumnIdentifier.Object));
            //            }
            //
            //            if (parentJoins.Count != 1)
            //            {
            //                throw new Exception(String.Format("Failed to identify parent object join to with for {0}", column));
            //            }
            //
            //            Join parentJoin = parentJoins[0];
            //
            //            List<ColumnDef> parentColumnDefs = new List<ColumnDef>(parentJoin.ObjectDef.ColumnDefs.Where(item => item.Name == SelectColumnIdentifier.Name));
            //
            //            if (parentColumnDefs.Count != 1)
            //            {
            //                throw new Exception(String.Format("Failed to identify root object column to join to {0} with", @object));
            //            }
            //
            //            //No parent object specified so join to the specofied column on the root object 
            //            ColumnDef columnDef = objectDef.ColumnDefs.SingleOrDefault(item => item.IsPrimaryKey);
            //
            //            if (columnDef == null)
            //            {
            //                throw new Exception(String.Format("Failed to identify root object column to join to {0} with", @object));
            //            }

        }

        private string MakeSafe(string value)
        {
            return _reserverdKeywords.Contains(value, StringComparer.OrdinalIgnoreCase) ? String.Format("[{0}]", value) : value;
        }

        private void AddValidationError(string format, params object[] args)
        {
            _validationErrors.Add(String.Format(format, args));
        }

        internal ObjectDef GetObjectDef(ObjectIdentifier objectIdentifier)
        {
            ObjectDef objectDef = null;

            List<ObjectDef> objectDefs = _databaseDef.ObjectDefs.Where(item => item.Name == objectIdentifier.Name
                   && (objectIdentifier.Schema == "" || item.SchemaDef.Name == objectIdentifier.Schema)).ToList();

            switch (objectDefs.Count)
            {
                case 0:
                    AddValidationError("Failed to find object definition for IdentifierBase '{0}'", objectIdentifier);
                    break;
                case 1:
                    objectDef = objectDefs[0];
                    break;
                default:
                    AddValidationError("Multiple object definitions found for IdentifierBase '{0}'", objectIdentifier);
                    break;
            }

            return objectDef;
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