// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SwishDB
{
    /// <summary>
    /// Standard implementation of IDatabaseFile.
    /// </summary>
    internal sealed class DatabaseFile : IDatabaseFile
    {
        private readonly PageFile pageFile;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseFile"/> class.
        /// </summary>
        public DatabaseFile()
        {
            pageFile = new PageFile();
        }


        /// <summary>
        /// Open an existing database file.
        /// </summary>
        /// <param name="path">The path of the file to open.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that can be awaited.</returns>
        public async Task OpenAsync(string path, CancellationToken cancellationToken)
        {
            // Can only open a file, if it already exists
            if (!File.Exists(path))
            {
                // TODO - create custom exception
                throw new Exception($"File does not exist: {path}.");
            }

            // Open the underlying page file
            await pageFile.OpenAsync(new FileStream(path, FileMode.Open, FileAccess.ReadWrite), cancellationToken);

            // Initialize the buffer manager
            // TODO

            // TODO - open the primary index
        }


        /// <summary>
        /// Create a new database file.
        /// </summary>
        /// <param name="path">The path of the file to create.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that can be awaited.</returns>
        public async Task CreateAsync(string path, CancellationToken cancellationToken)
        {
            // If trying to create a file, it should not already exist
            if (File.Exists(path))
            {
                // TODO - create custom exception
                throw new Exception($"File already exists: {path}.");
            }

            // Create the underlying page file
            await pageFile.CreateAsync(new FileStream(path, FileMode.CreateNew, FileAccess.ReadWrite), cancellationToken);

            // Initialize the buffer manager
            // TODO

            // TODO - create the primary index
        }


        /// <summary>
        /// Closes the file.
        /// </summary>
        public void Close()
        {
            pageFile.Close();
        }


        /// <inheritdoc/>
        public Task<byte[]> ReadObjectAsync(long key, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new System.NotImplementedException();
        }


        /// <inheritdoc/>
        public Task WriteObjectAsync(long key, byte[] bytes, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new System.NotImplementedException();
        }


        /// <inheritdoc/>
        public void Dispose()
        {
            // Flush buffers
            // TODO

            // Close the page file
            pageFile.Close();
        }
    }
}
