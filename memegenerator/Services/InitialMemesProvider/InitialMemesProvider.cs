using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.IO;

namespace MemeGenerator.Services.InitialMemesProvider
{
    public class InitialMemesProvider : IInitialMemesProvider
    {
        private readonly ImageTemplateConfig _imageTemplateConfig;

        public InitialMemesProvider(IOptionsMonitor<ImageTemplateConfig> imageTemplateAccessor)
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
