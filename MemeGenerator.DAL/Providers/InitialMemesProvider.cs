using System;
using System.IO;
using System.Threading.Tasks;
using MemeGenerator.DAL.Configs;
using MemeGenerator.DAL.FileReaders;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace MemeGenerator.DAL.Providers
{
    public class InitialMemesProvider : IInitialMemesProvider
    {
        private readonly MemesConfig _memesConfig;
        private readonly IFileReader _fileReader;

        public InitialMemesProvider(IOptionsMonitor<MemesConfig> imageTemplateAccessor, IFileReader fileReader)
        {
            _memesConfig = imageTemplateAccessor.CurrentValue;
            _fileReader = fileReader;
        }

        public async Task<InitialMemesStorageStructure> GetData()
        {
            var imagesMetadata = JsonConvert.DeserializeObject<InitialMemesStorageStructure>(
                await _fileReader.ReadString(_memesConfig.PathToMemesConfig));

            foreach (var image in imagesMetadata.MemeImages)
            {
                var filePath = Path.Combine(imagesMetadata.Folder, image.Name);
                var imageBytes = await _fileReader.ReadBytes(filePath);
                image.Data = $"data:image/jpeg;base64,{Convert.ToBase64String(imageBytes)}";
            }

            return imagesMetadata;
        }
    }
}
