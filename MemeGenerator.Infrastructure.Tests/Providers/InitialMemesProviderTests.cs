using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using MemeGenerator.Domain.Entities;
using MemeGenerator.Infrastructure.Configs;
using MemeGenerator.Infrastructure.Converters;
using MemeGenerator.Infrastructure.Exceptions;
using MemeGenerator.Infrastructure.FileReaders;
using MemeGenerator.Infrastructure.Providers;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace MemeGenerator.Infrastructure.Tests.Providers
{
    [TestFixture]
    public class InitialMemesProviderTests
    {
        private Mock<IOptionsMonitor<MemesConfig>> _monitor;
        private Mock<IFileReader> _mockFileReader;
        private Mock<IBase64ImageEncoder> _mockConverter;
        private MemesConfig _memesConfig;
        private InitialMemesProvider _initialMemesProvider;
        private const string FakePath = "/fakePath";
        private const string FakeBase64 = "data:image/jpeg;base64,R0lGODdhAQABAPAAAP8AAAAAACwAAAAAAQABAAACAkQBADs=";

        [SetUp]
        public void Setup()
        {
            _memesConfig = new MemesConfig { PathToMemesConfig = FakePath };

            _monitor = new Mock<IOptionsMonitor<MemesConfig>>();
            _monitor.Setup(m => m.CurrentValue).Returns(_memesConfig);

            _mockFileReader = new Mock<IFileReader>();

            _mockConverter = new Mock<IBase64ImageEncoder>();

            _initialMemesProvider =
                new InitialMemesProvider(_monitor.Object, _mockFileReader.Object, _mockConverter.Object);
        }

        [TestCase(0)]
        [TestCase(2)]
        [TestCase(10)]
        public async Task GetData_ValidData_InitializationData(int countMemeImages)
        {
            var initialMemesStorageStructure = GenerateInitialMemes(countMemeImages);
            ThereIsInitialMemesStorageStructure(initialMemesStorageStructure);
            ThereAreMemeImages(initialMemesStorageStructure);

            var result = await _initialMemesProvider.GetData();

            result.Should().BeEquivalentTo(initialMemesStorageStructure);
        }

        [Test]
        public void GetData_ImageWithoutExtension_ThrowsException()
        {
            var initialMemesStorageStructure = GenerateInitialMemesWithoutFileExtension();
            ThereIsInitialMemesStorageStructure(initialMemesStorageStructure);
            var filePath = Path.Combine(initialMemesStorageStructure.Folder, "withoutExtension");

            _initialMemesProvider.Invoking(y => y.GetData())
                .Should().Throw<InitialMemesStorageStructureException>()
                .WithMessage($"File has no extension: {filePath}");
        }

        private void ThereAreMemeImages(InitialMemesStorageStructure initialMemesStorageStructure)
        {
            foreach (var meme in initialMemesStorageStructure.MemeImages)
            {
                var filePath = Path.Combine(initialMemesStorageStructure.Folder, meme.Name);
                var imageBytes = GetBytes(initialMemesStorageStructure);

                _mockFileReader.Setup(m => m.ReadBytes(filePath))
                    .ReturnsAsync(imageBytes);
                _mockConverter.Setup(m => m.Convert(imageBytes, Path.GetExtension(filePath).Substring(1)))
                    .Returns(FakeBase64);

                meme.Data = FakeBase64;
            }
        }

        private static byte[] GetBytes(InitialMemesStorageStructure initialMemesStorageStructure)
        {
            return Encoding.UTF8.GetBytes(Serialize(initialMemesStorageStructure));
        }

        private void ThereIsInitialMemesStorageStructure(InitialMemesStorageStructure initialMemesStorageStructure)
        {
            _mockFileReader.Setup(m => m.ReadString(_memesConfig.PathToMemesConfig))
                .ReturnsAsync(Serialize(initialMemesStorageStructure));
        }

        private static string Serialize(object data)
        {
            return JsonConvert.SerializeObject(data);
        }

        private static InitialMemesStorageStructure GenerateInitialMemesWithoutFileExtension()
        {
            var initialMemes = new InitialMemesStorageStructure
            {
                Folder = "/folder",
                MemeImages = new List<MemeImage> {new MemeImage {Name = "withoutExtension"}}
            };

            return initialMemes;
        }

        private static InitialMemesStorageStructure GenerateInitialMemes(int countMemeImages)
        {
            var initialMemes = new InitialMemesStorageStructure
            {
                Folder = "/folder",
                MemeImages = Enumerable.Range(0, countMemeImages)
                    .Select(x => new MemeImage { Name = $"{x.ToString()}.jpg" })
                    .ToList()
            };

            return initialMemes;
        }
    }
}
