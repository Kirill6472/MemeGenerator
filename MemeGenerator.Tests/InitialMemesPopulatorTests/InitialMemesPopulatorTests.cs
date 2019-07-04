using System.Linq;
using MemeGenerator.BLL.Services.InitialMemesPopulator;
using MemeGenerator.DAL;
using MemeGenerator.DAL.ImageTemplateRepository;
using MemeGenerator.Domain.Models;
using MemeGenerator.Tests.InitialMemesProviderTests;
using MemeGenerator.Tests.MigrationsCheckerTests;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace MemeGenerator.Tests.InitialMemesPopulatorTests
{
    [TestFixture]
    public class InitialMemesPopulatorTests
    {
        private StubInitialMemesProvider stubInitialMemesProvider;

        [SetUp]
        public void Setup()
        {
            stubInitialMemesProvider = new StubInitialMemesProvider();
        }

        [Test]
        public void Initialize_AllMigrationsApplyAndImageTableEmpty_filledDb()
        {
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

                initialMemesPopulator.Initialize();

                Assert.AreEqual(1, context.ImageTemplates.Count());
            }
        }

        [Test]
        public void Initialize_NotAllMigrationsAreApplied_DbNotFilled()
        {
            StubMigrationsChecker stubMigrationsChecker = new StubMigrationsChecker
            {
                ShouldAllMigrationsBeApplied = false
            };

            var options = new DbContextOptionsBuilder<MemeGeneratorDbContext>()
                .UseInMemoryDatabase(databaseName: "DbNotFilled")
                .Options;

            using (var context = new MemeGeneratorDbContext(options))
            {
                var mockRepository = new ImageTemplateRepository(context);

                InitialMemesPopulator initialMemesPopulator =
                    new InitialMemesPopulator(stubInitialMemesProvider, mockRepository, stubMigrationsChecker);

                initialMemesPopulator.Initialize();

                Assert.AreEqual(0, context.ImageTemplates.Count());
            }
        }

        [Test]
        public void Initialize_ImageTableNotEmpty_DbNotChanged()
        {
            StubMigrationsChecker stubMigrationsChecker = new StubMigrationsChecker
            {
                ShouldAllMigrationsBeApplied = true
            };

            var options = new DbContextOptionsBuilder<MemeGeneratorDbContext>()
                .UseInMemoryDatabase(databaseName: "imageTableNotEmpty")
                .Options;

            using (var context = new MemeGeneratorDbContext(options))
            {
                var mockRepository = new ImageTemplateRepository(context);

                mockRepository.Insert(new ImageTemplate());
                mockRepository.Save();

                InitialMemesPopulator initialMemesPopulator =
                    new InitialMemesPopulator(stubInitialMemesProvider, mockRepository, stubMigrationsChecker);

                Assert.AreEqual(1, context.ImageTemplates.Count());

                initialMemesPopulator.Initialize();

                Assert.AreEqual(1, context.ImageTemplates.Count());
            }
        }
    }
}