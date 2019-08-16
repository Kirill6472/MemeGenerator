using FluentAssertions;
using MemeGenerator.Infrastructure.Converters;
using MemeGenerator.Infrastructure.Exceptions;
using NUnit.Framework;

namespace MemeGenerator.Infrastructure.Tests.Converters
{
    [TestFixture]
    public class Base64ImageEncoderTests
    {
        private Base64ImageEncoder _base64Converter;

        [SetUp]
        public void Setup()
        {
            _base64Converter = new Base64ImageEncoder();
        }
        
        [TestCase(new byte[] { 0x20, 0x20 }, "jpg", "data:image/jpg;base64,ICA=")]
        [TestCase(new byte[] { 0x30, 0x50 }, "bmp", "data:image/bmp;base64,MFA=")]
        [TestCase(new byte[] { 0x20, 0x50 }, "gif", "data:image/gif;base64,IFA=")]
        [TestCase(new byte[] { 0x30, 0x50 }, "png", "data:image/png;base64,MFA=")]
        public void Convert_DataInByte_ReturnsImageInBase64Format(
            byte[] imageBytes, 
            string imageExtension, 
            string expectedResult)
        {
            var result = _base64Converter.Convert(imageBytes, imageExtension);

            result.Should().Be(expectedResult);
        }

        [Test]
        public void Convert_DataInByte_ThrowsException()
        {
            byte[] imageBytes = { 0x20, 0x20 };
            const string fileExtension = "txt";

            _base64Converter.Invoking(y => y.Convert(imageBytes, fileExtension))
                .Should().Throw<Base64ConverterException>()
                .WithMessage($"File has an invalid extension: {fileExtension}");
        }
    }
}
