
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;
using SwishDB.Storage;
using SwishDB.Tests.TestHelpers;
using Xunit;

namespace SwishDB.Tests.Storage
{
    public class BlockStoreTests
    {
        private readonly CancellationToken token = default(CancellationToken);


        [Fact]
        public async Task BlockSizeIsPreserved()
        {
            // Arrange
            const int blockSize = 6543;     // arbitrary number

            byte[] buffer = null;

            // Create a block store and verify block size
            using (var memStream = new MemoryStream())
            {
                var store = await BlockStore.CreateAsync(memStream, blockSize, token);

                store.BlockSize.Should().Be(blockSize);

                store.Dispose();

                buffer = memStream.GetBuffer();
            }

            // TODO - dump the buffer
            Console.WriteLine(buffer.HexDump(64));

            // Reopen the block store and verify the block size
            using (var memStream = new MemoryStream(buffer))
            {
                var store = await BlockStore.OpenAsync(memStream, token);

                store.BlockSize.Should().Be(blockSize);
            }
        }
    }
}
