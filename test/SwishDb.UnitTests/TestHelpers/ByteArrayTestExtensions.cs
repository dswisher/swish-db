// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

namespace SwishDb.UnitTests.TestHelpers
{
    public static class ByteArrayTestExtensions
    {
        public static ByteArrayAssertions Should(this byte[] bytes)
        {
            return new ByteArrayAssertions(bytes);
        }
    }
}
