using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AA_Tech_Test.Exceptions
{
    public class MyXmlException : XmlException
    {
        public string RawXmlDocument { get => xmlString; }
        private string xmlString;

        public MyXmlException(string message) : base(message: message) { }

        public MyXmlException(string message, XmlException innerException) : 
            base(message: message, innerException: innerException) { }

        public MyXmlException(string message, XmlException innerException, string rawXmlDocument) : 
            base(message: message, innerException: innerException)
        {
            xmlString = rawXmlDocument == null ? string.Empty : rawXmlDocument;
        }
    }
}
