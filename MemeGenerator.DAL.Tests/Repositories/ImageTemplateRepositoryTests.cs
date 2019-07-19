using System.Linq;
using FluentAssertions;
using MemeGenerator.DAL.Repositories;
using MemeGenerator.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace MemeGenerator.DAL.Tests.Repositories
{
    [TestFixture]
    public class ImageTemplateRepositoryTests
    {
        private DbContextOptions<MemeGeneratorDbContext> options;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<MemeGeneratorDbContext>()
                .UseInMemoryDatabase(databaseName: "memeGeneratorDb")
                .Options;
        }

        [Test]
        public void GetAll_MemeImages_ReturnAllMemeImages()
        {
            using (var context = new MemeGeneratorDbContext(options))
            {
                var repository = new MemeRepository(context);
                context.MemeImages.Add(new MemeImage());
                context.SaveChanges();

                var memeImages = repository.GetAll();

                memeImages.Count().Should().Be(context.MemeImages.Count());
            }
        }

        [Test]
        public void GetMemesCount_MemeImages_ReturnCountMemeImages()
        {
            using (var context = new MemeGeneratorDbContext(options))
            {
                var repository = new MemeRepository(context);
                context.MemeImages.Add(new MemeImage());
                context.SaveChanges();

                var countMemeImages = repository.GetMemesCount();

                countMemeImages.Should().Be(context.MemeImages.Count());
            }
        }

        [Test]
        public void Insert_ImageTemplate_InsertImageIntoDb()
        {
            using (var context = new MemeGeneratorDbContext(options))
            {
                var repository = new MemeRepository(context);

                repository.Insert(new MemeImage { Name = "insertImage" });
                context.SaveChanges();

                context.MemeImages.Single().Name.Should().Be("insertImage");
            }
        }

        [Test]
        public void Save_ImageTemplate_SaveChanges()
        {
            using (var context = new MemeGeneratorDbContext(options))
            {
                var repository = new MemeRepository(context);
                context.MemeImages.Add(new MemeImage());

                repository.Save();

                context.MemeImages.Count().Should().Be(1);
            }
        }

        [TearDown]
        public void TearDown()
        {
            using (var context = new MemeGeneratorDbContext(options))
            {
                context.MemeImages.RemoveRange(context.MemeImages);
                context.SaveChanges();
            }
        }
    }
}
