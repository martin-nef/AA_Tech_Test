using System;

namespace AA_Tech_Test.Exceptions
{
    public class InvalidReferenceException : Exception
    {
        public InvalidReferenceException(string reference) : 
            base($"Reference retreived from the form was invalid: {reference}") { }
        public InvalidReferenceException(string example, string reference) : 
            base($"Reference retreived from the form was invalid: {reference}. Expected something of the form: {example}") { }
    }
}
