using System.IO;

namespace MemeGenerator.DAL.FileReader
{
    public class FileReader : IFileReader
    {
        public byte[] GetImageData(string filePath)
        {
            byte[] imageData;

            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var binaryReader = new BinaryReader(fileStream);
                imageData = binaryReader.ReadBytes((int)binaryReader.BaseStream.Length);
            }

            return imageData;
        }
    }
}
