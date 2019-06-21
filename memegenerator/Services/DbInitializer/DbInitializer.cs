using System.Linq;
using MemeGenerator.Models;
using MemeGenerator.Services;
using MemeGenerator.Services.InitialMemesProvider;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MemeGenerator
{
    public class DbInitializer : IDbInitializer
    {
        private readonly IInitialMemesProvider _initialMemesProvider;
        private readonly MemeGeneratorDbContext _context;

        public DbInitializer(IInitialMemesProvider provider, MemeGeneratorDbContext context)
        {
            _initialMemesProvider = provider;
            _context = context;
        }

        public void Initialize()
        {
            if (AllMigrationsApplied() && IsImageTableEmpty())
            {
                ImageTemplateList imageTemplates = _initialMemesProvider.GetDataFromJson();

                foreach (var image in imageTemplates.ImageTemplate)
                {
                    image.Folder = imageTemplates.Folder;
                    _context.AddRange(image);
                }

                _context.SaveChanges();
            }
        }

        private bool AllMigrationsApplied()
        {
            var applied = _context.GetService<IHistoryRepository>().GetAppliedMigrations().Select(m => m.MigrationId);

            var total = _context.GetService<IMigrationsAssembly>().Migrations.Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        private bool IsImageTableEmpty()
        {
            return !_context.ImageTemplates.Any();
        }
    }
}
