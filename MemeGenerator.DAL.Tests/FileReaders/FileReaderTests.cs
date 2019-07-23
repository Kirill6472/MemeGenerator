using System.IO;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using MemeGenerator.DAL.FileReaders;
using NUnit.Framework;

namespace MemeGenerator.DAL.Tests.FileReaders
{
    [TestFixture]
    public class FileReaderTests
    {
        private const string TestFilePath = "TestFile.json";
        private const string TestData = "testData";
        private FileReaderFixture _fileReaderFixture;
        private FileReader _fileReader;

        [SetUp]
        public async Task Setup()
        {
            _fileReaderFixture = new FileReaderFixture();
            await _fileReaderFixture.CreateFile(TestFilePath, TestData);

            _fileReader = new FileReader();
        }

        [Test]
        public async Task ReadBytes_JsonFileExist_DataInBytes()
        {
            var data = await _fileReader.ReadBytes(Path.Combine(Directory.GetCurrentDirectory(), TestFilePath));
            
            data.Should().Contain(Encoding.UTF8.GetBytes(TestData));
        }

        [Test]
        public async Task ReadString_JsonFileExist_DataInString()
        {
            var data = await _fileReader.ReadString(Path.Combine(Directory.GetCurrentDirectory(), TestFilePath));

            data.Should().Be(TestData);
        }

        [TearDown]
        public void TearDown()
        {
            _fileReaderFixture.DeleteFile(TestFilePath);
        }
    }
}
