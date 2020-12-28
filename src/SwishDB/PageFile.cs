// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SwishDB
{
    /// <summary>
    /// A file consisting of pages of data.
    /// </summary>
    public class PageFile : IDisposable
    {
        private Stream stream;


        /// <summary>
        /// Gets the page size.
        /// </summary>
        public int PageSize { get; private set; }


        /// <summary>
        /// Create a new file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that can be awaited.</returns>
        public async Task CreateAsync(string path, CancellationToken cancellationToken)
        {
            // Create the stream, read/write, no sharing.
            stream = new FileStream(path, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.None);

            // Set up
            // TODO - accept a config lambda to get page size and whatnot
            PageSize = 512;    // TODO - bump default to 4096 or 8192

            // Initialize and write the zero-page.
            var zeroPage = new ZeroPage
            {
                PageSize = PageSize
            };

            await WritePageAsync(0, zeroPage, cancellationToken);
        }


        /// <summary>
        /// Open an existing file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that can be awaited.</returns>
        public Task OpenAsync(string path, CancellationToken cancellationToken)
        {
            // TODO - accept a "mode" so we can open a file for read/write

            // Create the stream, read-only, read sharing.
            stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);

            // Read the zero-page to get the page size and the like.
            // TODO

            return Task.CompletedTask;
        }


        /// <summary>
        /// Write the specified page to the file.
        /// </summary>
        /// <param name="pageNum">The position within the file where the page should be written.</param>
        /// <param name="page">The content to write.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that can be awaited.</returns>
        public async Task WritePageAsync(int pageNum, Page page, CancellationToken cancellationToken)
        {
            // Sanity checks
            if (stream == null)
            {
                // TODO - create custom exception
                throw new Exception("Block store is not open!");
            }

            // TODO - sanity checks on page number and content size

            // Serialize the page to a byte stream.
            var bytes = page.Serialize(PageSize);

            // Seek to the proper spot in the file and write the content
            SeekToBlock(pageNum);

            await stream.WriteAsync(bytes, 0, PageSize);
        }


        /// <summary>
        /// Closes the page file.
        /// </summary>
        public void Close()
        {
            if (stream != null)
            {
                stream.Close();
                stream = null;
            }
        }


        /// <inheritdoc />
        public void Dispose()
        {
            Close();
        }


        private void SeekToBlock(int pageNum)
        {
            long pos = PageSize * pageNum;

            stream.Seek(pos, SeekOrigin.Begin);
        }
    }
}
