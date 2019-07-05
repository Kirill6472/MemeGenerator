using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using MemeGenerator.BLL.Services.InitialMemesPopulator;
using MemeGenerator.DAL;
using MemeGenerator.DAL.MigrationsChecker;
using MemeGenerator.DAL.Providers;
using MemeGenerator.DAL.Repositories;
using MemeGenerator.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace MemeGenerator.Tests.ServicesTests
{
    [TestFixture]
    public class InitialMemesPopulatorTests
    {
        private Mock<IInitialMemesProvider> mockInitialMemesProvider;
        private DbContextOptions<MemeGeneratorDbContext> options;

        [SetUp]
        public void Setup()
        {
            var image = new ImageTemplateList
            {
                Folder = "folder",
                ImageTemplate = new List<ImageTemplate>
                {
                    new ImageTemplate()
                    {
                        Name = "name",
                        Folder = "folder",
                        Description = "description",
                    }
                }
            };

            mockInitialMemesProvider = new Mock<IInitialMemesProvider>();
            mockInitialMemesProvider.Setup(m => m.GetDataFromJson()).Returns(image);

            options = new DbContextOptionsBuilder<MemeGeneratorDbContext>()
                .UseInMemoryDatabase(databaseName: "memeGeneratorDb")
                .Options;
        }

        [Test]
        public void Initialize_AllMigrationsApplyAndImageTableEmpty_filledDb()
        {
            var mockMigrationsChecker = new Mock<IMigrationsChecker>();
            mockMigrationsChecker.Setup(m => m.DoAllMigrationsApply()).Returns(true);

            using (var context = new MemeGeneratorDbContext(options))
            {
                var mockRepository = new ImageTemplateRepository(context);

                InitialMemesPopulator initialMemesPopulator =
                    new InitialMemesPopulator(mockInitialMemesProvider.Object, mockRepository, mockMigrationsChecker.Object);

                initialMemesPopulator.Initialize();

                context.ImageTemplates.Count().Should().Be(1);
            }
        }

        [Test]
        public void Initialize_NotAllMigrationsAreApplied_DbNotFilled()
        {
            var mockMigrationsChecker = new Mock<IMigrationsChecker>();
            mockMigrationsChecker.Setup(m => m.DoAllMigrationsApply()).Returns(false);

            using (var context = new MemeGeneratorDbContext(options))
            {
                var mockRepository = new ImageTemplateRepository(context);

                InitialMemesPopulator initialMemesPopulator =
                    new InitialMemesPopulator(mockInitialMemesProvider.Object, mockRepository, mockMigrationsChecker.Object);

                initialMemesPopulator.Initialize();

                context.ImageTemplates.Count().Should().Be(0);
            }
        }

        [Test]
        public void Initialize_ImageTableNotEmpty_DbNotChanged()
        {
            var mockMigrationsChecker = new Mock<IMigrationsChecker>();
            mockMigrationsChecker.Setup(m => m.DoAllMigrationsApply()).Returns(true);

            using (var context = new MemeGeneratorDbContext(options))
            {
                var mockRepository = new ImageTemplateRepository(context);

                mockRepository.Insert(new ImageTemplate());
                mockRepository.Save();

                InitialMemesPopulator initialMemesPopulator =
                    new InitialMemesPopulator(mockInitialMemesProvider.Object, mockRepository, mockMigrationsChecker.Object);

                context.ImageTemplates.Count().Should().Be(1);

                initialMemesPopulator.Initialize();

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