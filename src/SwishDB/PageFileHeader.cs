// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

using System.IO;
using System.Threading;
using System.Threading.Tasks;

using SwishDB.Util;

namespace SwishDB
{
    /// <summary>
    /// Wrapper to read/write page file header.
    /// </summary>
    internal class PageFileHeader
    {
        private const int Length = 32;       // TODO - fine-tune and/or calc from fields?

        private readonly byte[] buffer = new byte[Length];


        /// <summary>
        /// Initializes a new instance of the <see cref="PageFileHeader"/> class.
        /// </summary>
        public PageFileHeader()
        {
            Magic = "swish-db";
            MajorVersion = 1;
            MinorVersion = 0;
        }


        /// <summary>
        /// Gets the string that appears is at the start of the header.
        /// </summary>
        public string Magic { get; private set; }

        /// <summary>
        /// Gets or sets the major version number.
        /// </summary>
        public byte MajorVersion { get; set; }

        /// <summary>
        /// Gets or sets the minor version number.
        /// </summary>
        public byte MinorVersion { get; set; }


        /// <summary>
        /// Loads the header from the specified stream, seeking as needed.
        /// </summary>
        /// <param name="stream">The stream from which the header is laoded.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that can be awaited.</returns>
        public async Task LoadAsync(Stream stream, CancellationToken cancellationToken)
        {
            // Go to the start, where the header should be located...
            stream.Seek(0, SeekOrigin.Begin);

            // ...and read it.
            await stream.ReadAsync(buffer, 0, buffer.Length);

            // Pick apart the bytes and populate the properties
            using (var reader = new BufferReader(buffer))
            {
                var len = reader.ReadByte();
                Magic = reader.ReadAsciiString(len);
                MajorVersion = reader.ReadByte();
                MinorVersion = reader.ReadByte();
            }
        }


        /// <summary>
        /// Saves the header to the specified stream, seeking as needed.
        /// </summary>
        /// <param name="stream">The stream to which the header is saved.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that can be awaited.</returns>
        public async Task SaveAsync(Stream stream, CancellationToken cancellationToken)
        {
            // Populate the buffer with the header contents
            using (var writer = new BufferWriter(buffer))
            {
                writer.WriteByte(Magic.Length);
                writer.WriteAsciiString(Magic);
                writer.WriteByte(MajorVersion);
                writer.WriteByte(MinorVersion);

                // TODO - rest of header
            }

            // Go to the start, where the header should be located...
            stream.Seek(0, SeekOrigin.Begin);

            // ...and write it.
            await stream.WriteAsync(buffer, 0, buffer.Length);
        }
    }
}
