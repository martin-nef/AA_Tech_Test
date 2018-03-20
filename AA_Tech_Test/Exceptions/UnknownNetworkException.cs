using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AA_Tech_Test.Exceptions
{
    public class UnknownNetworkException : WebException
    {
        public UnknownNetworkException() : 
            base("An unknown network has occured.") { }

        public UnknownNetworkException(string additonalInfo) : 
            base($"An network has occured. Additional information: {additonalInfo}") { }

        public UnknownNetworkException(string message, WebException innerException) : 
            base(message: message, innerException: innerException) { }
    }
}
