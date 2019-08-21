using MemeGenerator.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MemeGenerator.DAL
{
    public class MemeGeneratorDbContext : IdentityDbContext
    {
        public MemeGeneratorDbContext(DbContextOptions<MemeGeneratorDbContext> options)
            : base(options)
        { }

        public DbSet<MemeImage> MemeImages { get; set; }
    }
}
