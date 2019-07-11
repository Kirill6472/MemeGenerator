 using System.Collections.Generic;
using System.Linq;
using MemeGenerator.Domain.Entities;

namespace MemeGenerator.DAL.Repositories
{
    public class ImageTemplateRepository : IImageTemplateRepository
    {
        private readonly MemeGeneratorDbContext _context;

        public ImageTemplateRepository(MemeGeneratorDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ImageTemplate> GetAll()
        {
            return _context.ImageTemplates.ToList();
        }

        public void Insert(ImageTemplate imageTemplate)
        {
            _context.ImageTemplates.Add(imageTemplate);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
