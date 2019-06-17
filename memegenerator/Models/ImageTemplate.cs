using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeGenerator.Models
{
    public class ImageTemplate
    {
        public int ImageTemplateId { get; set; }
        public string Folder { get; set; }
        public string Name { get; set; }
        public string Desctiption { get; set; }
    }
}
