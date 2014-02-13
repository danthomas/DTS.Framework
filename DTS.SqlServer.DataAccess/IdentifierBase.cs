using System;
using System.Collections.Generic;

namespace DTS.SqlServer.DataAccess
{
    public abstract class IdentifierBase
    {
        private string _text;
        protected List<string> Tokens;

        internal IdentifierBase(string text)
        {
            Text = text;
        }

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                Tokenise();
                SetTokens();
                SetObjectIdentifierType();
            }
        }

        public abstract void SetTokens();

        public abstract void SetObjectIdentifierType();

        protected virtual void Tokenise()
        {
            Tokens = new List<string>();
            string token = "";

            bool squareBrackets = false;

            foreach (char c in _text)
            {
                if (c == '[' && squareBrackets)
                {
                    throw new Exception("Invalid _text");
                }

                if (c == ']' && !squareBrackets)
                {
                    throw new Exception("Invalid _text");
                }

                if (c == '[')
                {
                    squareBrackets = true;
                }
                else if (c == ']')
                {
                    squareBrackets = false;
                }
                else if (!squareBrackets && c == '.')
                {
                    Tokens.Add(token);
                    Tokens.Add(".");
                    token = "";
                }
                else if (!squareBrackets && c == ' ')
                {
                    Tokens.Add(token);
                    Tokens.Add(" ");
                    token = "";
                }
                else
                {
                    token += c;
                }
            }

            Tokens.Add(token);

        }

        protected string GetNamingToken(int minCount)
        {
            int i = 0;
            List<string> tokens = new List<string>();

            do
            {
                tokens.Insert(0, Tokens[i]);
                i += 2;
            } while (Tokens.Count >= i && Tokens[i - 1] == ".");

            return tokens.Count >= minCount ? tokens[minCount - 1] : "";
        }

        protected string GetAliasToken()
        {
            return Tokens.Count >= 3 && Tokens[Tokens.Count - 2] == " " ? Tokens[Tokens.Count - 1] : "";
        }
    }
}