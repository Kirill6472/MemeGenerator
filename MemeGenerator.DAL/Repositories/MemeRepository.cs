using System.Collections.Generic;
using System.Linq;
using MemeGenerator.Domain.Entities;
using MemeGenerator.Domain.Models;

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

        public int Count()
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

        public MemePage GetPage(PageRequest request)
        {
            var memes = _context.MemeImages.OrderBy(request.OrderBy).Skip(request.Skip).Take(request.PageSize);

            return new MemePage(memes);
        }
    }
}
