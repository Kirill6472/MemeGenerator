using System.Threading.Tasks;

namespace MemeGenerator.Infrastructure.Providers
{
    public interface IInitialMemesProvider
    {
        Task<InitialMemesStorageStructure> GetData();
    }
}
