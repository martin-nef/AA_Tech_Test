using System;

namespace AA_Tech_Test.Exceptions
{
    public class InvalidReferenceException : Exception
    {
        public InvalidReferenceException() : base($"Reference returned by the form was invalid.") { }
    }
}
