using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using MemeGenerator.DAL.Configs;
using MemeGenerator.DAL.FileReaders;
using MemeGenerator.DAL.Providers;
using MemeGenerator.Domain.Entities;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace MemeGenerator.DAL.Tests.Providers
{
    [TestFixture]
    public class InitialMemesProviderTests
    {
        private Mock<IOptionsMonitor<ImageTemplateConfig>> _monitor;
        private Mock<IFileReader> _mockFileReader;
        private ImageTemplateConfig _imageTemplateConfig;
        private InitialMemesStorageStructure _initialMemesStorageStructure;

        [SetUp]
        public void Setup()
        {
            _imageTemplateConfig = new ImageTemplateConfig() {PathToImageTemplatesConfig = "fake path"};

            _monitor = new Mock<IOptionsMonitor<ImageTemplateConfig>>();
            _monitor.Setup(m => m.CurrentValue).Returns(_imageTemplateConfig);

            _initialMemesStorageStructure = new InitialMemesStorageStructure()
            {
                ImageTemplate = new List<ImageTemplate> { new ImageTemplate() }
            };
            var serializeInitialMemes = Serialize();

            _mockFileReader = new Mock<IFileReader>();
            _mockFileReader.Setup(m => m.ReadString(_imageTemplateConfig.PathToImageTemplatesConfig))
                .ReturnsAsync(Encoding.UTF8.GetString(serializeInitialMemes, 0, serializeInitialMemes.Length));
            _mockFileReader.Setup(m => m.ReadBytes(_imageTemplateConfig.PathToImageTemplatesConfig))
                .ReturnsAsync(serializeInitialMemes);
        }

        [Test]
        public async Task GetData_FileReader_InitialData()
        {
            var initialMemesProvider = new InitialMemesProvider(_monitor.Object, _mockFileReader.Object);

            await initialMemesProvider.GetData();

            _mockFileReader.Verify(
                mock => mock.ReadString(_imageTemplateConfig.PathToImageTemplatesConfig), Times.Once);
            _mockFileReader.Verify(
                mock => mock.ReadBytes(_imageTemplateConfig.PathToImageTemplatesConfig),
                Times.AtMost(_initialMemesStorageStructure.ImageTemplate.Count));
        }

        private byte[] Serialize()
        {
            var memoryStream = new MemoryStream();

            var serializer = new DataContractJsonSerializer(typeof(InitialMemesStorageStructure));
            serializer.WriteObject(memoryStream, _initialMemesStorageStructure);

            var json = memoryStream.ToArray();
            memoryStream.Close();

            return json;
        }
    }
}
