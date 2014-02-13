using System;

namespace DTS.SqlServer.DataAccess
{
    public class SelectColumnIdentifier : IdentifierBase
    {
        private string _text;

        internal SelectColumnIdentifier(string text)
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
                Schema = GetNamingToken(3);
                Object = GetNamingToken(2);
                Name = GetNamingToken(1);
                Alias = GetAliasToken();

                SetColumnIdentifierType();
            }
        }

        private void SetColumnIdentifierType()
        {
            if (Schema != "" && Object != "" && Name != "" && Alias != "")
            {
                IdentiferType = SelectColumnIdentiferType.SchemaObjectNameAlias;
            }
            else if (Schema == "" && Object != "" && Name != "" && Alias != "")
            {
                IdentiferType = SelectColumnIdentiferType.ObjectNameAlias;
            }
            else if (Schema == "" && Object == "" && Name != "" && Alias != "")
            {
                IdentiferType = SelectColumnIdentiferType.NameAlias;
            }
            else if (Schema != "" && Object != "" && Name != "" && Alias == "")
            {
                IdentiferType = SelectColumnIdentiferType.SchemaObjectName;
            }
            else if (Schema == "" && Object != "" && Name != "" && Alias == "")
            {
                IdentiferType = SelectColumnIdentiferType.ObjectName;
            }
            else if (Schema == "" && Object == "" && Name != "" && Alias == "")
            {
                IdentiferType = SelectColumnIdentiferType.Name;
            }
            else
            {
                throw new Exception(String.Format("Invalid SelectColumnIdentifier {0}", _text));
            }
        }

        public string Schema { get; private set; }

        public string Object { get; private set; }

        public string Name { get; private set; }

        public string Alias { get; private set; }

        public SelectColumnIdentiferType IdentiferType { get; set; }
    }
}
