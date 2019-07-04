using System.Linq;
using MemeGenerator.DAL;
using MemeGenerator.DAL.ImageTemplateConfig;
using MemeGenerator.DAL.InitialMemesProvider;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace MemeGenerator.Tests.InitialMemesProviderTests
{
    [TestFixture]
    class InitialMemesProviderTests
    {
        [Test]
        public void GetDataFromJson_PathToImageTemplateConfig_ImageTemplateListFromJson()
        {
            var monitor = Mock.Of<IOptionsMonitor<StubImageTemplateConfig>>();
            InitialMemesProvider initialMemesProvider = new InitialMemesProvider(monitor);

            ImageTemplateList imageTemplateList = initialMemesProvider.GetDataFromJson();

            Assert.AreEqual(2, imageTemplateList.ImageTemplate.Count());
        }
    }
}
