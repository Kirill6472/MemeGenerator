using System.Threading.Tasks;
using MemeGenerator.DAL.FileReader;

namespace MemeGenerator.DAL.Providers
{
    public class InitialMemesProvider : IInitialMemesProvider
    {
        private readonly IFileReader _fileReader;

        public InitialMemesProvider(IFileReader fileReader)
        {
            _fileReader = fileReader;
        }
        
        public async Task<ImageTemplateList> GetData()
        {
            var imageTemplates = await _fileReader.GetImageTemplateList();

            foreach (var image in imageTemplates.ImageTemplate)
            {
                var filePath = imageTemplates.Folder + image.Name;
                image.Data = _fileReader.GetImageData(filePath);
            }

            return imageTemplates;
        }
    }
}
