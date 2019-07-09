using System.Collections.Generic;
using MemeGenerator.Domain.Entities;

namespace MemeGenerator.DAL.Repositories
{
    public interface IImageTemplateRepository
    {
        IEnumerable<ImageTemplate> GetAll();
        void Insert(ImageTemplate imageTemplate);
        void Save();
    }
}
