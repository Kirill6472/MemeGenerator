namespace MemeGenerator.DAL.Converters
{
    public interface IBase64Converter
    {
        string ConvertToBase64(byte[] imageBytes, string imageExtension);
    }
}
