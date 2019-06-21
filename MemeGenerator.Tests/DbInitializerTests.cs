using Autofac.Extras.Moq;
using MemeGenerator;
using MemeGenerator.Services;
using MemeGenerator.Services.InitialMemesProvider;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class DbInitializerTests
    {
        [Test]
        public void Initialize_ObtainedDataAndDbContext_filledDb()
        {
            ImageTemplateList fakeimageTemplates = new ImageTemplateList();

            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IInitialMemesProvider>().Setup(x => x.GetDataFromJson()).Returns(fakeimageTemplates);
                DbInitializer dbInitializerStub = mock.Create<DbInitializer>();

                dbInitializerStub.Initialize();

                
            }
        }
    }
}