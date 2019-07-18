using System.IO;
using System.Threading.Tasks;

namespace MemeGenerator.DAL.Tests.FileReaders
{
    public class FileReaderFixture
    {
        public async Task CreateFile(string path, string data)
        {
            await File.WriteAllTextAsync(path, data);
        }

        public void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
