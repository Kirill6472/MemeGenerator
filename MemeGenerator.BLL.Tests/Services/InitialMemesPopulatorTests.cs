using System.Collections.Generic;
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
        private Mock<IImageTemplateRepository> _mockRepository;
        private Mock<IMigrationsChecker> _mockMigrationsChecker;
        private ImageTemplate _imageTemplate;
        private InitialMemesPopulator _initialMemesPopulator;

        [SetUp]
        public void Setup()
        {
            _imageTemplate = new ImageTemplate();

            _mockInitialMemesProvider = new Mock<IInitialMemesProvider>();
            _mockRepository = new Mock<IImageTemplateRepository>();
            _mockMigrationsChecker = new Mock<IMigrationsChecker>();
            _initialMemesPopulator = new InitialMemesPopulator(
                _mockInitialMemesProvider.Object,
                _mockRepository.Object,
                _mockMigrationsChecker.Object);
        }

        [TestCase(0)]
        [TestCase(2)]
        [TestCase(10)]
        public async Task Initialize_AllMigrationsApplyAndImageTableEmpty_filledDb(int countImageTemplates)
        {
            _mockInitialMemesProvider.Setup(m => m.GetData()).ReturnsAsync(GetInitialMemes(countImageTemplates));
            ConfigureInitializationConditions(true, true);
            
            await _initialMemesPopulator.InitializeAsync();

            VerifyGetData(true);
            VerifySaveDataInDb(countImageTemplates);
        }

        [Test]
        public async Task Initialize_NotAllMigrationsAreApplied_DbNotFilled()
        {
            ConfigureInitializationConditions(false, true);

            await _initialMemesPopulator.InitializeAsync();

            VerifyGetData(false);
            VerifyDataIsNotSaved();
        }

        [Test]
        public async Task Initialize_ImageTableNotEmpty_DbNotChanged()
        {
            ConfigureInitializationConditions(true, false);

            await _initialMemesPopulator.InitializeAsync();

            VerifyGetData(false);
            VerifyDataIsNotSaved();
        }

        private void VerifyDataIsNotSaved()
        {
            _mockRepository.Verify(mock => mock.Insert(_imageTemplate), Times.Never);
            _mockRepository.Verify(mock => mock.Save(), Times.Never);
        }

        private void VerifySaveDataInDb(int countImageTemplates)
        {
            _mockRepository.Verify(mock => mock.Insert(_imageTemplate),
                Times.AtMost(GetInitialMemes(countImageTemplates).ImageTemplate.Count));
            _mockRepository.Verify(mock => mock.Save(), Times.Once);
        }

        private void VerifyGetData(bool areReceivedData)
        {
            if (areReceivedData)
            {
                _mockInitialMemesProvider.Verify(mock => mock.GetData(), Times.Once);
            }
            else
            {
                _mockInitialMemesProvider.Verify(mock => mock.GetData(), Times.Never);
            }
        }

        private void ConfigureInitializationConditions(bool areMigrationsApplied, bool tableEmpty)
        {
            _mockMigrationsChecker.Setup(m => m.AreAllMigrationsApplied()).Returns(areMigrationsApplied);

            if (tableEmpty)
            {
                _mockRepository.Setup(m => m.GetAll()).Returns(new List<ImageTemplate>());
            }
            else
            {
                _mockRepository.Setup(m => m.GetAll()).Returns(new List<ImageTemplate> {new ImageTemplate()});
            }
        }

        private static InitialMemesStorageStructure GetInitialMemes(int countImageTemplates)
        {
            var initialMemes = new InitialMemesStorageStructure
            {
                ImageTemplate = new List<ImageTemplate>(countImageTemplates)
            };

            return initialMemes;
        }
    }
}