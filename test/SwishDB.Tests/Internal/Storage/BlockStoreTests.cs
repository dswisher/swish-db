
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;
using SwishDB.Internal.Storage;
using SwishDB.Tests.TestHelpers;
using Xunit;

namespace SwishDB.Tests.Internal.Storage
{
    public class BlockStoreTests
    {
        private const int BlockSize = 64;

        private readonly CancellationToken token = default(CancellationToken);

        private byte[] buffer;


        [Fact]
        public async Task BlockSizeIsPreserved()
        {
            // Create a block store and verify block size
            using (var memStream = new MemoryStream())
            {
                var store = await BlockStore.CreateAsync(memStream, BlockSize, token);

                store.BlockSize.Should().Be(BlockSize);

                store.Dispose();

                buffer = memStream.GetBuffer();
            }

            // Reopen the block store and verify the block size
            using (var memStream = new MemoryStream(buffer))
            {
                var store = await BlockStore.OpenAsync(memStream, token);

                store.BlockSize.Should().Be(BlockSize);
            }
        }


        [Fact]
        public async Task CanWriteReadBlock()
        {
            // Arrange
            const int expectedNum = 0x12345678;

            var expectedBytes = BitConverter.GetBytes(expectedNum);

            // Write a block
            using (var memStream = new MemoryStream())
            using (var store = await BlockStore.CreateAsync(memStream, BlockSize, token))
            {
                await store.WriteBlockAsync(0, expectedBytes, token);

                store.Close();
                buffer = memStream.GetBuffer();
            }

            Console.WriteLine(buffer.HexDump(128));

            // Read the block
            using (var memStream = new MemoryStream(buffer))
            using (var store = await BlockStore.OpenAsync(memStream, token))
            {
                var actualBytes = await store.ReadBlockAsync(0, token);

                var actualNum = BitConverter.ToInt32(actualBytes, 0);

                actualNum.Should().Be(expectedNum);
            }
        }


        // TODO - verify corrupted block is detected
    }
}
