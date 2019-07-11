﻿using System.Linq;
using System.Threading.Tasks;
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

        public async Task InitializeAsync()
        {
            if (!_checker.AreAllMigrationsApplied() || !IsImageTemplateExists()) return;

            var imageTemplates = await _initialMemesProvider.GetData();

            foreach (var image in imageTemplates.ImageTemplate)
            {
                _imageTemplateRepository.Insert(image);
            }

            //for (var i = 0; i < imageTemplates.ImageTemplate.Count; i++)
            //{
            //    var image = imageTemplates.ImageTemplate[i];
            //    image.Data = _initialMemesProvider.GetImageData(i);

            //    _imageTemplateRepository.Insert(image);
            //}

            _imageTemplateRepository.Save();
        }

        private bool IsImageTemplateExists()
        {
            return !_imageTemplateRepository.GetAll().Any();
        }
    }
}
