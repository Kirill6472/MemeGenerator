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

            var MemeGeneratorDbContext = new MemeGeneratorDbContext(options);

            var imageTemplates = new List<ImageTemplate>
            {
                new ImageTemplate()
                {
                    ImageTemplateId = 1,
                    Name = "name",
                    Folder = "folder",
                    Description = "description"
                }
            };

            MemeGeneratorDbContext.AddRange(imageTemplates);
            MemeGeneratorDbContext.SaveChanges();
        }
    }
}
