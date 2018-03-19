using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AA_Tech_Test.Exceptions
{
    class SpreadsheetEmptyException : Exception
    {
        public SpreadsheetEmptyException() : base("Spreadsheet provided was empty.") { }
    }
}
