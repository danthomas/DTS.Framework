using System;
using System.Collections.Generic;
using System.Linq;
using DTS.SqlServer.DataAccess.Definition;

namespace DTS.SqlServer.DataAccess
{
    public class Query
    {
        private readonly DatabaseDef _databaseDef;

        public Query(DatabaseDef databaseDef, string @object)
        {
            _databaseDef = databaseDef;
            QueryColumns = new List<QueryColumn>();

            ObjectIdentifier objectIdentifier = new ObjectIdentifier(@object);

            ObjectDef objectDef = GetObjectDef(objectIdentifier);

            Join(null, null, objectDef, null, objectIdentifier);
        }

        public List<QueryColumn> QueryColumns { get; private set; }

        public List<Join> Joins { get; set; }

        private void Join(Join parentJoin, ColumnDef parentColumnDef, ObjectDef objectDef, ColumnDef columnDef, ObjectIdentifier objectIdentifier)
        {
            Join join = new Join(parentJoin, parentColumnDef, objectDef, columnDef, objectIdentifier.Alias);

            SetAliases(join);

            Joins.Add(join);
        }

        internal void SetAliases(Join join)
        {
            if (join.Alias != "" && Joins.Any(item => item != join && item.Alias == join.Alias))
            {
                throw new DataAccessException(DataAccessException.ExceptionType.AliasAlreadySpecified, "Alias {0} is already specified.", join.Alias);
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

        internal ObjectDef GetObjectDef(ObjectIdentifier objectIdentifier)
        {
            return _databaseDef.ObjectDefs.Single(item => item.Name == objectIdentifier.Name
                   && (item.SchemaDef.Name == objectIdentifier.Schema || objectIdentifier.Schema == ""));
        }
    }

    public class Query<T> : Query
    {
        public Query(DatabaseDef databaseDef, string @object)
            : base(databaseDef, @object)
        {
        }
    }
}
