using System;

namespace MemeGenerator.Infrastructure.Exceptions
{
    public class Base64ConverterException : Exception
    {
        public Base64ConverterException(string message) : base(message)
        {
        }
    }
}
