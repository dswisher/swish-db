// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

using System;

namespace SwishDB
{
    /// <summary>
    /// The base class for all page objects.
    /// </summary>
    public abstract class Page
    {
        /// <summary>
        /// Convert the page to a sequence of bytes.
        /// </summary>
        /// <param name="pageSize">The expected size of the page.</param>
        /// <returns>The sequence of bytes for the page.</returns>
        public byte[] Serialize(int pageSize)
        {
            // TODO - for the moment, just return a random sequence of bytes of the proper length
            var bytes = new byte[pageSize];
            var chaos = new Random();
            chaos.NextBytes(bytes);
            return bytes;
        }
    }
}
