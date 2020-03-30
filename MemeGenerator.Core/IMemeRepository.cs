using System.Collections.Generic;
using MemeGenerator.Core.Entities;
using MemeGenerator.Core.Models;

namespace MemeGenerator.Core
{
    public interface IMemeRepository
    {
        IEnumerable<MemeImage> GetAll();
        int Count();
        void Insert(MemeImage meme);
        void Save();
        IEnumerable<MemeImage> GetPage(PageRequest<MemeImage> request);
    }
}
