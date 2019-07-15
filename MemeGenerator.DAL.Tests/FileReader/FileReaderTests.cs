using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using MemeGenerator.DAL.Configs;
using MemeGenerator.DAL.FileReader;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace MemeGenerator.DAL.Tests.FileReader
{
    [TestFixture]
    public class FileReaderTests
    {
        private const string TestFile = "TestFile.json";
        private Mock<IOptionsMonitor<ImageTemplateConfig>> _monitor;
        private IFileReader _fileReader;

        private const string ImagesList =
            "{\"folder\": \"MemeTemplate\",\"ImageTemplate\": [{\"name\": \"1.jpg\",\"description\": \"descr\"}]}";

        [SetUp]
        public void Setup()
        {
            var config = new ImageTemplateConfig
            {
                PathToImageTemplatesConfig = Path.Combine(Directory.GetCurrentDirectory(), TestFile)
            };
            
            if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), TestFile)))
            {
                File.Delete(Path.Combine(Directory.GetCurrentDirectory(), TestFile));
            }

            using (FileStream fs = File.Create(Path.Combine(Directory.GetCurrentDirectory(), TestFile)))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes(ImagesList);
                fs.Write(info, 0, info.Length);
            }

            _monitor = new Mock<IOptionsMonitor<ImageTemplateConfig>>();
            _monitor.Setup(mock => mock.CurrentValue).Returns(config);

            _fileReader = new DAL.FileReader.FileReader(_monitor.Object);
        }

        [Test]
        public async Task GetImageTemplateList_JsonFile_ImageTemplateListAsync()
        {
            var imageTemplateList = await _fileReader.GetImageTemplateList();

            imageTemplateList.ImageTemplate.Count.Should().Be(1);
            imageTemplateList.Folder.Should().BeEquivalentTo("MemeTemplate");
        }

        [Test]
        public void GetImageData_FilePath_ImageData()
        {
            var imageData = _fileReader.GetImageData(Path.Combine(Directory.GetCurrentDirectory(), TestFile));

            imageData.Should().NotBeNull();
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(TestFile))
            {
                File.Delete(TestFile);
            }
        }
    }
}
