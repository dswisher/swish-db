// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SwishDB
{
    /// <summary>
    /// Methods to open a new or existing database file.
    /// </summary>
    public static class DatabaseManager
    {
        /// <summary>
        /// Open a database file.
        /// </summary>
        /// <remarks>
        /// If the file does not exists, it is created and initialized.
        /// </remarks>
        /// <param name="path">The path of the file to create or open.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The database file.</returns>
        public static async Task<IDatabaseFile> OpenAsync(string path, CancellationToken cancellationToken = default(CancellationToken))
        {
            var file = new DatabaseFile();

            if (File.Exists(path))
            {
                await file.OpenAsync(path, cancellationToken);
            }
            else
            {
                await file.CreateAsync(path, cancellationToken);
            }

            return file;
        }
    }
}
