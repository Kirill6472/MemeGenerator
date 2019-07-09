using System.IO;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using MemeGenerator.DAL.Configs;
using System.Threading.Tasks;

namespace MemeGenerator.DAL.Providers
{
    public class InitialMemesProvider : IInitialMemesProvider
    {
        private readonly ImageTemplateConfig _imageTemplateConfig;

        public InitialMemesProvider(IOptionsMonitor<ImageTemplateConfig> imageTemplateAccessor)
        {
            _imageTemplateConfig = imageTemplateAccessor.CurrentValue;
        }

        public async Task<ImageTemplateList> GetData()
        {
            return JsonConvert.DeserializeObject<ImageTemplateList>(
                await File.ReadAllTextAsync(_imageTemplateConfig.PathToImageTemplatesConfig));
        }

        public byte[] GetImageData(int i)
        {
            byte[] imageData = null;

            var files = Directory.GetFiles(GetData().Result.Folder);
            var fileInfo = new FileInfo(files[i]);
            var imageBytes = fileInfo.Length;

            using (var fileStream = new FileStream(files[i], FileMode.Open, FileAccess.Read))
            {
                var binaryReader = new BinaryReader(fileStream);
                imageData = binaryReader.ReadBytes((int)imageBytes);
            }

            return imageData;
        }
    }
}
