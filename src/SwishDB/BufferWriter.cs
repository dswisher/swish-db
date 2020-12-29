// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

using System;
using System.Text;

namespace SwishDB
{
    /// <summary>
    /// Class to assist with writing values to a byte buffer.
    /// </summary>
    public class BufferWriter : IDisposable
    {
        private readonly byte[] buffer;

        private int currentOffset;


        /// <summary>
        /// Initializes a new instance of the <see cref="BufferWriter"/> class.
        /// </summary>
        /// <param name="buffer">The buffer to which data will be written.</param>
        public BufferWriter(byte[] buffer)
        {
            this.buffer = buffer;
        }


        /// <summary>
        /// Gets the number of bytes written so far.
        /// </summary>
        public int Length
        {
            get { return currentOffset; }
        }


        /// <summary>
        /// Write a string to the buffer, using ASCII encoding.
        /// </summary>
        /// <remarks>
        /// This does NOT include length.
        /// </remarks>
        /// <param name="s">The string to write.</param>
        public void WriteAsciiString(string s)
        {
            var bytes = Encoding.ASCII.GetBytes(s);

            Write(bytes);
        }


        /// <summary>
        /// Write a byte to the buffer.
        /// </summary>
        /// <param name="b">The byte to write.</param>
        public void WriteByte(int b)
        {
            WriteByte((byte)b);
        }


        /// <summary>
        /// Write a byte to the buffer.
        /// </summary>
        /// <param name="b">The byte to write.</param>
        public void WriteByte(byte b)
        {
            buffer[currentOffset] = b;

            currentOffset += 1;
        }


        /// <summary>
        /// Write an array of bytes to the buffer.
        /// </summary>
        /// <param name="bytes">The bytes to write.</param>
        public void WriteBytes(byte[] bytes)
        {
            foreach (var b in bytes)
            {
                WriteByte(b);
            }
        }


        /// <summary>
        /// Write an unsigned short to the buffer.
        /// </summary>
        /// <param name="s">The unsigned short to write.</param>
        public void WriteUShort(ushort s)
        {
            WriteBytes(BitConverter.GetBytes(s));
        }


        /// <summary>
        /// Write an unsigned short to the buffer.
        /// </summary>
        /// <param name="s">The unsigned short to write.</param>
        public void WriteUShort(int s)
        {
            WriteUShort((ushort)s);
        }


        /// <summary>
        /// Write an unsigned int to the buffer.
        /// </summary>
        /// <param name="i">The unsigned int to write.</param>
        public void WriteUInt(uint i)
        {
            WriteBytes(BitConverter.GetBytes(i));
        }


        /// <summary>
        /// Write an unsigned long to the buffer.
        /// </summary>
        /// <param name="l">The unsigned long to write.</param>
        public void WriteULong(ulong l)
        {
            WriteBytes(BitConverter.GetBytes(l));
        }


        /// <summary>
        /// Write an array of bytes to the buffer.
        /// </summary>
        /// <remarks>
        /// This does NOT include length.
        /// </remarks>
        /// <param name="bytes">The bytes to write.</param>
        public void Write(byte[] bytes)
        {
            Buffer.BlockCopy(bytes, 0, buffer, currentOffset, bytes.Length);

            currentOffset += bytes.Length;
        }


        /// <inheritdoc/>
        public void Dispose()
        {
        }
    }
}
