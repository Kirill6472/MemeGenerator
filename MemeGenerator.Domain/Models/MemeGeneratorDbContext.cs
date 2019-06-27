using Microsoft.EntityFrameworkCore;

namespace MemeGenerator.Domain.Models
{
    public class MemeGeneratorDbContext : DbContext
    {
        public MemeGeneratorDbContext(DbContextOptions<MemeGeneratorDbContext> options)
            : base(options)
        { }

        public DbSet<ImageTemplate> ImageTemplates { get; set; }
    }
}
