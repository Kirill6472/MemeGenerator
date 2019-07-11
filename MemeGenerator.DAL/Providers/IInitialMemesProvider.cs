using System.Threading.Tasks;

namespace MemeGenerator.DAL.Providers
{
    public interface IInitialMemesProvider
    {
        Task<ImageTemplateList> GetData();
    }
}
