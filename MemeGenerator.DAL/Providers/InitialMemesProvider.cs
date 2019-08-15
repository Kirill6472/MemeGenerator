using System.IO;
using System.Threading.Tasks;
using MemeGenerator.DAL.Configs;
using MemeGenerator.DAL.Converters;
using MemeGenerator.DAL.Exceptions;
using MemeGenerator.DAL.FileReaders;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace MemeGenerator.DAL.Providers
{
    public class InitialMemesProvider : IInitialMemesProvider
    {
        private readonly MemesConfig _memesConfig;
        private readonly IFileReader _fileReader;
        private readonly IBase64ImageEncoder _base64ImageEncoder;

        public InitialMemesProvider(
            IOptionsMonitor<MemesConfig> imageTemplateAccessor,
            IFileReader fileReader,
            IBase64ImageEncoder base64Converter)
        {
            _memesConfig = imageTemplateAccessor.CurrentValue;
            _fileReader = fileReader;
            _base64ImageEncoder = base64Converter;
        }

        public async Task<InitialMemesStorageStructure> GetData()
        {
            var imagesMetadata = JsonConvert.DeserializeObject<InitialMemesStorageStructure>(
                await _fileReader.ReadString(_memesConfig.PathToMemesConfig));

            foreach (var image in imagesMetadata.MemeImages)
            {
                var filePath = Path.Combine(imagesMetadata.Folder, image.Name);
                AssertThatFileHasExtension(filePath);

                var imageBytes = await _fileReader.ReadBytes(filePath);
                image.Data = _base64ImageEncoder.Convert(imageBytes, Path.GetExtension(filePath).Substring(1));
            }

            return imagesMetadata;
        }

        private static void AssertThatFileHasExtension(string filePath)
        {
            if (string.IsNullOrEmpty(Path.GetExtension(filePath)))
            {
                throw new InitialMemesStorageStructureException($"File has no extension: {filePath}");
            }
        }
    }
}
