using System.Linq;
using MemeGenerator.DAL.MigrationsChecker;
using MemeGenerator.DAL.Providers;
using MemeGenerator.DAL.Repositories;

namespace MemeGenerator.BLL.Services.InitialMemesPopulator
{
    public class InitialMemesPopulator : IInitialMemesPopulator
    {
        private readonly IInitialMemesProvider _initialMemesProvider;
        private readonly IImageTemplateRepository _imageTemplateRepository;
        private readonly IMigrationsChecker _checker;

        public InitialMemesPopulator(IInitialMemesProvider provider, IImageTemplateRepository imageTemplateRepository,
            IMigrationsChecker checker)
        {
            _initialMemesProvider = provider;
            _imageTemplateRepository = imageTemplateRepository;
            _checker = checker;
        }

        public void Initialize()
        {
            if (_checker.DoAllMigrationsApply() && IsImageTableEmpty())
            {
                var imageTemplates = _initialMemesProvider.GetDataFromJson();

                foreach (var image in imageTemplates.ImageTemplate)
                {
                    image.Folder = imageTemplates.Folder;
                    _imageTemplateRepository.Insert(image);
                }

                _imageTemplateRepository.Save();
            }
        }

        private bool IsImageTableEmpty()
        {
            return !_imageTemplateRepository.GetAll().Any();
        }
    }
}
