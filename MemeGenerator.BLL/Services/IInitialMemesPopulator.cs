using System.Threading.Tasks;

namespace MemeGenerator.BLL.Services
{
    public interface IInitialMemesPopulator
    {
        Task Initialize();
    }
}
