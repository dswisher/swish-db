// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SwishDB.Internal.Storage
{
    /// <summary>
    /// A data store consisting of small blocks of data.
    /// </summary>
    internal class BlockStore : IDisposable
    {
        private const byte StartOfPageByte = 0x53;  // S
        private const byte EndOfPageByte = 0x45;    // E

        private readonly FileHeader fileHeader = new FileHeader();

        private byte[] blockBuffer;
        private Stream stream;


        /// <summary>
        /// Initializes a new instance of the <see cref="BlockStore"/> class.
        /// </summary>
        private BlockStore()
        {
        }


        /// <summary>
        /// Gets the size of a block.
        /// </summary>
        public int BlockSize
        {
            get
            {
                return fileHeader.BlockSize;
            }
        }


        /// <summary>
        /// Opens an existing block store.
        /// </summary>
        /// <param name="stream">The stream used to access the existing block store.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that can be awaited.</returns>
        public static async Task<BlockStore> OpenAsync(Stream stream, CancellationToken cancellationToken)
        {
            // Create the store
            var store = new BlockStore();

            // If already open, throw.
            if (store.stream != null)
            {
                // TODO - create custom exception
                throw new Exception("Block store is already open!");
            }

            // Hang onto the stream
            store.stream = stream;

            // Read the header
            await store.fileHeader.LoadAsync(stream, cancellationToken);

            // Allocate the block buffer
            store.blockBuffer = new byte[store.BlockSize];

            // ...and return the result.
            return store;
        }


        /// <summary>
        /// Create a new block store on the specified stream.
        /// </summary>
        /// <param name="stream">The stream used to access the new block store.</param>
        /// <param name="blockSize">The size of a block.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that can be awaited.</returns>
        public static async Task<BlockStore> CreateAsync(Stream stream, ushort blockSize, CancellationToken cancellationToken)
        {
            // Create the store
            var store = new BlockStore();

            // If already open, throw.
            if (store.stream != null)
            {
                // TODO - create custom exception
                throw new Exception("Block store is already open!");
            }

            // Hang onto the stream and the block size
            store.stream = stream;

            // Populate the header
            store.fileHeader.BlockSize = blockSize;

            // Write the file header
            await store.fileHeader.SaveAsync(stream, cancellationToken);

            // Allocate the block buffer
            store.blockBuffer = new byte[store.BlockSize];

            // Return the result
            return store;
        }


        /// <summary>
        /// Writes the specified content to the specified block.
        /// </summary>
        /// <param name="blockNum">The number of the block to write.</param>
        /// <param name="content">The content to save to the block.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that can be awaited.</returns>
        public async Task WriteBlockAsync(int blockNum, byte[] content, CancellationToken cancellationToken)
        {
            // Sanity checks
            if (stream == null)
            {
                // TODO - create custom exception
                throw new Exception("Block store is not open!");
            }

            // TODO - sanity checks on block number and content size

            // Populate the block buffer
            using (var writer = new BufferWriter(blockBuffer))
            {
                writer.WriteByte(StartOfPageByte);
                writer.WriteUShort(content.Length);
                writer.WriteBytes(content);

                // TODO - pad with zeros
                // TODO - write CRC
                // TODO - write EndOfPageByte

                // HACK!
                blockBuffer[BlockSize - 1] = EndOfPageByte;
            }

            // Seek to the proper spot in the file and write the content
            SeekToBlock(blockNum);

            // Write the buffer
            await stream.WriteAsync(blockBuffer, 0, BlockSize);
        }


        /// <summary>
        /// Reads the content from the specified block.
        /// </summary>
        /// <param name="blockNum">The number of the block to write.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that can be awaited.</returns>
        public async Task<byte[]> ReadBlockAsync(int blockNum, CancellationToken cancellationToken)
        {
            // Sanity checks
            if (stream == null)
            {
                // TODO - create custom exception
                throw new Exception("Block store is not open!");
            }

            // Seek to the proper spot
            SeekToBlock(blockNum);

            // Load the buffer
            await stream.ReadAsync(blockBuffer, 0, BlockSize);

            // Pick out the content
            using (var reader = new BufferReader(blockBuffer))
            {
                var sob = reader.ReadByte();

                // TODO - verify start of block byte

                var len = reader.ReadUShort();
                var bytes = reader.ReadBytes(len);

                // TODO - verify CRC
                // TODO - verify end of block byte

                return bytes;
            }
        }


        /// <summary>
        /// Closes the store.
        /// </summary>
        public void Close()
        {
            if (stream != null)
            {
                stream.Close();
                stream = null;
            }
        }


        /// <inheritdoc/>
        public void Dispose()
        {
            Close();
        }


        private void SeekToBlock(int blockNum)
        {
            long pos = (BlockSize * blockNum) + 32;

            stream.Seek(pos, SeekOrigin.Begin);
        }
    }
}
