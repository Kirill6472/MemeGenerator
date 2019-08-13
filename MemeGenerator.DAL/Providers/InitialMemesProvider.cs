using System.IO;
using System.Threading.Tasks;
using MemeGenerator.DAL.Configs;
using MemeGenerator.DAL.Converters;
using MemeGenerator.DAL.FileReaders;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace MemeGenerator.DAL.Providers
{
    public class InitialMemesProvider : IInitialMemesProvider
    {
        private readonly MemesConfig _memesConfig;
        private readonly IFileReader _fileReader;
        private readonly IBase64Converter _base64Converter;

        public InitialMemesProvider(
            IOptionsMonitor<MemesConfig> imageTemplateAccessor,
            IFileReader fileReader,
            IBase64Converter base64Converter)
        {
            _memesConfig = imageTemplateAccessor.CurrentValue;
            _fileReader = fileReader;
            _base64Converter = base64Converter;
        }

        public async Task<InitialMemesStorageStructure> GetData()
        {
            var imagesMetadata = JsonConvert.DeserializeObject<InitialMemesStorageStructure>(
                await _fileReader.ReadString(_memesConfig.PathToMemesConfig));

            foreach (var image in imagesMetadata.MemeImages)
            {
                var filePath = Path.Combine(imagesMetadata.Folder, image.Name);
                var imageBytes = await _fileReader.ReadBytes(filePath);
                image.Data = _base64Converter.ConvertToBase64(imageBytes);
            }

            return imagesMetadata;
        }
    }
}
