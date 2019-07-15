using System.IO;
using System.Threading.Tasks;
using MemeGenerator.DAL.Configs;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace MemeGenerator.DAL.FileReaders
{
    public class FileReader : IFileReader
    {
        private readonly ImageTemplateConfig _imageTemplateConfig;

        public FileReader(IOptionsMonitor<ImageTemplateConfig> imageTemplateAccessor)
        {
            _imageTemplateConfig = imageTemplateAccessor.CurrentValue;
        }

        public async Task<ImageTemplateList> GetImageTemplateList()
        {
            return JsonConvert.DeserializeObject<ImageTemplateList>(
                await File.ReadAllTextAsync(_imageTemplateConfig.PathToImageTemplatesConfig));
        }

        public byte[] GetImageData(string filePath)
        {
            byte[] imageData;

            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var binaryReader = new BinaryReader(fileStream);
                imageData = binaryReader.ReadBytes((int)binaryReader.BaseStream.Length);
            }

            return imageData;
        }
    }
}
