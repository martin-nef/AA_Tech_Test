using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AA_Tech_Test.Exceptions
{
    public class TypeMismatchException : Exception
    {
        public TypeMismatchException(Type expected, Type got) : 
            base($"Expected an object of type {expected}, but got a(n) {got} instead.") { }
        public TypeMismatchException(Type expected, Type got, Exception innerException) : 
            base(message: $"Expected an object of type {expected}, but got a(n) {got} instead.", innerException: innerException) { }
    }
}
