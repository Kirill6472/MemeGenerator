using System.Threading.Tasks;
using MemeGenerator.DAL.Configs;
using MemeGenerator.DAL.FileReaders;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

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

        public async Task<InitialMemesStorageStructure> GetData()
        {
            var imagesMetadata = JsonConvert.DeserializeObject<InitialMemesStorageStructure>(
                await _fileReader.ReadString(_imageTemplateConfig.PathToImageTemplatesConfig));

            foreach (var image in imagesMetadata.ImageTemplate)
            {
                var filePath = imagesMetadata.Folder + image.Name;
                var imageBytes = await _fileReader.ReadBytes(filePath);
                image.Data = new byte[imageBytes.Length];
            }

            return imagesMetadata;
        }
    }
}
