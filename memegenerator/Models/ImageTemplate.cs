using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeGenerator.Models
{
    public class ImageTemplate
    {
        public int ImageTemplateId { get; set; }
        public byte[] Image { get; set; }
    }
}
