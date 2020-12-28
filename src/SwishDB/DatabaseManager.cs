// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SwishDB
{
    /// <summary>
    /// Main entry point for creating/opening SwishDB files.
    /// </summary>
    public static class DatabaseManager
    {
        /// <summary>
        /// Open the specified SwishDB file.
        /// </summary>
        /// <param name="path">The path to the file to open.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The newly opened file.</returns>
        public static async Task<Database> OpenAsync(string path, CancellationToken cancellationToken = default(CancellationToken))
        {
            var db = new Database();

            if (File.Exists(path))
            {
                // Open an existing file
                await db.OpenAsync(path, cancellationToken);
            }
            else
            {
                // Create a new file
                await db.CreateAsync(path, cancellationToken);
            }

            return db;
        }
    }
}
