using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using DTS.SqlServer.DataAccess.Definition;

namespace DTS.SqlServer.DataAccess.Designer
{
    public partial class Form1 : Form
    {
        private DatabaseDef _databaseDef;
        private bool _changing;

        public Form1()
        {
            InitializeComponent();

            if (server.Text != "" && database.Text != "")
            {
                LoadDefinitions();
            }
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            LayoutPanels();
        }

        private void Form1_LocationChanged(object sender, EventArgs e)
        {
            LayoutPanels();
        }

        private void LayoutPanels()
        {
            panel1.Width = panel2.Width = panel3.Width = Width / 4;
        }

        private void connect_Click(object sender, EventArgs e)
        {
            LoadDefinitions();
        }

        private void LoadDefinitions()
        {
            definition.Nodes.Clear();

            _databaseDef = new DatabaseDef();

            _databaseDef.Populate(server.Text, database.Text);

            foreach (ObjectDef objectDef in _databaseDef.ObjectDefs.OrderBy(item => item.SchemaDef.Name + "." + item.Name))
            {
                ObjectDefTreeNode objectDefTreeNode = new ObjectDefTreeNode(objectDef);

                definition.Nodes.Add(objectDefTreeNode);
            }
        }

        class ObjectDefTreeNode : TreeNode
        {
            public ObjectDef ObjectDef { get; set; }

            public ObjectDefTreeNode(ObjectDef objectDef)
            {
                ObjectDef = objectDef;
                Text = ObjectDef.SchemaDef.Name + "." + ObjectDef.Name;
            }
        }

        class ObjectDefListViewItem : ListViewItem
        {
            public ObjectDef ObjectDef { get; set; }

            public ObjectDefListViewItem(ObjectDef objectDef)
            {
                ObjectDef = objectDef;
                Text = ObjectDef.Name;
            }
        }

        private void dql_TextChanged(object sender, EventArgs e)
        {
            if (_changing)
            {
                return;
            }

            lookups.Items.Clear();

            if (_databaseDef != null &&
                editor.Lines.Length > 0 &&
                !editor.SelectedText.Contains(Environment.NewLine))
            {
                string text = editor.Lines[editor.GetLineFromCharIndex(editor.GetFirstCharIndexOfCurrentLine())];

                if (text != "")
                {
                    if (text.IndexOf('.') == -1)
                    {
                        lookups.Items.AddRange(_databaseDef.ObjectDefs
                            .Where(item => MatchName(text, item))
                            .Select(item => new ObjectDefListViewItem(item))
                            .OrderBy(item => item.Text)
                            .ToArray());
                    }
                    else if (text.IndexOf('.') == text.LastIndexOf('.')
                        && text.IndexOf('.') != text.Length - 1)
                    {
                        string[] parts = text.Split('.');

                        lookups.Items.AddRange(_databaseDef.ObjectDefs
                            .Where(item => item.SchemaDef.Name == parts[0] && MatchName(parts[1], item))
                            .Select(item => new ObjectDefListViewItem(item))
                            .OrderBy(item => item.Text)
                            .ToArray());
                    }
                }

                if (lookups.Items.Count > 0)
                {
                    lookups.Items[0].Selected = true;
                }

                lookups.Visible = lookups.Items.Count > 0;
            }
            else
            {
                lookups.Visible = false;
            }

            Build();
        }

        private bool MatchName(string text, ObjectDef objectDef)
        {
            if (objectDef.Name.StartsWith(text, true, null))
            {
                return true;
            }
            else if (Acronym(objectDef).StartsWith(text, true, null))
            {
                return true;
            }

            return false;
        }

        private string Acronym(ObjectDef objectDef)
        {
            return objectDef.Name.ToCharArray().Where(Char.IsUpper).Select(item => item.ToString(CultureInfo.InvariantCulture)).Aggregate((l, r) => l + r);
        }


        private void editor_KeyUp(object sender, KeyEventArgs e)
        {
            if (lookups.SelectedIndices.Count == 1 && lookups.Visible)
            {
                int index = lookups.SelectedIndices[0];
                if (e.KeyValue == 38 && index > 0)
                {
                    lookups.Items[index].Selected = false;
                    lookups.Items[index - 1].Selected = true;
                    e.Handled = true;
                }
                else if (e.KeyValue == 40 && index < lookups.Items.Count - 1)
                {
                    lookups.Items[index].Selected = false;
                    lookups.Items[index + 1].Selected = true;
                    e.Handled = true;
                }
            }
        }
        private void dql_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27) //Esc
            {
                lookups.Visible = false;
            }
            else if (e.KeyChar == '\t' && lookups.Visible && lookups.SelectedItems.Count == 1)
            {
                _changing = true;


                ObjectDef objectDef = ((ObjectDefListViewItem)lookups.SelectedItems[0]).ObjectDef;

                string objectName = objectDef.SchemaDef.Name + "." + objectDef.Name;

                int currentIndex = editor.GetFirstCharIndexOfCurrentLine();
                int currentLineIndex = editor.GetLineFromCharIndex(currentIndex);

                string[] lines = editor.Lines;

                lines[currentLineIndex] = objectName;

                editor.Lines = lines;

                editor.SelectionStart = currentIndex + objectName.Length;

                e.Handled = true;

                lookups.Visible = false;

                Build();

                _changing = false;
            }
        }

        private void build_Click(object sender, EventArgs e)
        {
            Build();
        }

        private void Build()
        {
            int emptyLineCount = 0;

            Query query = new Query(_databaseDef);

            foreach (string line in editor.Lines)
            {
                if (line.Trim() == "")
                {
                    emptyLineCount++;
                }
                else
                {
                    switch (emptyLineCount)
                    {
                        case 0:
                            query.Join(line);
                            break;
                    }
                }
            }

            sql.Text = query.SelectStatement;
        }
    }
}
