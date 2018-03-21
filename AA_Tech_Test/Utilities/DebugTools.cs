using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AA_Tech_Test.Utilities
{
    public class DebugTools
    {

        public static DebugBox Form;
        public static RichTextBox Output;
        public static TextBox UserMessageBox;
        public DebugTools(Form root)
        {
            if (Form == null)
            {
                Form = new DebugBox(root);
            }
            if (Output == null)
            {
                Output = Form.debugOutput;
            }
            if (UserMessageBox == null && root != null &&
                root.GetType() == typeof(Start))
            {
                UserMessageBox = ((Start)root).userMessageBox;
            }
        }

        public static void Log(string line)
        {
            Log(new string[] { line });
        }

        public static void Log(IEnumerable<string> lines)
        {
            if (Output == null) return;
            Output.Lines = Output.Lines.Concat(lines).ToArray();
        }
    }
}
