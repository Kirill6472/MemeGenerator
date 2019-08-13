using System;

namespace MemeGenerator.DAL.Converters
{
    public class Base64Converter : IBase64Converter
    {
        private const string JpegBase64Prefix = "data:image/jpeg;base64";

        public string ConvertToBase64(byte[] imageBytes)
        {
            return String.Concat(JpegBase64Prefix, Convert.ToBase64String(imageBytes));
        }
    }
}
