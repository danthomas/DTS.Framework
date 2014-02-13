using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DTS.SqlServer.DataAccess.Designer.Tests
{
    [TestClass]
    public class EditorTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            Editor editor = new Editor();

            editor.PressKey('a');
        }
    }

    internal class Editor : IEditor
    {
        private List<string> _lines;

        public Editor()
        {
            _lines = new List<string>();
        }

        public string Text
        {
            get { return _lines.Aggregate((a, b) => a + b); }
            set { _lines = value.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList(); }
        }

        public void PressKey(char keyChar)
        {
            KeyPress(this, new KeyPressEventArgs(keyChar));

        }

        public event KeyPressEventHandler KeyPress;
    }
}
