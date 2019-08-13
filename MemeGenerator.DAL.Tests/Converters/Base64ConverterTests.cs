using FluentAssertions;
using MemeGenerator.DAL.Converters;
using NUnit.Framework;
using System;

namespace MemeGenerator.DAL.Tests.Converters
{
    [TestFixture]
    public class Base64ConverterTests
    {
        private Base64Converter _base64Converter;
        private readonly byte[] _fakeImageBytes = { 0x20, 0x20, 0x20, 0x20 };
        private const string JpegBase64Prefix = "data:image/jpeg;base64";

        [SetUp]
        public void Setup()
        {
            _base64Converter = new Base64Converter();
        }

        [Test]
        public void ConvertToBase64_DataInByte_ReturnsDataInBase64Format()
        {
            var result = _base64Converter.ConvertToBase64(_fakeImageBytes);

            result.Should().Be(String.Concat(JpegBase64Prefix, Convert.ToBase64String(_fakeImageBytes)));
        }
    }
}
