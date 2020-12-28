// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

using System;
using System.Threading;
using System.Threading.Tasks;

namespace SwishDB
{
    /// <summary>
    /// A SwishDB database.
    /// </summary>
    public class Database : IDisposable
    {
        private readonly PageFile pageFile;


        /// <summary>
        /// Initializes a new instance of the <see cref="Database"/> class.
        /// </summary>
        public Database()
        {
            pageFile = new PageFile();
        }


        /// <summary>
        /// Create a new file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that can be awaited.</returns>
        public async Task CreateAsync(string path, CancellationToken cancellationToken)
        {
            // Initialize the page file
            await pageFile.CreateAsync(path, cancellationToken);

            // TODO
        }


        /// <summary>
        /// Open an existing file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that can be awaited.</returns>
        public Task OpenAsync(string path, CancellationToken cancellationToken)
        {
            // TODO
            return Task.CompletedTask;
        }


        /// <inheritdoc />
        public void Dispose()
        {
            pageFile.Dispose();
        }
    }
}
