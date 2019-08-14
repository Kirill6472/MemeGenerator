using System;

namespace MemeGenerator.DAL.Converters
{
    public class Base64Converter : IBase64Converter
    {
        public string ConvertToBase64(byte[] imageBytes, string imageExtension)
        {
            var base64Prefix = $"data:image/{imageExtension};base64";

            return String.Concat(base64Prefix, Convert.ToBase64String(imageBytes));
        }
    }
}
