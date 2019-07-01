using MemeGenerator.BLL.Services.InitialMemesPopulator;
using NUnit.Framework;

namespace MemeGenerator.Tests
{
    [TestFixture]
    public class InitialMemesPopulatorTests
    {
        private InitialMemesPopulatorTests initialMemesPopulatorTests = null;
        
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void Initialize_ObtainedDataAndDbContext_filledDb()
        {
            StubInitialMemesProvider stubInitialMemesProvider = new StubInitialMemesProvider();

            InitialMemesPopulator initialMemesPopulator = new InitialMemesPopulator(stubInitialMemesProvider);
        }
    }
}