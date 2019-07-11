using System.IO;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using MemeGenerator.DAL.Configs;
using System.Threading.Tasks;
using MemeGenerator.DAL.FileReader;

namespace MemeGenerator.DAL.Providers
{
    public class InitialMemesProvider : IInitialMemesProvider
    {
        private readonly ImageTemplateConfig _imageTemplateConfig;
        private readonly IFileReader _fileReader;

        public InitialMemesProvider(IOptionsMonitor<ImageTemplateConfig> imageTemplateAccessor, IFileReader fileReader)
        {
            _imageTemplateConfig = imageTemplateAccessor.CurrentValue;
            _fileReader = fileReader;
        }
        
        public async Task<ImageTemplateList> GetData()
        {
            var imageTemplates = JsonConvert.DeserializeObject<ImageTemplateList>(
                await File.ReadAllTextAsync(_imageTemplateConfig.PathToImageTemplatesConfig));

            foreach (var image in imageTemplates.ImageTemplate)
            {
                var filePath = imageTemplates.Folder + image.Name;
                image.Data = _fileReader.GetImageData(filePath);
            }

            return imageTemplates;
        }
    }
}
