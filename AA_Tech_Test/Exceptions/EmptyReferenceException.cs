using System;

namespace AA_Tech_Test.Exceptions
{
    public class EmptyReferenceException : Exception
    {
        public EmptyReferenceException() : base("Form returned an empty reference.") { }
    }
}
