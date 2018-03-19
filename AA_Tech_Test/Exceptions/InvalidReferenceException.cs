using System;

namespace AA_Tech_Test.Exceptions
{
    public class InvalidReferenceException : Exception
    {
        private const string EXAMPLE_REFERENCE = "1234567";
        public InvalidReferenceException(string reference) : base($"Reference returned from the form was invalid (Was supposed to be of the form \"{EXAMPLE_REFERENCE}\", but received \"{reference}\").") { }
    }
}
