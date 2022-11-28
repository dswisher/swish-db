// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using SwishDb.Core;
using SwishDb.UnitTests.TestHelpers;
using Xunit;

namespace SwishDb.UnitTests.Core
{
    public class CreationTests
    {
        [Fact]
        public async Task NewDbHasMagicString()
        {
            // Arrange
            ISwishDb db = null;
            byte[] bytes = null;

            // Act
            using (var stream = new MemoryStream())
            {
                db = await SwishFactory.CreateAsync(stream);

                bytes = stream.GetBuffer();
            }

            // Assert
            db.Should().NotBeNull();

            bytes.Should().HaveBytes(0, 0x73, 0x77, 0x69, 0x73, 0x68, 0x64, 0x62);  // swishdb
        }
    }
}
