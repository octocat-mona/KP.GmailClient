using System;
using FluentAssertions;
using KP.GmailClient.Builders;
using Xunit;

namespace KP.GmailClient.Tests.UnitTests.BuilderTests
{
    public class HistoryQueryStringBuilderTests
    {
        [Fact]
        public void CannotBuild_WithoutHistoryId()
        {
            // Act
            Action action = () => new HistoryQueryStringBuilder().Build();

            // Assert
            action.Should().Throw<Exception>();
        }
    }
}
