using MemeGenerator.Models;
using MemeGenerator.Services;

namespace MemeGenerator
{
    interface IDbInitializer
    {
        bool AllMigrationsApplied(MemeGeneratorDbContext context);

        void Initialize(MemeGeneratorDbContext context, ImageTemplateList imageTemplates);
    }
}
