// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SwishDB
{
   /// <summary>
    /// A file with a header and pages (blocks) of data.
    /// </summary>
    internal class PageFile : IDisposable
    {
        private readonly PageFileHeader fileHeader = new PageFileHeader();
        private Stream stream;

        /// <summary>
        /// Initializes a new instance of the <see cref="PageFile"/> class.
        /// </summary>
        public PageFile()
        {
        }


        /// <summary>
        /// Opens an existing page file.
        /// </summary>
        /// <param name="stream">The stream used to access the existing pagefile.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that can be awaited.</returns>
        public async Task OpenAsync(Stream stream, CancellationToken cancellationToken)
        {
            // If already open, throw.
            if (this.stream != null)
            {
                // TODO - create custom exception
                throw new Exception("PageFile is already open!");
            }

            // Hang onto the stream
            this.stream = stream;

            // Read the header
            await fileHeader.LoadAsync(stream, cancellationToken);
        }


        /// <summary>
        /// Create a new database file.
        /// </summary>
        /// <param name="stream">The stream used to access the new pagefile.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that can be awaited.</returns>
        public async Task CreateAsync(Stream stream, CancellationToken cancellationToken)
        {
            // If already open, throw.
            if (this.stream != null)
            {
                // TODO - create custom exception
                throw new Exception("PageFile is already open!");
            }

            // Hang onto the stream
            this.stream = stream;

            // Populate the header
            // TODO - set header values

            // Write the file header
            await fileHeader.SaveAsync(stream, cancellationToken);
        }


        /// <summary>
        /// Closes the file.
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
