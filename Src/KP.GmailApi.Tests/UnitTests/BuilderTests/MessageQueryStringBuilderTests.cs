using FluentAssertions;
using KP.GmailApi.Builders;
using Xunit;

namespace KP.GmailApi.Tests.UnitTests.BuilderTests
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
            queryString.ShouldBeEquivalentTo("messages");
        }

        [Fact]
        public void WithWhitespaceThreadId_DoesNotIncludeThreadId()
        {
            // Act
            string queryString = new MessageQueryStringBuilder()
                .SetThreadId("   ")
                .Build();

            // Assert
            queryString.ShouldBeEquivalentTo("messages");
        }
    }
}
