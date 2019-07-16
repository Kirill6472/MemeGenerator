using System.Collections.Generic;
using System.Threading.Tasks;
using MemeGenerator.DAL.FileReaders;
using MemeGenerator.DAL.Providers;
using MemeGenerator.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace MemeGenerator.DAL.Tests.Providers
{
    [TestFixture]
    public class InitialMemesProviderTests
    {
        private Mock<IFileReader> _mockFileReader;
        private InitialMemesStorageStructure _imageTemplateList;

        [SetUp]
        public void Setup()
        {
            _imageTemplateList = new InitialMemesStorageStructure
            {
                Folder = "folder",
                ImageTemplate = new List<ImageTemplate>
                {
                    new ImageTemplate
                    {
                        Name = "name",
                        Data = new byte[0],
                        Description = "description"
                    }
                }
            };

            _mockFileReader = new Mock<IFileReader>();
        }

        [Test]
        public async Task GetData_FileReader_ImageTemplateList()
        {
            _mockFileReader.Setup(mock => mock.GetImageTemplateList()).ReturnsAsync(_imageTemplateList);
            var fakeFilePath = _imageTemplateList.Folder + _imageTemplateList.ImageTemplate[0].Name;
            var initialMemesProvider = new InitialMemesProvider(_mockFileReader.Object);

            await initialMemesProvider.GetData();

            _mockFileReader.Verify(mock => mock.GetImageTemplateList(), Times.Once);
            _mockFileReader.Verify(mock => mock.GetImageData(fakeFilePath), Times.AtLeastOnce);
        }
    }
}
