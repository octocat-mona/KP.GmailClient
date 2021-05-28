using FluentAssertions;
using KP.GmailClient.Builders;
using Xunit;

namespace KP.GmailClient.UnitTests.BuilderTests
{
    public class MessageQueryStringBuilderTests
    {
        [Fact]
        public void WithEmptyThreadId_DoesNotIncludeThreadId()
        {
            // Act
            string queryString = new MessageQueryStringBuilder()
                .SetThreadId("")
                .Build();

            // Assert
            queryString.Should().BeEquivalentTo("messages");
        }

        [Fact]
        public void WithWhitespaceThreadId_DoesNotIncludeThreadId()
        {
            // Act
            string queryString = new MessageQueryStringBuilder()
                .SetThreadId("   ")
                .Build();

            // Assert
            queryString.Should().BeEquivalentTo("messages");
        }
    }
}
