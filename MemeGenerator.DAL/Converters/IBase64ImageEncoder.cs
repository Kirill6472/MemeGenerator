namespace MemeGenerator.DAL.Converters
{
    public interface IBase64ImageEncoder
    {
        string Convert(byte[] imageBytes, string extension);
    }
}
