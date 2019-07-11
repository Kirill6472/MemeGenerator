using System.Threading.Tasks;

namespace MemeGenerator.BLL.Services.InitialMemesPopulator
{
    public interface IInitialMemesPopulator
    {
        Task InitializeAsync();
    }
}
