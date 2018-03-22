using AA_Tech_Test.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AA_Tech_Test.Utilities
{
    public static class DebugTools
    {
        private static object instanceLock = new object();
        private static Form Parent;
        public static DebugBox WinForm;
        public static RichTextBox DebugOutputRichTextBox;

        public static void Initialise(Start root)
        {
            if (Parent == null)
            {
                Parent = root;
            }
            if (WinForm == null)
            {
                WinForm = new DebugBox(root);
            }
            if (DebugOutputRichTextBox == null)
            {
                DebugOutputRichTextBox = WinForm.DebugOutputRichTextBox;
            }
        }

        public static void Log(string line)
        {
            Log(new string[] { line });
        }

        public static void Log(IEnumerable<string> lines)
        {
            if (DebugOutputRichTextBox == null)
            {
                if (WinForm == null)
                    throw new DebugFormUninitialisedException();

                DebugOutputRichTextBox = WinForm.DebugOutputRichTextBox;
            }

            DebugOutputRichTextBox.Lines = DebugOutputRichTextBox.Lines.Concat(lines).ToArray();
        }
    }
}
