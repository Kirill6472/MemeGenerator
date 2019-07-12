using MemeGenerator.DAL.Configs;
using MemeGenerator.DAL.FileReader;
using MemeGenerator.DAL.Providers;
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

        [SetUp]
        public void Setup()
        {
            _monitor = new Mock<IOptionsMonitor<ImageTemplateConfig>>();
            _mockFileReader = new Mock<IFileReader>();
        }

        [Test]
        public void GetData_PathToImageTemplateConfig_ImageTemplateListFromJson()
        {
            const string filePath = "filePath";
            var initialMemesProvider = new InitialMemesProvider(_monitor.Object, _mockFileReader.Object);

            initialMemesProvider.GetData();

            _mockFileReader.Verify(mock => mock.GetImageData(filePath), Times.AtMostOnce);
        }
    }
}
