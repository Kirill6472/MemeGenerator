using System;

namespace MemeGenerator.DAL.Exceptions
{
    public class InitialMemesStorageStructureException : Exception
    {
        public InitialMemesStorageStructureException(string message) : base(message)
        {
        }
    }
}
