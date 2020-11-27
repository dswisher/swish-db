// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SwishDB.Storage
{
   /// <summary>
    /// A data store consisting of small blocks of data.
    /// </summary>
    internal class BlockStore : IDisposable
    {
        private readonly FileHeader fileHeader = new FileHeader();
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

            // Return the result
            return store;
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
    }
}
