﻿using System;
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
            ColumnIdentifier columnIdentifier = new ColumnIdentifier(identifier);

            QueryColumns.Add(new QueryColumn(columnIdentifier, true, isWhere, isVisible));

            _isDirty = true;

            return this;

            //            string name = columnIdentifier.Alias == "" ? columnIdentifier.Name : columnIdentifier.Alias;
            //
            //            if (SelectColumns.Any(item => (item.Alias == "" && item.ColumnDef.Name == name) || item.Alias == name))
            //            {
            //                throw new DataAccessException(DataAccessExceptionType.DuplicateSelectColumn, "SelectColumn {0} already exists.", name);
            //            }
            //
            //            JoinAndColumnDef joinAndColumnDef = GetSelectJoinAndColumnDef(columnIdentifier);
            //
            //            QueryColumns.Add(new QueryColumn(joinAndColumnDef.Join, joinAndColumnDef.ColumnDef, columnIdentifier.Alias, isWhere, true, isVisible));

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

            Joins.Add(new Join(objectIdentifier, null));

            _isDirty = true;

            return this;

            //            ObjectDef objectDef = GetObjectDef(objectIdentifier);
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
            //            Join(parentJoin, parentColumnDefs[0], objectDef, columnDef, objectIdentifier);
        }

        public Query Join(string @object, string column)
        {
            ObjectIdentifier objectIdentifier = new ObjectIdentifier(@object);

            ColumnIdentifier columnIdentifier = new ColumnIdentifier(column);

            Joins.Add(new Join(objectIdentifier, columnIdentifier));

            _isDirty = true;

            return this;
            //
            //            ObjectDef objectDef = GetObjectDef(objectIdentifier);
            //
            //            List<Join> parentJoins = new List<Join>();
            //
            //            if ((columnIdentifier.ColumnIdentiferType & ColumnIdentiferType.SchemaObjectName) == ColumnIdentiferType.Name)
            //            {
            //                parentJoins.Add(Joins[0]);
            //            }
            //            else if ((columnIdentifier.ColumnIdentiferType & ColumnIdentiferType.SchemaObjectName) == ColumnIdentiferType.ObjectName)
            //            {
            //                parentJoins.AddRange(Joins.Where(item => item.ObjectDef.Name == columnIdentifier.Object));
            //
            //                parentJoins.AddRange(Joins.Where(item => item.Alias == columnIdentifier.Object));
            //            }
            //            else if ((columnIdentifier.ColumnIdentiferType & ColumnIdentiferType.SchemaObjectName) == ColumnIdentiferType.SchemaObjectName)
            //            {
            //                parentJoins.AddRange(Joins.Where(item => item.ObjectDef.SchemaDef.Name == columnIdentifier.Schema &&
            //                    item.ObjectDef.Name == columnIdentifier.Object));
            //            }
            //
            //            if (parentJoins.Count != 1)
            //            {
            //                throw new Exception(String.Format("Failed to identify parent object join to with for {0}", column));
            //            }
            //
            //            Join parentJoin = parentJoins[0];
            //
            //            List<ColumnDef> parentColumnDefs = new List<ColumnDef>(parentJoin.ObjectDef.ColumnDefs.Where(item => item.Name == columnIdentifier.Name));
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
                    AddValidationError("Failed to find object definition for identifier '{0}'", objectIdentifier);
                    break;
                case 1:
                    objectDef = objectDefs[0];
                    break;
                default:
                    AddValidationError("Multiple object definitions found for identifier '{0}'", objectIdentifier);
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