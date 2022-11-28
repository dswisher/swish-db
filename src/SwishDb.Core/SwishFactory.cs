// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SwishDb.Core.Impl;

namespace SwishDb.Core
{
    /// <summary>
    /// A factory to create or open databases.
    /// </summary>
    public static class SwishFactory
    {
        /// <summary>
        /// Create a new database, using the provided settings (or defaults, if null).
        /// </summary>
        /// <param name="path">The path where the database will be created.</param>
        /// <param name="settings">The settings to use when creating the database (or null to use defaults).</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>An implementation of the ISwishDb interface.</returns>
        public static async Task<ISwishDb> CreateAsync(string path, CreationSettings settings = null, CancellationToken cancellationToken = default)
        {
            // Make sure the file does not already exist. If it does, throw a nice exception.
            // TODO

            // Open the stream that will be used, and hand off the work to the stream-based overload.
            await using (var stream = new FileStream(path, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.None))
            {
                return await CreateAsync(stream, settings, cancellationToken);
            }
        }


        /// <summary>
        /// Create a new database on the specified stream.
        /// </summary>
        /// <param name="stream">The stream where the database will be created.</param>
        /// <param name="settings">The settings to use when creating the database (or null to use defaults).</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>An implementation of the ISwishDb interface.</returns>
        public static async Task<ISwishDb> CreateAsync(Stream stream, CreationSettings settings = null, CancellationToken cancellationToken = default)
        {
            // Create default settings, if none specified.
            settings ??= new CreationSettings();

            // Create the database wrapper, which is the actual implementation.
            var wrapper = new DatabaseWrapper(stream);

            // Initialize
            await wrapper.Initialize(settings, cancellationToken);

            // Create and return the wrapper
            return wrapper;
        }


        /// <summary>
        /// Open an existing database.
        /// </summary>
        /// <param name="path">The path to the database that should be opened.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>An implementation of the ISwishDb interface.</returns>
        public static async Task<ISwishDb> OpenAsync(string path, CancellationToken cancellationToken = default)
        {
            // TODO - implement me!
            await Task.CompletedTask;
            return null;
        }
    }
}
