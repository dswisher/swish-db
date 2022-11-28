// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

namespace SwishDb.Core.Helpers
{
    /// <summary>
    /// Helpers functions for working with byte arrays.
    /// </summary>
    public static class ByteArrayExtensions
    {
        /// <summary>
        /// Checks if a larger byte array contains a smaller byte array.
        /// </summary>
        /// <param name="big">The larger array to check to see if it contains the smaller array.</param>
        /// <param name="sub">The smaller array that may be inside the larger array.</param>
        /// <returns>True if found, false if not.</returns>
        public static bool Contains(this byte[] big, byte[] sub)
        {
            for (var i = 0; i <= big.Length - sub.Length; i++)
            {
                var found = true;
                for (var j = 0; j < sub.Length && found; j++)
                {
                    if (big[i + j] != sub[j])
                    {
                        found = false;
                    }
                }

                if (found)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
