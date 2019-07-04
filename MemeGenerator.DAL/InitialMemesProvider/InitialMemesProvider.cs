using System.IO;
using MemeGenerator.DAL.ImageTemplateConfig;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace MemeGenerator.DAL.InitialMemesProvider
{
    public class InitialMemesProvider : IInitialMemesProvider
    {
        private readonly IImageTemplateConfig _imageTemplateConfig;

        public InitialMemesProvider(IOptionsMonitor<IImageTemplateConfig> imageTemplateAccessor)
        {
            _imageTemplateConfig = imageTemplateAccessor.CurrentValue;
        }

        public ImageTemplateList GetDataFromJson()
        {
            return JsonConvert.DeserializeObject<ImageTemplateList>(
                File.ReadAllText(_imageTemplateConfig.PathToImageTemplatesConfig));
        }
    }
}
