using System;

namespace DTS.SqlServer.DataAccess
{
    [Flags]
    public enum ObjectIdentiferType
    {
        Schema = 1,
        Name = 2,
        Alias = 4,
        SchemaName = Schema | Name,
        NameAlias = Name | Alias,
        SchemaNameAlias = Schema | Name | Alias
    }

    public class ObjectIdentifier : IdentifierBase
    {

        internal ObjectIdentifier(string text)
        {
            Text = text;
        }

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                Tokenise(_text);

                SetObjectIdentifierType();
            }
        }

        protected override void Tokenise(string _text)
        {
            base.Tokenise(_text);
                Schema = GetNamingToken(2);
                Name = GetNamingToken(1);
                Alias = GetAliasToken();

        }

        private void SetObjectIdentifierType()
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
                throw new Exception(String.Format("Invalid ObjectIdentifier {0}", _text));
            }
        }

        public string Schema { get; private set; }

        public string Name { get; private set; }

        public string Alias { get; private set; }

        public ObjectIdentiferType ObjectIdentiferType { get; set; }
    }
}
