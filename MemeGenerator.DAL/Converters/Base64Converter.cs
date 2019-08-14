using System;
using System.Collections.Generic;
using MemeGenerator.DAL.Exceptions;

namespace MemeGenerator.DAL.Converters
{
    public class Base64Converter : IBase64Converter
    {
        private readonly List<string> _imageTypes = new List<string> { "jpg", "bmp", "gif", "png" };

        public string ConvertToBase64(byte[] data, string extension)
        {
            AssertThatExtensionImage(extension);

            var base64Prefix = $"data:image/{extension};base64";

            return String.Concat(base64Prefix, Convert.ToBase64String(data));
        }

        private void AssertThatExtensionImage(string extension)
        {
            if (!_imageTypes.Contains(extension))
            {
                throw new Base64ConverterException($"File has an invalid extension: {extension}");
            }
        }
    }
}
