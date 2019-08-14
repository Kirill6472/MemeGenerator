using System;
using NUnit.Framework;
using FluentAssertions;
using MemeGenerator.DAL.Converters;
using MemeGenerator.DAL.Exceptions;

namespace MemeGenerator.DAL.Tests.Converters
{
    [TestFixture]
    public class Base64ConverterTests
    {
        private Base64Converter _base64Converter;
        private readonly byte[] _fakeImageBytes = { 0x20, 0x20, 0x20, 0x20 };

        [SetUp]
        public void Setup()
        {
            _base64Converter = new Base64Converter();
        }

        [Test]
        public void ConvertToBase64_DataInByte_ReturnsDataInBase64Format()
        {
            const string imageExtension = "jpg";
            var base64Prefix = $"data:image/{imageExtension};base64";

            var result = _base64Converter.ConvertToBase64(_fakeImageBytes, imageExtension);

            result.Should().Be(String.Concat(base64Prefix, Convert.ToBase64String(_fakeImageBytes)));
        }

        [Test]
        public void ConvertToBase64_DataInByte_ThrowsException()
        {
            const string fileExtension = "txt";

            _base64Converter.Invoking(y => y.ConvertToBase64(_fakeImageBytes, fileExtension))
                .Should().Throw<Base64ConverterException>();
        }
    }
}
