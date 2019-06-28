using MemeGenerator.BLL.Services.InitialMemesProvider;
using MemeGenerator.DAL.ImageTemplateRepository;

namespace MemeGenerator.BLL.Services.InitialMemesPopulator
{
    public class InitialMemesPopulator : IInitialMemesPopulator
    {
        private readonly IInitialMemesProvider _initialMemesProvider;
        private readonly IImageTemplateRepository _imageTemplateRepository;

        public InitialMemesPopulator(IInitialMemesProvider provider, IImageTemplateRepository imageTemplateRepository)
        {
            _initialMemesProvider = provider;
            _imageTemplateRepository = imageTemplateRepository;
        }

        public void Initialize()
        {
            if (!_imageTemplateRepository.AllMigrationsApplied() || IsImageTableEmpty()) return;

            var imageTemplates = _initialMemesProvider.GetDataFromJson();

            foreach (var image in imageTemplates.ImageTemplate)
            {
                image.Folder = imageTemplates.Folder;
                _imageTemplateRepository.InsertImageTemplate(image);
            }

            _imageTemplateRepository.Save();
        }

        private bool IsImageTableEmpty()
        {
            return _imageTemplateRepository.GetImageTemplates() == null;
        }
    }
}
