
using System;

using FluentAssertions;
using SwishDB.Util;
using Xunit;

namespace SwishDB.Tests.Util
{
    public class BufferReaderWriterTests : IDisposable
    {
        private const int Length = 32;

        private readonly BufferReader reader;
        private readonly BufferWriter writer;

        private readonly byte[] buffer = new byte[Length];


        public BufferReaderWriterTests()
        {
            reader = new BufferReader(buffer);
            writer = new BufferWriter(buffer);
        }


        [Fact]
        public void CanRoundTripByte()
        {
            // Arrange
            byte expected = 42;

            // Act
            writer.WriteByte(expected);

            var actual = reader.ReadByte();

            // Assert
            actual.Should().Be(expected);
        }


        [Fact]
        public void CanRouteTripAsciiString()
        {
            // Arrange
            string expected = "swish";

            // Act
            writer.WriteAsciiString(expected);

            var actual = reader.ReadAsciiString(expected.Length);

            // Assert
            actual.Should().Be(expected);
        }


        public void Dispose()
        {
            reader.Dispose();
            writer.Dispose();
        }
    }
}
