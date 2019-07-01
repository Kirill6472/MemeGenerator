using System.Collections.Generic;
using MemeGenerator.DAL;
using MemeGenerator.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MemeGenerator.Tests
{
    public class FakeDbContext
    {
        public MemeGeneratorDbContext MemeGeneratorDbContext { get; set; }

        public FakeDbContext()
        {
            var builder = new DbContextOptionsBuilder<MemeGeneratorDbContext>();
            builder.UseInMemoryDatabase();
            var options = builder.Options;

            MemeGeneratorDbContext = new MemeGeneratorDbContext(options);
        }
    }
}
