using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AA_Tech_Test
{
    public partial class DebugBox : Form
    {
        Form Root;
        int maxLines = 100;

        public DebugBox(Form root)
        {
            InitializeComponent();
            Root = root;
        }

        private void debugOutput_TextChanged(object sender, EventArgs e)
        {
            if (DebugOutputRichTextBox.Lines.Count() >= maxLines)
            {
                DebugOutputRichTextBox.Lines = DebugOutputRichTextBox.Lines.Skip(1).ToArray();
            }
        }

        private void DebugBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Root != null)
            {
                Root.Show();
            }
        }

        private void debugOutput_Leave(object sender, EventArgs e)
        {
            if (Root != null)
            {
                Root.Show();
            }
        }
    }
}
