using System.Collections.Generic;
using MemeGenerator.Domain.Models;

namespace MemeGenerator.DAL.ImageTemplateRepository
{
    public interface IImageTemplateRepository
    {
        IEnumerable<ImageTemplate> GetImageTemplates();
        ImageTemplate GetImageTemplateById(int imageTemplateId);
        void InsertImageTemplate(ImageTemplate imageTemplate);
        void DeleteImageTemplate(int imageTemplateId);
        void UpdateImageTemplate(ImageTemplate imageTemplate);
        void Save();
        bool AllMigrationsApplied();
    }
}
