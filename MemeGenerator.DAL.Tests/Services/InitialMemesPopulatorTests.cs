using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemeGenerator.Core;
using MemeGenerator.Core.Entities;
using MemeGenerator.DAL.MigrationChecker;
using MemeGenerator.DAL.Providers;
using MemeGenerator.DAL.Services;
using MemeGenerator.Infrastructure;
using Moq;
using NUnit.Framework;

namespace MemeGenerator.DAL.Tests.Services
{
    [TestFixture]
    public class InitialMemesPopulatorTests
    {
        private Mock<IInitialMemesProvider> _mockInitialMemesProvider;
        private Mock<IMemeRepository> _mockRepository;
        private Mock<IMigrationsChecker> _mockMigrationsChecker;
        private InitialMemesPopulator _initialMemesPopulator;

        [SetUp]
        public void Setup()
        {
            _mockInitialMemesProvider = new Mock<IInitialMemesProvider>();
            _mockRepository = new Mock<IMemeRepository>();
            _mockMigrationsChecker = new Mock<IMigrationsChecker>();

            _initialMemesPopulator = new InitialMemesPopulator(
                _mockInitialMemesProvider.Object,
                _mockRepository.Object,
                _mockMigrationsChecker.Object);
        }

        [TestCase(0)]
        [TestCase(2)]
        [TestCase(10)]
        public async Task Initialize_InitializationDataIsCorrect_SaveData(int countMemeImages)
        {
            var memes = ThereAreInitialMemes(countMemeImages);
            AllMigrationAreApplied();
            ThereAreNoSavedMemes();

            await _initialMemesPopulator.Initialize();

            AssertThatDataExists();
            AssertThatDataSaved(memes);
        }

        [Test]
        public async Task Initialize_NotAllMigrationsAreApplied_DataNotSaved()
        {
            NotAllMigrationsAreApplied();
            ThereAreNoSavedMemes();
            
            await _initialMemesPopulator.Initialize();

            AssertThatDataNotSaved();
        }

        [Test]
        public async Task Initialize_ThereAreMemeImages_DataNotSaved()
        {
            AllMigrationAreApplied();
            ThereAreMemeImages();

            await _initialMemesPopulator.Initialize();

            AssertThatDataNotSaved();
        }

        private void AssertThatDataNotSaved()
        {
            _mockRepository.Verify(mock => mock.Save(), Times.Never);
        }

        private void AssertThatDataSaved(InitialMemesStorageStructure initialMemesStorageStructure)
        {
            foreach (var image in initialMemesStorageStructure.MemeImages)
            {
                _mockRepository.Verify(mock => mock.Insert(image), Times.Once);
            }
            _mockRepository.Verify(mock => mock.Save(), Times.Once);
        }

        private void AssertThatDataExists()
        {
            _mockInitialMemesProvider.Verify(mock => mock.GetData(), Times.Once);
        }

        private void ThereAreMemeImages()
        {
            var memeImages = GenerateInitialMemes(2);
            _mockRepository.Setup(m => m.Count()).Returns(memeImages.MemeImages.Count);
        }

        private void ThereAreNoSavedMemes()
        {
            _mockRepository.Setup(m => m.GetAll()).Returns(new List<MemeImage>());
        }

        private void NotAllMigrationsAreApplied()
        {
            _mockMigrationsChecker.Setup(m => m.AreAllMigrationsApplied()).Returns(false);
        }

        private void AllMigrationAreApplied()
        {
            _mockMigrationsChecker.Setup(m => m.AreAllMigrationsApplied()).Returns(true);
        }

        private InitialMemesStorageStructure ThereAreInitialMemes(int countMemeImages)
        {
            var initialMemes = GenerateInitialMemes(countMemeImages);
            _mockInitialMemesProvider.Setup(m => m.GetData()).ReturnsAsync(initialMemes);

            return initialMemes;
        }

        private static InitialMemesStorageStructure GenerateInitialMemes(int countMemeImages)
        {
            var initialMemes = new InitialMemesStorageStructure
            {
                MemeImages = Enumerable.Range(0, countMemeImages).Select(image => new MemeImage()).ToList()
            };

            return initialMemes;
        }
    }
}