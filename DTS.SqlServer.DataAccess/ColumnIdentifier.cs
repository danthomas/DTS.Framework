using System;

namespace DTS.SqlServer.DataAccess
{
    [Flags]
    internal enum ColumnIdentiferType
    {
        Schema = 1,
        Object = 2,
        Name = 4,
        Alias = 8,
        ObjectName = Object | Name,
        SchemaObjectName = Schema | Object | Name,
        NameAlias = Name | Alias,
        ObjectNameAlias = Object | Name | Alias,
        SchemaObjectNameAlias = Schema | Object | Name | Alias
    }

    internal class ColumnIdentifier : Identifier
    {
        internal ColumnIdentifier(string identifier)
            : base(identifier)
        {
            Schema = GetNamingToken(3);
            Object = GetNamingToken(2);
            Name = GetNamingToken(1);
            Alias = GetAliasToken();

            SetColumnIdentifierType(identifier);
        }

        private void SetColumnIdentifierType(string identifier)
        {
            if (Schema != "" && Object != "" && Name != "" && Alias != "")
            {
                ColumnIdentiferType = ColumnIdentiferType.SchemaObjectNameAlias;
            }
            else if (Schema == "" && Object != "" && Name != "" && Alias != "")
            {
                ColumnIdentiferType = ColumnIdentiferType.ObjectNameAlias;
            }
            else if (Schema == "" && Object == "" && Name != "" && Alias != "")
            {
                ColumnIdentiferType = ColumnIdentiferType.NameAlias;
            }
            else if (Schema != "" && Object != "" && Name != "" && Alias == "")
            {
                ColumnIdentiferType = ColumnIdentiferType.SchemaObjectName;
            }
            else if (Schema == "" && Object != "" && Name != "" && Alias == "")
            {
                ColumnIdentiferType = ColumnIdentiferType.ObjectName;
            }
            else if (Schema == "" && Object == "" && Name != "" && Alias == "")
            {
                ColumnIdentiferType = ColumnIdentiferType.Name;
            }
            else
            {
                throw new Exception(String.Format("Invalid ColumnIdentifier {0}", identifier));
            }
        }

        public string Schema { get; private set; }

        public string Object { get; private set; }

        public string Name { get; private set; }

        public string Alias { get; private set; }

        public ColumnIdentiferType ColumnIdentiferType { get; set; }
    }
}
