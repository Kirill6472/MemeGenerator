using System.IO;
using System.Threading.Tasks;

namespace MemeGenerator.DAL.FileReaders
{
    public class FileReader : IFileReader
    {
        public Task<byte[]> ReadBytes(string path)
        {
            var data = File.ReadAllBytesAsync(path);

            return data;
        }

        public Task<string> ReadString(string path)
        {
            var data = File.ReadAllTextAsync(path);

            return data;
        }
    }
}
