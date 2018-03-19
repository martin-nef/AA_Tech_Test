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
        public static Label ErrorMessage;
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
            if (ErrorMessage == null && root != null &&
                root.GetType() == typeof(Start))
            {
                ErrorMessage = ((Start)root).errorLabel;
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
