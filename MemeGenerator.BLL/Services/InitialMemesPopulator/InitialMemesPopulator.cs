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

        public InitialMemesPopulator(
            IInitialMemesProvider provider, 
            IImageTemplateRepository imageTemplateRepository,
            IMigrationsChecker checker)
        {
            _initialMemesProvider = provider;
            _imageTemplateRepository = imageTemplateRepository;
            _checker = checker;
        }

        public void Initialize()
        {
            if (!_checker.AreAllMigrationsApplied() || !IsImageTemplateExists()) return;

            var imageTemplates = _initialMemesProvider.GetData();

            for (var i = 0; i < imageTemplates.Result.ImageTemplate.Count; i++)
            {
                var image = imageTemplates.Result.ImageTemplate[i];
                image.Data = _initialMemesProvider.GetImageData(i);

                _imageTemplateRepository.Insert(image);
            }

            _imageTemplateRepository.Save();
        }

        private bool IsImageTemplateExists()
        {
            return !_imageTemplateRepository.GetAll().Any();
        }
    }
}
