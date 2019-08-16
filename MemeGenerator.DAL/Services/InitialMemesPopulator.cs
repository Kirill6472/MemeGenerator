using System.Threading.Tasks;
using MemeGenerator.DAL.MigrationsChecker;
using MemeGenerator.DAL.Repositories;
using MemeGenerator.Infrastructure.Providers;

namespace MemeGenerator.DAL.Services
{
    public class InitialMemesPopulator : IInitialMemesPopulator
    {
        private readonly IInitialMemesProvider _initialMemesProvider;
        private readonly IMemeRepository _memeRepository;
        private readonly IMigrationsChecker _checker;

        public InitialMemesPopulator(
            IInitialMemesProvider provider, 
            IMemeRepository imageTemplateRepository,
            IMigrationsChecker checker)
        {
            _initialMemesProvider = provider;
            _memeRepository = imageTemplateRepository;
            _checker = checker;
        }

        public async Task Initialize()
        {
            if (_checker.AreAllMigrationsApplied() && IsImageTemplateExists())
            {
                var imageTemplates = await _initialMemesProvider.GetData();

                foreach (var image in imageTemplates.MemeImages)
                {
                    _memeRepository.Insert(image);
                }

                _memeRepository.Save();
            }
        }

        private bool IsImageTemplateExists()
        {
            return _memeRepository.Count() == 0;
        }
    }
}
