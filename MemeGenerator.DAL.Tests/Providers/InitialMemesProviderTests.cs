using System;
using System.IO;
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
            var initialMemesStorageStructure = GenerateInitialMemes(countMemeImages);
            ThereIsInitialMemesStorageStructure(initialMemesStorageStructure);
            ThereAreMemeImages(initialMemesStorageStructure);

            var result = await _initialMemesProvider.GetData();

            result.Should().BeEquivalentTo(initialMemesStorageStructure);
        }

        private void ThereAreMemeImages(InitialMemesStorageStructure initialMemesStorageStructure)
        {
            foreach (var meme in initialMemesStorageStructure.MemeImages)
            {
                _mockFileReader.Setup(m => m.ReadBytes(Path.Combine(initialMemesStorageStructure.Folder, meme.Name)))
                    .ReturnsAsync(GetBytes(initialMemesStorageStructure));
                meme.Data = $"data:image/jpeg;base64,{Convert.ToBase64String(GetBytes(initialMemesStorageStructure))}";
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
