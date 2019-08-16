using System;

namespace MemeGenerator.Infrastructure.Exceptions
{
    public class InitialMemesStorageStructureException : Exception
    {
        public InitialMemesStorageStructureException(string message) : base(message)
        {
        }
    }
}
