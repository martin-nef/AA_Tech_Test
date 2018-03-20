using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AA_Tech_Test.Exceptions
{
    public class UnknownException : Exception
    {
        public UnknownException() : 
            base("An unknown error has occured.") { }

        public UnknownException(string additonalInfo) : 
            base("An unknown error has occured. Additional information: " + additonalInfo) { }

        public UnknownException(string message, Exception innerException) : 
            base(message: message, innerException: innerException) { }
    }
}
