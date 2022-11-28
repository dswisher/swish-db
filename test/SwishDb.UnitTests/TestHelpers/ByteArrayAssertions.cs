// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

using FluentAssertions.Execution;
using SwishDb.Core.Helpers;

namespace SwishDb.UnitTests.TestHelpers
{
    public class ByteArrayAssertions
    {
        private readonly byte[] actual;

        public ByteArrayAssertions(byte[] actual)
        {
            this.actual = actual;
        }


        public void HaveBytes(int offset, params byte[] expected)
        {
            // Make sure we have enough bytes to check
            var need = offset + expected.Length;

            Execute.Assertion
                .ForCondition(need < actual.Length)
                .FailWith("Expected to have at least {0} bytes to check, but only found {1}.", need, actual.Length);

            for (var i = 0; i < expected.Length; i++)
            {
                Execute.Assertion
                    .ForCondition(expected[i] == actual[i + offset])
                    .FailWith("Byte {0} should be {1}, but found {2}.", i + offset, expected[i], actual[i + offset]);
            }
        }


        public void ContainBytes(byte[] expected)
        {
            Execute.Assertion
                .ForCondition(actual.Contains(expected))
                .FailWith("Expected {0} to exist within {1}.", expected, actual);
        }
    }
}
