using System.IO;
using System.Threading.Tasks;

namespace MemeGenerator.Infrastructure.FileReaders
{
    public class FileReader : IFileReader
    {
        public Task<byte[]> ReadBytes(string path)
        {
            return File.ReadAllBytesAsync(path);
        }

        public Task<string> ReadString(string path)
        {
            return File.ReadAllTextAsync(path);
        }
    }
}
