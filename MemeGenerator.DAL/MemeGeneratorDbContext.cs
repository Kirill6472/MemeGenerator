using MemeGenerator.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MemeGenerator.DAL
{
    public class MemeGeneratorDbContext : DbContext
    {
        public MemeGeneratorDbContext(DbContextOptions<MemeGeneratorDbContext> options)
            : base(options)
        { }

        public DbSet<ImageTemplate> ImageTemplates { get; set; }
    }
}
