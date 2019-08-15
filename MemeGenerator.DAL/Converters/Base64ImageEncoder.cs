using System;
using System.Collections.Generic;
using System.Linq;
using MemeGenerator.DAL.Exceptions;

namespace MemeGenerator.DAL.Converters
{
    public class Base64ImageEncoder : IBase64ImageEncoder
    {
        public string Convert(byte[] data, string extension)
        {
            AssertFileExtensionIsAcceptable(extension);

            var base64Prefix = $"data:image/{extension};base64,";

            return string.Concat(base64Prefix, System.Convert.ToBase64String(data));
        }

        private static void AssertFileExtensionIsAcceptable(string extension)
        {
            if (!GetImageExtension().Contains(extension))
            {
                throw new Base64ConverterException($"File has an invalid extension: {extension}");
            }
        }

        private static IEnumerable<string> GetImageExtension()
        {
            yield return "jpg";
            yield return "bmp";
            yield return "gif";
            yield return "png";
        }
    }
}
