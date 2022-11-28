// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SwishDb.Core.Chunked
{
    /// <summary>
    /// Represents a file that is broken into chunks, somewhat similar to the way the standard C library
    /// handles memory allocation.
    /// </summary>
    internal sealed class ChunkedFile : IDisposable
    {
        private readonly Stream stream;
        private readonly ChunkedFileHeader header = new ChunkedFileHeader();

        private bool disposed;


        /// <summary>
        /// Initializes a new instance of the <see cref="ChunkedFile"/> class.
        /// </summary>
        /// <param name="stream">The stream containing a chunked file (or empty file).</param>
        public ChunkedFile(Stream stream)
        {
            this.stream = stream;
        }


        /// <inheritdoc/>
        public void Dispose()
        {
            if (!disposed)
            {
                stream.Dispose();
                disposed = true;
            }
        }


        /// <summary>
        /// Initialize the chunked file.
        /// </summary>
        /// <remarks>
        /// Any existing data will be erased.
        /// </remarks>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task that can be awaited.</returns>
        public async Task InitializeAsync(CancellationToken cancellationToken = default)
        {
            // Write the header
            await header.WriteAsync(stream, cancellationToken);

            // Allocate the first chunk, and set it up with blocks
            // TODO
        }


        /// <summary>
        /// Allocate a new chunk within the file, of the specified size.
        /// </summary>
        /// <remarks>
        /// The length is the amount of data that can be stored. The allocated space will be larger,
        /// to accomodate the chunk header, footer, etc.
        /// </remarks>
        /// <param name="length">The length of the chunk to be allocated.</param>
        /// <returns>A Chunk.</returns>
        public async Task<Chunk> AllocateAsync(int length)
        {
            // TODO - implement me!
            await Task.CompletedTask;
            return new Chunk();
        }
    }
}
