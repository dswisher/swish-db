using System.Text;
using FluentAssertions;
using SwishDb.Core.Helpers;
using Xunit;

namespace SwishDb.UnitTests.Helpers
{
    public class ByteArrayExtensionsTests
    {
        [Theory]
        [InlineData("ABCD", "AB")]
        [InlineData("ABCD", "BC")]
        [InlineData("ABCD", "CD")]
        public void PositiveContains(string bigStr, string subStr)
        {
            // Arrange
            var bigBytes = Encoding.ASCII.GetBytes(bigStr);
            var subBytes = Encoding.ASCII.GetBytes(subStr);

            // Act and assert
            bigBytes.Contains(subBytes).Should().BeTrue();
        }


        [Theory]
        [InlineData("ABCD", "ZZ")]
        public void NegativeContains(string bigStr, string subStr)
        {
            // Arrange
            var bigBytes = Encoding.ASCII.GetBytes(bigStr);
            var subBytes = Encoding.ASCII.GetBytes(subStr);

            // Act and assert
            bigBytes.Contains(subBytes).Should().BeFalse();
        }
    }
}
