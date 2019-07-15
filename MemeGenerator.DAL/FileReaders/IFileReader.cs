using System.Threading.Tasks;

namespace MemeGenerator.DAL.FileReaders
{
    public interface IFileReader
    {
        Task<ImageTemplateList> GetImageTemplateList();
        byte[] GetImageData(string filePath);
    }
}
