using System.Linq;
using MemeGenerator.DAL;
using MemeGenerator.DAL.ImageTemplateRepository;
using MemeGenerator.Domain.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace MemeGenerator.Tests.ImageTemplateRepositoryTests
{
    [TestFixture]
    class ImageTemplateRepositoryTests
    {
        [Test]
        public void GetAll_ImageTemplates_GetListOfImageTemplates()
        {
            var options = new DbContextOptionsBuilder<MemeGeneratorDbContext>()
                .UseInMemoryDatabase(databaseName: "getAll")
                .Options;

            using (var context = new MemeGeneratorDbContext(options))
            {
                var repository = new ImageTemplateRepository(context);

                context.ImageTemplates.Add(new ImageTemplate());
                context.SaveChanges();

                var imageTemplatesList = repository.GetAll();

                Assert.AreEqual(context.ImageTemplates.Count(), imageTemplatesList.Count());
            }
        }

        [Test]
        public void Insert_ImageTemplate_InsertImageIntoDb()
        {
            var options = new DbContextOptionsBuilder<MemeGeneratorDbContext>()
                .UseInMemoryDatabase(databaseName: "insert")
                .Options;

            using (var context = new MemeGeneratorDbContext(options))
            {
                var repository = new ImageTemplateRepository(context);

                repository.Insert(new ImageTemplate {Name = "insertImage"});
                context.SaveChanges();

                Assert.AreEqual("insertImage", context.ImageTemplates.Single().Name);
            }
        }

        [Test]
        public void Save_ImageTemplate_SaveChanges()
        {
            var options = new DbContextOptionsBuilder<MemeGeneratorDbContext>()
                .UseInMemoryDatabase(databaseName: "save")
                .Options;

            using (var context = new MemeGeneratorDbContext(options))
            {
                var repository = new ImageTemplateRepository(context);

                context.ImageTemplates.Add(new ImageTemplate());

                repository.Save();

                Assert.AreEqual(1, context.ImageTemplates.Count());
            }
        }
    }
}
