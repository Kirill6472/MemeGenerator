using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MemeGenerator.Models
{
    public class MemeGeneratorDbContext : DbContext
    {
        public MemeGeneratorDbContext(DbContextOptions<MemeGeneratorDbContext> options)
            : base(options)
        { }

        public DbSet<ImageTemplate> ImageTemplates { get; set; }
    }
}
