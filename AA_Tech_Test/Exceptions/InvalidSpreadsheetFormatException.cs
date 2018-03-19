using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AA_Tech_Test.Exceptions
{
    class InvalidSpreadsheetFormatException : Exception
    {
        public InvalidSpreadsheetFormatException() 
            : base("The spreadsheet didn't contain enough data. " +
                  "Expected file with 4 rows, with a contact name, " +
                  "email, subject, and message in their respective first cells.")
        {
        }
    }
}
