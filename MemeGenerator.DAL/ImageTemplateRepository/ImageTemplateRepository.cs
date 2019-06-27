using System.Collections.Generic;
using System.Linq;
using MemeGenerator.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MemeGenerator.DAL.ImageTemplateRepository
{
    public class ImageTemplateRepository : IImageTemplateRepository
    {
        private readonly MemeGeneratorDbContext context;

        public ImageTemplateRepository(MemeGeneratorDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<ImageTemplate> GetImageTemplates()
        {
            return context.ImageTemplates.ToList();
        }

        public ImageTemplate GetImageTemplateById(int imageTemplateId)
        {
            return context.ImageTemplates.Find(imageTemplateId);
        }

        public void InsertImageTemplate(ImageTemplate imageTemplate)
        {
            context.ImageTemplates.Add(imageTemplate);
        }

        public void DeleteImageTemplate(int imageTemplateId)
        {
            ImageTemplate imageTemplate = context.ImageTemplates.Find(imageTemplateId);
            context.ImageTemplates.Remove(imageTemplate);
        }

        public void UpdateImageTemplate(ImageTemplate imageTemplate)
        {
            context.Entry(imageTemplate).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public bool AllMigrationsApplied()
        {
            var applied = context.GetService<IHistoryRepository>().GetAppliedMigrations().Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>().Migrations.Select(m => m.Key);

            return !total.Except(applied).Any();
        }
    }
}
