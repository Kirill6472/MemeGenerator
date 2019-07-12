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
        public void GetAll_ImageTemplates_GetListOfImageTemplates()
        {
            using (var context = new MemeGeneratorDbContext(options))
            {
                var repository = new ImageTemplateRepository(context);
                context.ImageTemplates.Add(new ImageTemplate());
                context.SaveChanges();

                var imageTemplatesList = repository.GetAll();

                imageTemplatesList.Count().Should().Be(context.ImageTemplates.Count());
            }
        }

        [Test]
        public void Insert_ImageTemplate_InsertImageIntoDb()
        {
            using (var context = new MemeGeneratorDbContext(options))
            {
                var repository = new ImageTemplateRepository(context);

                repository.Insert(new ImageTemplate { Name = "insertImage" });
                context.SaveChanges();

                context.ImageTemplates.Single().Name.Should().Be("insertImage");
            }
        }

        [Test]
        public void Save_ImageTemplate_SaveChanges()
        {
            using (var context = new MemeGeneratorDbContext(options))
            {
                var repository = new ImageTemplateRepository(context);
                context.ImageTemplates.Add(new ImageTemplate());

                repository.Save();

                context.ImageTemplates.Count().Should().Be(1);
            }
        }

        [TearDown]
        public void TearDown()
        {
            using (var context = new MemeGeneratorDbContext(options))
            {
                context.ImageTemplates.RemoveRange(context.ImageTemplates);
                context.SaveChanges();
            }
        }
    }
}
