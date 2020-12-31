// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

using System;
using System.Text;

namespace SwishDB.Pages
{
    /// <summary>
    /// Class to assist with reading values from a byte buffer.
    /// </summary>
    public class BufferReader : IDisposable
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
        /// Read an unsigned short from the buffer.
        /// </summary>
        /// <returns>The unsigned short that was read.</returns>
        public ushort ReadUShort()
        {
            var val = BitConverter.ToUInt16(buffer, currentOffset);

            currentOffset += 2;

            return val;
        }


        /// <summary>
        /// Read bytes from the buffer.
        /// </summary>
        /// <param name="len">The length of the bytes to read.</param>
        /// <returns>The bytes that were read.</returns>
        public byte[] ReadBytes(int len)
        {
            var bytes = new byte[len];

            for (int i = 0; i < len; i++)
            {
                bytes[i] = buffer[currentOffset + i];
            }

            currentOffset += len;

            return bytes;
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
