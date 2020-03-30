using System.Threading.Tasks;

namespace MemeGenerator.DAL.Services
{
    public interface IInitialMemesPopulator
    {
        Task Initialize();
    }
}
