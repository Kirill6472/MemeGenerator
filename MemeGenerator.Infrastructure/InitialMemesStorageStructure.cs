using System.Collections.Generic;
using MemeGenerator.Domain.Entities;

namespace MemeGenerator.Infrastructure
{
    public class InitialMemesStorageStructure
    {
        public string Folder { get; set; }
        public List<MemeImage> MemeImages { get; set; }
    }
}
