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
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseFile"/> class.
        /// </summary>
        public DatabaseFile()
        {
        }


        /// <summary>
        /// Open an existing database file.
        /// </summary>
        /// <param name="path">The path of the file to open.</param>
        public void Open(string path)
        {
            // Can only open a file, if it already exists
            if (!File.Exists(path))
            {
                // TODO - create custom exception
                throw new Exception($"File does not exist: {path}.");
            }

            // Open the underlying page file
            // TODO
        }


        /// <summary>
        /// Create a new database file.
        /// </summary>
        /// <param name="path">The path of the file to create.</param>
        public void Create(string path)
        {
            // If trying to create a file, it should not already exist
            if (File.Exists(path))
            {
                // TODO - create custom exception
                throw new Exception($"File already exists: {path}.");
            }

            // Create the underlying page file
            // TODO
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
        }
    }
}
