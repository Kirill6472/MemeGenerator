using MemeGenerator.BLL.Services.InitialMemesPopulator;
using NUnit.Framework;
using FluentAssertions;
using MemeGenerator.DAL;
using MemeGenerator.DAL.ImageTemplateRepository;
using Moq;
using Microsoft.EntityFrameworkCore;
using MemeGenerator.Domain.Models;

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

            var mockSet = new Mock<DbSet<ImageTemplate>>();

            var mockContext = new Mock<MemeGeneratorDbContext>();
            mockContext.Setup(m => m.ImageTemplates).Returns(mockSet.Object);

            var mockRepository = new ImageTemplateRepository(mockContext.Object);

            InitialMemesPopulator initialMemesPopulator =
                new InitialMemesPopulator(stubInitialMemesProvider, mockRepository, stubMigrationsChecker);

            initialMemesPopulator.Initialize();
        }
    }
}