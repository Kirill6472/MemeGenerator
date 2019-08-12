using System.Collections.Generic;
using MemeGenerator.Domain.Entities;

namespace MemeGenerator.Domain.Models
{
    public class MemePage
    {
        public MemePage(IEnumerable<MemeImage> memes)
        {
            Memes = memes;
        }

        public IEnumerable<MemeImage> Memes { get; set; }
    }
}
