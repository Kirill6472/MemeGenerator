using System.Linq;
using MemeGenerator.Models;
using MemeGenerator.Services;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MemeGenerator
{
    public class DbInitializer : IDbInitializer
    {
        public DbInitializer() { }

        public bool AllMigrationsApplied(MemeGeneratorDbContext context)
        {
            var applied = context.GetService<IHistoryRepository>().GetAppliedMigrations().Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>().Migrations.Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        public void Initialize(MemeGeneratorDbContext context, ImageTemplateList imageTemplates)
        {
            context.Database.EnsureCreated();

            if (!context.ImageTemplates.Any())
            {
                foreach (var image in imageTemplates.ImageTemplate)
                {
                    image.Folder = imageTemplates.Folder;
                    context.AddRange(image);
                }

                context.SaveChanges();
            }
        }
    }
}
