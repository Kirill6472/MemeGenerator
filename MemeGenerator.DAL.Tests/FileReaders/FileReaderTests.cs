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
        private FileReaderFixture _fileReaderFixture;

        [SetUp]
        public async Task Setup()
        {
            _fileReaderFixture = new FileReaderFixture();
            await _fileReaderFixture.CreateFile(TestFilePath, "test data");
        }

        [Test]
        public void ReadBytes_JsonFileExist_DataInBytes()
        {
            var fileReader = new FileReader();

            var data = fileReader.ReadBytes(Path.Combine(Directory.GetCurrentDirectory(), TestFilePath));
            
            data.Result.Should().Contain(Encoding.ASCII.GetBytes("test data"));
        }

        [Test]
        public void ReadString_JsonFileExist_DataInString()
        {
            var fileReader = new FileReader();

            var data = fileReader.ReadString(Path.Combine(Directory.GetCurrentDirectory(), TestFilePath));

            data.Result.Should().Be("test data");
        }

        [TearDown]
        public void TearDown()
        {
            _fileReaderFixture.DeleteFile(TestFilePath);
        }
    }
}
