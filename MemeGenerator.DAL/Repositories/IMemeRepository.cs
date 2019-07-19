using System.Collections.Generic;
using MemeGenerator.Domain.Entities;

namespace MemeGenerator.DAL.Repositories
{
    public interface IMemeRepository
    {
        IEnumerable<MemeImage> GetAll();
        int GetMemesCount();
        void Insert(MemeImage meme);
        void Save();
    }
}
