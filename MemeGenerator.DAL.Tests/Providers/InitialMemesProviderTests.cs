using System.Linq;
using FluentAssertions;
using MemeGenerator.DAL.Configs;
using MemeGenerator.DAL.Providers;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace MemeGenerator.DAL.Tests.Providers
{
    [TestFixture]
    public class InitialMemesProviderTests
    {
        [Test]
        public void GetData_PathToImageTemplateConfig_ImageTemplateListFromJson()
        {
            var config = new ImageTemplateConfig
            {
                PathToImageTemplatesConfig = "D:\\Projects\\MemeGenerator\\MemeGenerator.DAL\\Seed\\memeTemplates.json"
            };
            var monitor = new Mock<IOptionsMonitor<ImageTemplateConfig>>();
            monitor.Setup(m => m.CurrentValue).Returns(config);
            var initialMemesProvider = new InitialMemesProvider(monitor.Object);

            var imageTemplateList = initialMemesProvider.GetData();

            imageTemplateList.Result.ImageTemplate.Count().Should().Be(2);
        }
    }
}
