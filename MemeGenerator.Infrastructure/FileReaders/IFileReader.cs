using System.Threading.Tasks;

namespace MemeGenerator.Infrastructure.FileReaders
{
    public interface IFileReader
    {
        Task<byte[]> ReadBytes(string path);
        Task<string> ReadString(string path);
    }
}
