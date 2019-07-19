using System.Collections.Generic;
using System.Linq;
using MemeGenerator.Domain.Entities;

namespace MemeGenerator.DAL.Repositories
{
    public class MemeRepository : IMemeRepository
    {
        private readonly MemeGeneratorDbContext _context;

        public MemeRepository(MemeGeneratorDbContext context)
        {
            _context = context;
        }

        public IEnumerable<MemeImage> GetAll()
        {
            return _context.MemeImages.ToList();
        }

        public int GetMemesCount()
        {
            return _context.MemeImages.Count();
        }

        public void Insert(MemeImage imageTemplate)
        {
            _context.MemeImages.Add(imageTemplate);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
