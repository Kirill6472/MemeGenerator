using System.Threading.Tasks;
using MemeGenerator.Infrastructure;

namespace MemeGenerator.DAL.Providers
{
    public interface IInitialMemesProvider
    {
        Task<InitialMemesStorageStructure> GetData();
    }
}
