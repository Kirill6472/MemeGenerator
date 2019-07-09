using System.Collections.Generic;
using MemeGenerator.Domain.Entities;

namespace MemeGenerator.DAL
{
    public class ImageTemplateList
    {
        public string Folder { get; set; }
        public List<ImageTemplate> ImageTemplate { get; set; }
    }
}
