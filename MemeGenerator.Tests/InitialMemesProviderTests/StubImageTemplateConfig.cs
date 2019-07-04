using MemeGenerator.DAL.ImageTemplateConfig;

namespace MemeGenerator.Tests.InitialMemesProviderTests
{
    public class StubImageTemplateConfig : IImageTemplateConfig
    {
        public string PathToImageTemplatesConfig
        {
            get { return PathToImageTemplatesConfig; }
            set
            {
                PathToImageTemplatesConfig =
                    "D:\\Projects\\MemeGenerator\\MemeGenerator.BLL\\Seed\\memeTemplates.json";
            }
        }
    }
}
