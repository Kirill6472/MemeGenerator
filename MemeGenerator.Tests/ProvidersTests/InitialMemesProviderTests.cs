using System.Linq;
using FluentAssertions;
using MemeGenerator.DAL;
using MemeGenerator.DAL.Configs;
using MemeGenerator.DAL.Providers;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace MemeGenerator.Tests.ProvidersTests
{
    [TestFixture]
    class InitialMemesProviderTests
    {
        [Test]
        public void GetDataFromJson_PathToImageTemplateConfig_ImageTemplateListFromJson()
        {
            ImageTemplateConfig config = new ImageTemplateConfig
            {
                PathToImageTemplatesConfig = "D:\\Projects\\MemeGenerator\\MemeGenerator.BLL\\Seed\\memeTemplates.json"
            };

            var monitor = new Mock<IOptionsMonitor<ImageTemplateConfig>>();
            monitor.Setup(m => m.CurrentValue).Returns(config);

            InitialMemesProvider initialMemesProvider = new InitialMemesProvider(monitor.Object);

            ImageTemplateList imageTemplateList = initialMemesProvider.GetDataFromJson();

            imageTemplateList.ImageTemplate.Count().Should().Be(2);
        }
    }
}
