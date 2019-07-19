using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using MemeGenerator.DAL.Configs;
using MemeGenerator.DAL.FileReaders;
using MemeGenerator.DAL.Providers;
using MemeGenerator.Domain.Entities;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace MemeGenerator.DAL.Tests.Providers
{
    [TestFixture]
    public class InitialMemesProviderTests
    {
        private Mock<IOptionsMonitor<MemesConfig>> _monitor;
        private Mock<IFileReader> _mockFileReader;
        private MemesConfig _memesConfig;
        private InitialMemesStorageStructure _initialMemesStorageStructure;
        private InitialMemesProvider _initialMemesProvider;
        private const string FakePath = "/fakePath";

        [SetUp]
        public void Setup()
        {
            _memesConfig = new MemesConfig() { PathToMemesConfig = FakePath };

            _monitor = new Mock<IOptionsMonitor<MemesConfig>>();
            _monitor.Setup(m => m.CurrentValue).Returns(_memesConfig);

            _mockFileReader = new Mock<IFileReader>();

            _initialMemesProvider = new InitialMemesProvider(_monitor.Object, _mockFileReader.Object);
        }

        [TestCase(0)]
        [TestCase(2)]
        [TestCase(10)]
        public async Task GetData_FileReader_InitialData(int countMemeImages)
        {
            _initialMemesStorageStructure = GenerateInitialMemes(countMemeImages);
            ThereIsFileReadInString();
            ThereIsFileReadInBytes();

            var result = await _initialMemesProvider.GetData();

            result.Folder.Should().Be(_initialMemesStorageStructure.Folder);
            result.MemeImages.Should().Contain(_initialMemesStorageStructure.MemeImages);
        }

        private void ThereIsFileReadInBytes()
        {
            _mockFileReader.Setup(m => m.ReadBytes(_memesConfig.PathToMemesConfig))
                .ReturnsAsync(Encoding.ASCII.GetBytes(SerializeInitialMemes(_initialMemesStorageStructure)));
        }

        private void ThereIsFileReadInString()
        {
            _mockFileReader.Setup(m => m.ReadString(_memesConfig.PathToMemesConfig))
                .ReturnsAsync(SerializeInitialMemes(_initialMemesStorageStructure));
        }

        private static string SerializeInitialMemes(InitialMemesStorageStructure initialMemesStorageStructure)
        {
            return JsonConvert.SerializeObject(initialMemesStorageStructure);
        }

        private static InitialMemesStorageStructure GenerateInitialMemes(int countMemeImages)
        {
            var initialMemes = new InitialMemesStorageStructure
            {
                Folder = "/folder",
                MemeImages = Enumerable.Range(0, countMemeImages)
                    .Select(x => new MemeImage { Name = x.ToString() })
                    .ToList()
            };

            return initialMemes;
        }
    }
}
