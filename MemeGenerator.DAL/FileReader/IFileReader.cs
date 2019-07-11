namespace MemeGenerator.DAL.FileReader
{
    public interface IFileReader
    {
        byte[] GetImageData(string filePath);
    }
}
