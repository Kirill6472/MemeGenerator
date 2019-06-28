using System.Collections.Generic;
using Autofac.Extras.Moq;
using MemeGenerator.BLL.Services.InitialMemesPopulator;
using MemeGenerator.DAL;
using MemeGenerator.DAL.InitialMemesProvider;
using MemeGenerator.Domain.Models;
using NUnit.Framework;

namespace MemeGenerator.Tests
{
    [TestFixture]
    public class InitialMemesPopulatorTests
    {
        private InitialMemesPopulatorTests initialMemesPopulatorTests = null;
        
        [SetUp]
        public void SetUp()
        {
            ImageTemplateList fakeImageTemplates = new ImageTemplateList
            {
                Folder = "folder",
                ImageTemplate = new List<ImageTemplate>
                {
                    new ImageTemplate()
                    {
                        Name = "name",
                        Folder = "folder",
                        Description = "description",
                    }
                }
            };

            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IInitialMemesProvider>().Setup(x => x.GetDataFromJson()).Returns(fakeImageTemplates);
                var mockInitialMemeProvider = mock.Create<InitialMemesProvider>();

                FakeDbContext fakeDbContext = new FakeDbContext();

                //InitialMemesPopulator = new InitialMemesPopulator(mockInitialMemeProvider, fakeDbContext.MemeGeneratorDbContext);
            }
        }

        [Test]
        public void Initialize_ObtainedDataAndDbContext_filledDb()
        {
        }
    }
}