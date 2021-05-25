using FluentAssertions;
using KP.GmailClient.Models;
using Xunit;

namespace KP.GmailClient.Tests.UnitTests.ModelTests
{
    public class HeaderTests
    {
        [Theory]
        [InlineData(null, HeaderName.Unknown)]
        [InlineData("", HeaderName.Unknown)]
        [InlineData("invalid", HeaderName.Unknown)]
        [InlineData("from", HeaderName.From)]
        [InlineData("to", HeaderName.To)]
        [InlineData("In-reply-to", HeaderName.InReplyTo)]
        public void CanParseImfHeader(string name, HeaderName expectedHeader)
        {
            // Arrange
            var header = new Header { Name = name };

            // Act
            HeaderName imfHeader = header.ImfHeader;

            // Assert
            imfHeader.Should().Be(expectedHeader);
        }
    }
}
