using System;
using System.Collections.Generic;

namespace DTS.SqlServer.DataAccess
{
    abstract internal class Identifier
    {
        private readonly string _identifier;

        protected List<string> Tokens;

        internal Identifier(string identifier)
        {
            _identifier = identifier;
            Tokenise(identifier);
        }

        private void Tokenise(string identifier)
        {
            Tokens = new List<string>();
            string token = "";

            bool squareBrackets = false;

            foreach (char c in identifier)
            {
                if (c == '[' && squareBrackets)
                {
                    throw new Exception("Invalid identifier");
                }

                if (c == ']' && !squareBrackets)
                {
                    throw new Exception("Invalid identifier");
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

        public override string ToString()
        {
            return _identifier;
        }

        protected string MakeSafe(string value)
        {
            return String.Format("[{0}]", value);
        }
    }
}