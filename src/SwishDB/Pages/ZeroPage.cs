// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

namespace SwishDB.Pages
{
    /// <summary>
    /// The first page of every file.
    /// </summary>
    public class ZeroPage : Page
    {
        /// <summary>
        /// The page type for this page.
        /// </summary>
        public const ushort PageId = 0x00;


        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroPage"/> class.
        /// </summary>
        public ZeroPage()
        {
            PageType = PageId;
        }


        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        public ushort PageSize { get; set; }


        /// <inheritdoc />
        protected override string Summary
        {
            get
            {
                return $"pageSize={PageSize}";
            }
        }


        /// <inheritdoc />
        protected override void SerializeContent(BufferWriter writer)
        {
            writer.WriteUShort(PageSize);
        }


        /// <inheritdoc />
        protected override void DeserializeContent(BufferReader reader)
        {
            PageSize = reader.ReadUShort();
        }
    }
}
