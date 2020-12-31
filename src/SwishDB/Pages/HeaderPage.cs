// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

namespace SwishDB.Pages
{
    /// <summary>
    /// The second and third pages of every file.
    /// </summary>
    public class HeaderPage : Page
    {
        /// <summary>
        /// The page type for this page.
        /// </summary>
        public const ushort PageId = 0x01;


        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderPage"/> class.
        /// </summary>
        public HeaderPage()
        {
            PageType = PageId;
        }


        /// <inheritdoc />
        protected override string Summary
        {
            get
            {
                // TODO
                return string.Empty;
            }
        }


        /// <inheritdoc />
        protected override void SerializeContent(BufferWriter writer)
        {
            // TODO
        }


        /// <inheritdoc />
        protected override void DeserializeContent(BufferReader reader)
        {
            // TODO
        }
    }
}
