using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTS.SqlServer.DataAccess.Definition;

namespace DTS.SqlServer.DataAccess
{
    public class Query2
    {
        private readonly DatabaseDef _databaseDef;

        public Query2(DatabaseDef databaseDef)
        {
            _databaseDef = databaseDef;
            Lines = new List<Line>();
        }

        public List<Line> Lines { get; private set; }

        public string Text
        {
            get
            {
                return Lines.Count == 0 ? "" : Lines.Select(item => item.CalculatedText).Aggregate((a, b) => a + b);
            }
        }

        public void AddLine(int index, string text)
        {
            if (text.Trim() == "")
            {
                Lines.Insert(index, new BlankLine(this, text));
            }
            else
            {
                int noBlankLines = Lines.Take(index).Count(item => item.LineType == LineType.Blank);

                switch (noBlankLines)
                {
                    case 0:
                        Lines.Insert(index, new FromLine(this, text));
                        break;
                    case 1:
                        Lines.Insert(index, new SelectLine(this, text));
                        break;
                }
            }
        }

        public void GetObjectDefs(ObjectIdentifier objectIdentifier)
        {
            if (objectIdentifier.ObjectIdentiferType == ObjectIdentiferType.Name)
            {
              ObjectDefs = _databaseDef.ObjectDefs.Where(item => item.Name.StartsWith(objectIdentifier.Name, true, null)).ToList();
            }
            else if (objectIdentifier.ObjectIdentiferType == ObjectIdentiferType.Name)
            {
                ObjectDefs = _databaseDef.ObjectDefs.Where(item => item.SchemaDef.Name == objectIdentifier.Schema &&
                    item.Name.StartsWith(objectIdentifier.Name, true, null)).ToList();
            }
        }

        public List<ObjectDef> ObjectDefs { get; set; }

        public void Clear()
        {
            Lines.Clear();
        }
    }

    public enum LineType
    {
        Blank,
        From,
        Select,
        Where,
        GroupBy,
        OrderBy
    }

    public abstract class Line
    {
        protected readonly Query2 _query;

        protected Line(Query2 query, string text)
        {
            Text = text;
            _query = query;
        }

        public abstract string CalculatedText { get; }

        public abstract LineType LineType { get; }

        public string Text { get; set; }
    }

    public class BlankLine : Line
    {
        public BlankLine(Query2 query, string text)
            : base(query, text)
        { }

        public override string CalculatedText { get { return ""; } }

        public override LineType LineType { get { return LineType.Blank; } }
    }

    public class FromLine : Line
    {
        public FromLine(Query2 query, string text)
            : base(query, text)
        {
            ObjectIdentifier = new ObjectIdentifier(text);
            query.GetObjectDefs(ObjectIdentifier);
        }

        public ObjectIdentifier ObjectIdentifier { get; set; }

        public ObjectDef Object { get; set; }

        public ColumnDef Column { get; set; }

        public string Alias { get; set; }

        public string CalculatedAlias { get; set; }

        public override string CalculatedText { get { return Object == null ? "" : Object.SchemaDef.Name + "." + Object.Name + " " + CalculatedAlias; } }

        public override LineType LineType { get { return LineType.From; } }
    }

    public class SelectLine : Line
    {
        public SelectLine(Query2 query, string text)
            : base(query, text)
        { }

        public ObjectDef Object { get; set; }

        public ColumnDef Column { get; set; }

        public string Alias { get; set; }

        public override string CalculatedText { get { return Object.Name; } }

        public override LineType LineType { get { return LineType.Select; } }
    }
}
