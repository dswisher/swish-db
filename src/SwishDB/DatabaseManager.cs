// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

using System.IO;

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
        /// <returns>The database file.</returns>
        public static IDatabaseFile Open(string path)
        {
            var file = new DatabaseFile();

            if (File.Exists(path))
            {
                file.Open(path);
            }
            else
            {
                file.Create(path);
            }

            return file;
        }
    }
}
