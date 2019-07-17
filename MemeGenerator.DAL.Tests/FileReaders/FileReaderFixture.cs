using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MemeGenerator.DAL.Tests.FileReaders
{
    public class FileReaderFixture
    {
        public async Task CreateFile(string path)
        {
            if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), path)))
            {
                File.Delete(Path.Combine(Directory.GetCurrentDirectory(), path));
            }

            using (File.Create(Path.Combine(Directory.GetCurrentDirectory(), path))) { }

            var serializeData = JsonConvert.SerializeObject("testData");

            using (var streamWriter = File.CreateText(Path.Combine(Directory.GetCurrentDirectory(), path)))
            {
                await streamWriter.WriteAsync(serializeData);
            }
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
