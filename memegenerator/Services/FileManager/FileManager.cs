using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.IO;

namespace MemeGenerator.Services.FileManager
{
    public class FileManager : IFileManager
    {
        private readonly ImageTemplateConfig _imageTemplateConfig;

        public FileManager(IOptionsMonitor<ImageTemplateConfig> imageTemplateAccessor)
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
