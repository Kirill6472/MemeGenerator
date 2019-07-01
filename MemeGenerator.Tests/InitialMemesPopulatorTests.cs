using Autofac.Extras.Moq;
using MemeGenerator.BLL.Services.InitialMemesPopulator;
using MemeGenerator.DAL.ImageTemplateRepository;
using NUnit.Framework;

namespace MemeGenerator.Tests
{
    [TestFixture]
    public class InitialMemesPopulatorTests
    {
        private InitialMemesPopulatorTests initialMemesPopulatorTests = null;

        [Test]
        public void Initialize_ObtainedDataAndDbContext_filledDb()
        {
            StubInitialMemesProvider stubInitialMemesProvider = new StubInitialMemesProvider();
            StubMigrationsChecker stubMigrationsChecker = new StubMigrationsChecker
            {
                ShouldAllMigrationsBeApplied = true
            };

            using (var mock = AutoMock.GetLoose())
            {
                var mockRepository = mock.Create<ImageTemplateRepository>();

                InitialMemesPopulator initialMemesPopulator =
                    new InitialMemesPopulator(stubInitialMemesProvider, mockRepository, stubMigrationsChecker);
            }
        }
    }
}