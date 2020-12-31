// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SwishDB.Pages
{
    /// <summary>
    /// A file consisting of pages of data.
    /// </summary>
    public class PageFile : IDisposable
    {
        private const ushort MinPageSize = 512;

        private Stream stream;


        /// <summary>
        /// Gets the page size.
        /// </summary>
        public ushort PageSize { get; private set; }


        /// <summary>
        /// Gets the number of pages in the file.
        /// </summary>
        public int NumPages
        {
            get
            {
                return stream == null ? 0 : (int)Math.Ceiling(1.0 * stream.Length / PageSize);
            }
        }


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
            PageSize = 2 * MinPageSize;    // TODO - bump default to 4096 or 8192

            // Initialize and write the zero-page, which always uses the minimum page size.
            var zeroPage = new ZeroPage
            {
                PageSize = PageSize
            };

            await WritePageAsync(0, zeroPage, MinPageSize, cancellationToken);

            // Write the two header pages.
            var header1 = new HeaderPage();
            var header2 = new HeaderPage();

            await WritePageAsync(1, header1, cancellationToken);
            await WritePageAsync(2, header2, cancellationToken);
        }


        /// <summary>
        /// Open an existing file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that can be awaited.</returns>
        public async Task OpenAsync(string path, CancellationToken cancellationToken)
        {
            // TODO - accept a "mode" so we can open a file for read/write

            // Create the stream, read-only, read sharing.
            stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);

            // Read the zero page, which contains the page size and other useful bits. It always uses
            // the minimum page size.
            var zeroPage = (ZeroPage)await ReadPageAsync(0, MinPageSize, cancellationToken);

            PageSize = zeroPage.PageSize;
        }


        /// <summary>
        /// Write the specified page to the file.
        /// </summary>
        /// <param name="pageNum">The position within the file where the page should be written.</param>
        /// <param name="page">The content to write.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that can be awaited.</returns>
        public Task WritePageAsync(int pageNum, Page page, CancellationToken cancellationToken)
        {
            return WritePageAsync(pageNum, page, PageSize, cancellationToken);
        }


        /// <summary>
        /// Read the specified page from the file.
        /// </summary>
        /// <param name="pageNum">The position within the file where the page should be read.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that can be awaited to obtain the page.</returns>
        public Task<Page> ReadPageAsync(int pageNum, CancellationToken cancellationToken)
        {
            return ReadPageAsync(pageNum, pageNum == 0 ? MinPageSize : PageSize, cancellationToken);
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


        private async Task WritePageAsync(int pageNum, Page page, ushort pageSize, CancellationToken cancellationToken)
        {
            // Sanity checks
            if (stream == null)
            {
                // TODO - create custom exception
                throw new Exception("Block store is not open!");
            }

            // TODO - sanity checks on page number and content size

            // Serialize the page to a byte stream.
            var bytes = page.Serialize(pageSize);

            // Seek to the proper spot in the file and write the content
            SeekToPage(pageNum);

            await stream.WriteAsync(bytes, 0, pageSize);
        }


        private async Task<Page> ReadPageAsync(int pageNum, ushort pageSize, CancellationToken cancellationToken)
        {
            // Sanity checks
            if (stream == null)
            {
                // TODO - create custom exception
                throw new Exception("Block store is not open!");
            }

            // Seek to the proper spot
            SeekToPage(pageNum);

            // Read the bytes for the page
            var bytes = new byte[pageSize];

            await stream.ReadAsync(bytes, 0, pageSize);

            // Peek at the page type and build the proper page type.
            // TODO - a big switch statement is ugly...find a better way.
            var pageType = BitConverter.ToUInt16(bytes, 1);
            Page page = null;
            switch (pageType)
            {
                case ZeroPage.PageId:
                    page = new ZeroPage();
                    break;

                case HeaderPage.PageId:
                    page = new HeaderPage();
                    break;

                default:
                    throw new Exception($"Unhandled page type: {pageType}.");
            }

            // Have the page deserialize itself
            page.Deserialize(bytes);

            // And return whatever we've got.
            return page;
        }


        private void SeekToPage(int pageNum)
        {
            long pos = PageSize * pageNum;

            stream.Seek(pos, SeekOrigin.Begin);
        }
    }
}
