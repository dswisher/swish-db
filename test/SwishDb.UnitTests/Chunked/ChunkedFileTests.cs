// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using SwishDb.Core.Chunked;
using SwishDb.UnitTests.TestHelpers;
using Xunit;

namespace SwishDb.UnitTests.Chunked
{
    public class ChunkedFileTests
    {
        [Fact]
        public async Task NewFileHasMagicString()
        {
            // Arrange
            byte[] bytes = null;

            // Act
            using (var stream = new MemoryStream())
            {
                var file = new ChunkedFile(stream);

                await file.InitializeAsync();

                bytes = stream.GetBuffer();
            }

            // Assert
            bytes.Should().HaveBytes(0, Encoding.ASCII.GetBytes(ChunkedFileHeader.Magic));
            bytes.Should().HaveBytes(ChunkedFileHeader.Magic.Length, BitConverter.GetBytes(0L));
            bytes.Should().HaveBytes(ChunkedFileHeader.Magic.Length + 8, BitConverter.GetBytes(0L));
        }


        [Fact]
        public async Task CanSaveAndRetrieve()
        {
            // Arrange
            const string expected = "Hello, world!";
            var expectedBytes = Encoding.ASCII.GetBytes(expected);

            string actual;

            byte[] bytes = null;

            // Act
            using (var stream = new MemoryStream())
            {
                var file = new ChunkedFile(stream);

                await file.InitializeAsync();

                var chunk = await file.AllocateAsync(500);

                await chunk.WriteAsync(0, Encoding.ASCII.GetBytes(expected));

                var actualBytes = await chunk.ReadAsync(0, expected.Length);

                actual = Encoding.ASCII.GetString(actualBytes);

                bytes = stream.GetBuffer();
            }

            // Assert
            actual.Should().Be(expected);

            bytes.Should().ContainBytes(expectedBytes);
        }
    }
}
