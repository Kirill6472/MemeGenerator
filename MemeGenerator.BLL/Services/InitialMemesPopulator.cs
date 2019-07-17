using System.Linq;
using System.Threading.Tasks;
using MemeGenerator.DAL.MigrationsChecker;
using MemeGenerator.DAL.Providers;
using MemeGenerator.DAL.Repositories;

namespace MemeGenerator.BLL.Services
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

        public async Task Initialize()
        {
            if (_checker.AreAllMigrationsApplied() && IsImageTemplateExists())
            {
                var imageTemplates = await _initialMemesProvider.GetData();

                foreach (var image in imageTemplates.ImageTemplate)
                {
                    _imageTemplateRepository.Insert(image);
                }

                _imageTemplateRepository.Save();
            }
        }

        private bool IsImageTemplateExists()
        {
            return _imageTemplateRepository.GetAll().Count() == 0;
        }
    }
}
