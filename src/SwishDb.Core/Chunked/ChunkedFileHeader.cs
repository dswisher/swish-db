// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SwishDb.Core.Chunked
{
    /// <summary>
    /// Represents the data at the start of a chunked file.
    /// </summary>
    internal class ChunkedFileHeader
    {
        /// <summary>
        /// The special string written at the start of a chunked file to identify it.
        /// </summary>
        public const string Magic = "swishdb";

        private readonly byte[] buffer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChunkedFileHeader"/> class.
        /// </summary>
        internal ChunkedFileHeader()
        {
            buffer = new byte[Magic.Length + 16];

            Encoding.ASCII.GetBytes(Magic).CopyTo(buffer, 0);
        }


        /// <summary>
        /// Gets or sets a pointer (file offset) to the first chunk, or zero if there are no chunks.
        /// </summary>
        internal ulong FirstChunk { get; set; }

        /// <summary>
        /// Gets or sets a pointer (file offset) to the last chunk, or zero if there are no chunks.
        /// </summary>
        internal ulong LastChunk { get; set; }


        /// <summary>
        /// Write the header to the specified stream.
        /// </summary>
        /// <param name="stream">The stream on which to write the header.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task that can be awaited.</returns>
        internal async Task WriteAsync(Stream stream, CancellationToken cancellationToken)
        {
            // Push the latest values into the buffer
            BitConverter.GetBytes(FirstChunk).CopyTo(buffer, Magic.Length);
            BitConverter.GetBytes(LastChunk).CopyTo(buffer, Magic.Length + 8);

            // Write the buffer
            stream.Seek(0, SeekOrigin.Begin);

            await stream.WriteAsync(buffer, cancellationToken);
        }
    }
}
