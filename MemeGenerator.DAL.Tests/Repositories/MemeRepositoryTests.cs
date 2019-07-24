using System.Linq;
using FluentAssertions;
using MemeGenerator.DAL.Repositories;
using MemeGenerator.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace MemeGenerator.DAL.Tests.Repositories
{
    [TestFixture]
    public class MemeRepositoryTests
    {
        private DbContextOptions<MemeGeneratorDbContext> _options;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<MemeGeneratorDbContext>()
                .UseInMemoryDatabase(databaseName: "memeGeneratorDb")
                .Options;
        }

        [Test]
        public void GetAll_MemeImages_ReturnAllMemeImages()
        {
            using (var context = new MemeGeneratorDbContext(_options))
            {
                var repository = new MemeRepository(context);
                context.MemeImages.Add(new MemeImage());
                context.SaveChanges();

                var memeImages = repository.GetAll();

                memeImages.Count().Should().Be(context.MemeImages.Count());
            }
        }

        [Test]
        public void Count_MemeImages_ReturnCountMemeImages()
        {
            using (var context = new MemeGeneratorDbContext(_options))
            {
                var repository = new MemeRepository(context);
                context.MemeImages.Add(new MemeImage());
                context.SaveChanges();

                var countMemeImages = repository.Count();

                countMemeImages.Should().Be(context.MemeImages.Count());
            }
        }

        [Test]
        public void Insert_MemeImage_InsertImageIntoDb()
        {
            const string fakeName = "name";

            using (var context = new MemeGeneratorDbContext(_options))
            {
                var repository = new MemeRepository(context);

                repository.Insert(new MemeImage { Name = fakeName });
                context.SaveChanges();

                context.MemeImages.Single().Name.Should().Be(fakeName);
            }
        }

        [Test]
        public void Save_MemeImages_SaveChanges()
        {
            using (var context = new MemeGeneratorDbContext(_options))
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
            using (var context = new MemeGeneratorDbContext(_options))
            {
                context.MemeImages.RemoveRange(context.MemeImages);
                context.SaveChanges();
            }
        }
    }
}
