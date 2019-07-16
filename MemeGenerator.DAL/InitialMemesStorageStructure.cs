using System.Collections.Generic;
using MemeGenerator.Domain.Entities;

namespace MemeGenerator.DAL
{
    public class InitialMemesStorageStructure
    {
        public string Folder { get; set; }
        public List<ImageTemplate> ImageTemplate { get; set; }
    }
}
