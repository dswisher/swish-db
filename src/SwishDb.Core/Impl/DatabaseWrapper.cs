// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SwishDb.Core.Chunked;

namespace SwishDb.Core.Impl
{
    /// <summary>
    /// An implementation of the ISwishDb interface.
    /// </summary>
    internal sealed class DatabaseWrapper : ISwishDb
    {
        private readonly ChunkedFile file;
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseWrapper"/> class.
        /// </summary>
        /// <param name="stream">The stream containing the database (which may be empty).</param>
        public DatabaseWrapper(Stream stream)
        {
            file = new ChunkedFile(stream);
        }


        /// <inheritdoc/>
        public void Dispose()
        {
            if (!disposed)
            {
                file.Dispose();
                disposed = true;
            }
        }


        /// <summary>
        /// Initialize a new database.
        /// </summary>
        /// <remarks>
        /// This will erase any data already present in the database.
        /// </remarks>
        /// <param name="settings">Settings that influence how the database is configured.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task to be awaited.</returns>
        internal async Task Initialize(CreationSettings settings, CancellationToken cancellationToken)
        {
            // Initialize the chunked file
            await file.InitializeAsync(cancellationToken);

            // Set up and initialize the block manager
            // TODO
        }
    }
}
