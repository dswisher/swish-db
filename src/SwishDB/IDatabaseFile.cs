// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

using System;
using System.Threading;
using System.Threading.Tasks;

namespace SwishDB
{
    /// <summary>
    /// Public operations on a database.
    /// </summary>
    public interface IDatabaseFile : IDisposable
    {
        /// <summary>
        /// Put an object in the database.
        /// </summary>
        /// <remarks>
        /// If the object already exists, and space permits, the existing allocated space is overwritten,
        /// otherwise new space is allocated and the old space is put on the free list.
        /// </remarks>
        /// <param name="key">The key of the object to write.</param>
        /// <param name="bytes">The bytes that represent the object.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that can be awaited.</returns>
        Task WriteObjectAsync(long key, byte[] bytes, CancellationToken cancellationToken = default(CancellationToken));


        /// <summary>
        /// Get an object from the database.
        /// </summary>
        /// <param name="key">The key of the object to write.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The bytes reresenting the object, or null if the object does not exist.</returns>
        // TODO - should this return a memory stream or ?
        Task<byte[]> ReadObjectAsync(long key, CancellationToken cancellationToken = default(CancellationToken));
    }
}
