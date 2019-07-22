using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemeGenerator.BLL.Services;
using MemeGenerator.DAL;
using MemeGenerator.DAL.MigrationsChecker;
using MemeGenerator.DAL.Providers;
using MemeGenerator.DAL.Repositories;
using MemeGenerator.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace MemeGenerator.BLL.Tests.Services
{
    [TestFixture]
    public class InitialMemesPopulatorTests
    {
        private Mock<IInitialMemesProvider> _mockInitialMemesProvider;
        private Mock<IMemeRepository> _mockRepository;
        private Mock<IMigrationsChecker> _mockMigrationsChecker;
        private MemeImage _memeImage;
        private InitialMemesPopulator _initialMemesPopulator;

        [SetUp]
        public void Setup()
        {
            _memeImage = new MemeImage();

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
        public async Task Initialize_InitializationDataIsCorrect_filledDb(int countMemeImages)
        {
            ThereAreInitialMemes(countMemeImages);
            AllMigrationAreApplied();
            ThereAreNotMemeImages();
            var memes = GenerateInitialMemes(countMemeImages);

            await _initialMemesPopulator.Initialize();

            AssertThatDataExists();
            AssertThatDataSaved(countMemeImages, memes);
        }

        [Test]
        public async Task Initialize_NotAllMigrationsAreApplied_DbNotFilled()
        {
            NotAllMigrationsAreApplied();
            ThereAreNotMemeImages();
            
            await _initialMemesPopulator.Initialize();

            AssertThatDataNotSaved();
        }

        [Test]
        public async Task Initialize_ThereAreMemeImages_DbNotChanged()
        {
            AllMigrationAreApplied();
            ThereAreMemeImages();

            await _initialMemesPopulator.Initialize();

            AssertThatDataNotSaved();
        }

        private void AssertThatDataNotSaved()
        {
            _mockRepository.Verify(mock => mock.Insert(_memeImage), Times.Never);
            _mockRepository.Verify(mock => mock.Save(), Times.Never);
        }

        private void AssertThatDataSaved(int countMemeImages, InitialMemesStorageStructure initialMemesStorageStructure)
        {
            foreach (var image in initialMemesStorageStructure.MemeImages)
            {
                _mockRepository.Verify(mock => mock.Insert(image),
                    Times.Exactly(1));
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
            _mockRepository.Setup(m => m.GetMemesCount()).Returns(memeImages.MemeImages.Count);
        }

        private void ThereAreNotMemeImages()
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

        private void ThereAreInitialMemes(int countMemeImages)
        {
            _mockInitialMemesProvider.Setup(m => m.GetData()).ReturnsAsync(GenerateInitialMemes(countMemeImages));
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