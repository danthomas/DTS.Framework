using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTS.SqlServer.DataAccess.Definition;

namespace DTS.SqlServer.DataAccess.Designer
{
    public partial class Form1 : Form
    {
        private DatabaseDef _databaseDef;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            Layout();
        }

        private void Form1_LocationChanged(object sender, EventArgs e)
        {
            Layout();
        }

        private void Layout()
        {
            panel1.Width = panel2.Width = panel3.Width = Width / 4;
        }

        private void connect_Click(object sender, EventArgs e)
        {
            _databaseDef = new DatabaseDef();

            _databaseDef.Populate(server.Text, database.Text);

            foreach (ObjectDef objectDef in _databaseDef.ObjectDefs)
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
                Text = ObjectDef.Name;
            }
        }

        private void dql_TextChanged(object sender, EventArgs e)
        {
            lookup.Items.Clear();

            if (_databaseDef != null)
            {
                int i = dql.GetFirstCharIndexOfCurrentLine();

                int j = dql.Text.IndexOf(Environment.NewLine, i);

                j = j == -1 ? dql.Text.Length : j;

                string text = dql.Text.Substring(i, j - i);

                if (text != "")
                {
                    lookup.Items.AddRange(_databaseDef.ObjectDefs.Where(item => IsMatch(text, item)).Select(item => item.Name).ToArray());
                }

                if (lookup.Items.Count > 0)
                {
                    lookup.SelectedIndex = 0;
                }
            }
        }

        private bool IsMatch(string text, ObjectDef objectDef)
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
            return objectDef.Name.ToCharArray().Where(Char.IsUpper).Select(item => item.ToString(CultureInfo.InvariantCulture)).Aggregate(
                (l, r) => l + r);
        }

        private void dql_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\t')
            {

                int i = dql.GetFirstCharIndexOfCurrentLine();

                int j = dql.Text.IndexOf(Environment.NewLine, i);

                string text = dql.Text.Substring(0, i) + lookup.Items[0];

                j = j == -1 ? dql.Text.Length : j;

                dql.Text = text;

                e.Handled = true;
            }
        }
    }
}
