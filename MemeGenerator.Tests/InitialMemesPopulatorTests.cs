using System.Linq;
using MemeGenerator.BLL.Services.InitialMemesPopulator;
using NUnit.Framework;
using MemeGenerator.DAL;
using MemeGenerator.DAL.ImageTemplateRepository;
using Microsoft.EntityFrameworkCore;

namespace MemeGenerator.Tests
{
    [TestFixture]
    public class InitialMemesPopulatorTests
    {
        [Test]
        public void Initialize_DataProviderAndRepository_filledDb()
        {
            StubInitialMemesProvider stubInitialMemesProvider = new StubInitialMemesProvider();
            StubMigrationsChecker stubMigrationsChecker = new StubMigrationsChecker
            {
                ShouldAllMigrationsBeApplied = true
            };

            var options = new DbContextOptionsBuilder<MemeGeneratorDbContext>()
                .UseInMemoryDatabase(databaseName: "initializeDb")
                .Options;

            using (var context = new MemeGeneratorDbContext(options))
            {
                var mockRepository = new ImageTemplateRepository(context);

                InitialMemesPopulator initialMemesPopulator =
                    new InitialMemesPopulator(stubInitialMemesProvider, mockRepository, stubMigrationsChecker);

                Assert.AreEqual(0, context.ImageTemplates.Count());

                initialMemesPopulator.Initialize();

                Assert.AreEqual(1, context.ImageTemplates.Count());
            }
        }
    }
}