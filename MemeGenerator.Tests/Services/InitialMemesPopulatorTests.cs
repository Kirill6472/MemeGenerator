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
        private ImageTemplateList _imageTemplateList;
        private IEnumerable<ImageTemplate> _emptyImageList;
        private IEnumerable<ImageTemplate> _notEmptyImageList;

        [SetUp]
        public void Setup()
        {
            _imageTemplate = new ImageTemplate
            {
                Name = "name",
                Data = new byte[0],
                Description = "description"
            };

            _imageTemplateList = new ImageTemplateList
            {
                Folder = "folder",
                ImageTemplate = new List<ImageTemplate> { _imageTemplate }
            };

            _emptyImageList = new List<ImageTemplate>();
            _notEmptyImageList = new List<ImageTemplate> { _imageTemplate };

            _mockInitialMemesProvider = new Mock<IInitialMemesProvider>();
            _mockInitialMemesProvider.Setup(m => m.GetData()).ReturnsAsync(_imageTemplateList);

            _mockRepository = new Mock<IImageTemplateRepository>();

            _mockMigrationsChecker = new Mock<IMigrationsChecker>();
        }

        [Test]
        public async Task Initialize_AllMigrationsApplyAndImageTableEmpty_filledDbAsync()
        {
            _mockMigrationsChecker.Setup(m => m.AreAllMigrationsApplied()).Returns(true);
            _mockRepository.Setup(m => m.GetAll()).Returns(_emptyImageList);
            var initialMemesPopulator = new InitialMemesPopulator(
                _mockInitialMemesProvider.Object,
                _mockRepository.Object,
                _mockMigrationsChecker.Object);

            await initialMemesPopulator.InitializeAsync();

            _mockInitialMemesProvider.Verify(mock => mock.GetData(), Times.Once);
            _mockRepository.Verify(mock => mock.Insert(_imageTemplate), Times.AtLeastOnce);
            _mockRepository.Verify(mock => mock.Save(), Times.Once);
        }

        [Test]
        public async Task Initialize_NotAllMigrationsAreApplied_DbNotFilledAsync()
        {
            _mockMigrationsChecker.Setup(mock => mock.AreAllMigrationsApplied()).Returns(false);
            _mockRepository.Setup(m => m.GetAll()).Returns(_emptyImageList);
            var initialMemesPopulator = new InitialMemesPopulator(
                _mockInitialMemesProvider.Object,
                _mockRepository.Object,
                _mockMigrationsChecker.Object);

            await initialMemesPopulator.InitializeAsync();

            _mockInitialMemesProvider.Verify(mock => mock.GetData(), Times.Never);
            _mockRepository.Verify(mock => mock.Insert(_imageTemplate), Times.Never);
            _mockRepository.Verify(mock => mock.Save(), Times.Never);
        }

        [Test]
        public async Task Initialize_ImageTableNotEmpty_DbNotChangedAsync()
        {
            _mockMigrationsChecker.Setup(m => m.AreAllMigrationsApplied()).Returns(true);
            _mockRepository.Setup(m => m.GetAll()).Returns(_notEmptyImageList);
            var initialMemesPopulator = new InitialMemesPopulator(
                _mockInitialMemesProvider.Object,
                _mockRepository.Object,
                _mockMigrationsChecker.Object);

            await initialMemesPopulator.InitializeAsync();

            _mockInitialMemesProvider.Verify(mock => mock.GetData(), Times.Never);
            _mockRepository.Verify(mock => mock.Insert(_imageTemplate), Times.Never);
            _mockRepository.Verify(mock => mock.Save(), Times.Never);
        }
    }
}