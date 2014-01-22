using System;

namespace DTS.SqlServer.DataAccess
{
    [Flags]
    internal enum ObjectIdentiferType
    {
        Schema = 1,
        Name = 2,
        Alias = 4,
        SchemaName = Schema | Name,
        NameAlias = Name | Alias,
        SchemaNameAlias = Schema | Name | Alias
    }

    internal class ObjectIdentifier : Identifier
    {
        internal ObjectIdentifier(string identifier)
            : base(identifier)
        {
            Schema = GetNamingToken(2);
            Name = GetNamingToken(1);
            Alias = GetAliasToken();

            SetObjectIdentifierType(identifier);
        }

        private void SetObjectIdentifierType(string identifier)
        {
            if (Schema != "" && Name != "" && Alias != "")
            {
                ObjectIdentiferType = ObjectIdentiferType.SchemaNameAlias;
            }
            else if (Schema == "" && Name != "" && Alias != "")
            {
                ObjectIdentiferType = ObjectIdentiferType.NameAlias;
            }
            else if (Schema != "" && Name != "" && Alias == "")
            {
                ObjectIdentiferType = ObjectIdentiferType.SchemaName;
            }
            else if (Schema == "" && Name != "" && Alias == "")
            {
                ObjectIdentiferType = ObjectIdentiferType.Name;
            }
            else
            {
                throw new Exception(String.Format("Invalid ObjectIdentifier {0}", identifier));
            }
        }

        public string Schema { get; private set; }

        public string Name { get; private set; }

        public string Alias { get; private set; }

        public ObjectIdentiferType ObjectIdentiferType { get; set; }
    }
}
