using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AA_Tech_Test.Exceptions
{
    class NoWriteAccessException : Exception
    {
        public NoWriteAccessException() : base("Couldn't open file with write permissions.") { }
    }
}
