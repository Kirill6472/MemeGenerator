using System.IO;
using System.Linq;

namespace MemeGenerator.DAL.FileReader
{
    public class FileReader : IFileReader
    {
        public byte[] GetImageData(string filePath)
        {
            byte[] imageData;

            var files = Directory.GetFiles(filePath);
            var fileInfo = new FileInfo(files.First());
            var imageBytes = fileInfo.Length;

            using (var fileStream = new FileStream(files.First(), FileMode.Open, FileAccess.Read))
            {
                var binaryReader = new BinaryReader(fileStream);
                imageData = binaryReader.ReadBytes((int)imageBytes);
            }

            return imageData;
        }
    }
}
