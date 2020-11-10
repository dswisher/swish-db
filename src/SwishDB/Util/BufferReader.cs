// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

using System;
using System.Text;

namespace SwishDB.Util
{
    /// <summary>
    /// Class to assist with reading values from a byte buffer.
    /// </summary>
    internal class BufferReader : IDisposable
    {
        private readonly byte[] buffer;

        private int currentOffset;


        /// <summary>
        /// Initializes a new instance of the <see cref="BufferReader"/> class.
        /// </summary>
        /// <param name="buffer">The buffer from which data will be read.</param>
        public BufferReader(byte[] buffer)
        {
            this.buffer = buffer;
        }


        /// <summary>
        /// Read a byte from the buffer.
        /// </summary>
        /// <returns>The byte that was read.</returns>
        public byte ReadByte()
        {
            return buffer[currentOffset++];
        }


        /// <summary>
        /// Read a string of the specified length from the buffer, using ASCII encoding.
        /// </summary>
        /// <param name="len">The length of the string to read.</param>
        /// <returns>The string that was read.</returns>
        public string ReadAsciiString(int len)
        {
            var s = Encoding.ASCII.GetString(buffer, currentOffset, len);

            currentOffset += len;

            return s;
        }


        /// <inheritdoc/>
        public void Dispose()
        {
        }
    }
}
