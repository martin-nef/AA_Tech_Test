using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AA_Tech_Test.Exceptions
{
    public class DebugFormUninitialisedException : Exception
    {
        public DebugFormUninitialisedException() 
            : base(@"The DebugForms.Form was accessed before it was initialised.")
        {
        }
    }
}
