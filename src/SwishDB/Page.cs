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
        private const byte StartOfPageByte = 0x53;  // S
        private const byte EndOfPageByte = 0x45;    // E


        /// <summary>
        /// Gets or sets the page type for this page.
        /// </summary>
        public ushort PageType { get; protected set; }


        /// <summary>
        /// Gets a summary for this page. Mainly intended for debugging.
        /// </summary>
        protected abstract string Summary { get; }


        /// <summary>
        /// Convert the page to a sequence of bytes.
        /// </summary>
        /// <param name="pageSize">The expected size of the page.</param>
        /// <returns>The sequence of bytes for the page.</returns>
        public byte[] Serialize(ushort pageSize)
        {
            // TODO - can we/should we allocate this once and reuse it?
            // Set up the byte buffer.
            var bytes = new byte[pageSize];
            using (var writer = new BufferWriter(bytes))
            {
                // Set up the page header
                writer.WriteByte(StartOfPageByte);
                writer.WriteUShort(PageType);

                // Write the page content
                SerializeContent(writer);

                // Pad out to end
                while (writer.Length < pageSize - 5)
                {
                    writer.WriteByte(0x12);
                }

                // Write the page footer
                writer.WriteUInt(0x12345678);   // TODO - should be a checksum!
                writer.WriteByte(EndOfPageByte);
            }

            // Return the result
            return bytes;
        }


        /// <summary>
        /// Populate the object from the specified array of bytes.
        /// </summary>
        /// <param name="bytes">The bytes from which to extract the page info.</param>
        public void Deserialize(byte[] bytes)
        {
            using (var reader = new BufferReader(bytes))
            {
                // Verify the start byte and skip over the page type (as we've already constructed the class by peeking at it)
                var start = reader.ReadByte();
                if (start != StartOfPageByte)
                {
                    // TODO - custom exception
                    throw new Exception($"Expected StartOfPageByte ({StartOfPageByte}) but found {start}.");
                }

                reader.ReadUShort();  // page type

                // Verify the end of page byte
                var end = bytes[bytes.Length - 1];
                if (end != EndOfPageByte)
                {
                    // TODO - custom exception
                    throw new Exception($"Expected EndOfPageByte ({EndOfPageByte}) but found {end}.");
                }

                // Verify the checksum of the page.
                // TODO

                // Have the page read its content
                DeserializeContent(reader);
            }
        }


        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.GetType().Name}: {Summary}";
        }


        /// <summary>
        /// Write the content for the specific type of page.
        /// </summary>
        /// <param name="writer">The writer used to write to the buffer.</param>
        protected abstract void SerializeContent(BufferWriter writer);


        /// <summary>
        /// Read the content for the specific type of page.
        /// </summary>
        /// <param name="reader">The reader used to read from the buffer.</param>
        protected abstract void DeserializeContent(BufferReader reader);
    }
}
