using System.Threading.Tasks;

namespace MemeGenerator.DAL.FileReader
{
    public interface IFileReader
    {
        Task<ImageTemplateList> GetImageTemplateList();
        byte[] GetImageData(string filePath);
    }
}
