using MemeGenerator.Models;

namespace MemeGenerator
{
    interface IDbInitializer
    {
        void Initialize(MemeGeneratorDbContext context);
    }
}
