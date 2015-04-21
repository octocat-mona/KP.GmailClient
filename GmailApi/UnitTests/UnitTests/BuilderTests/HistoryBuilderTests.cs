using System;
using FluentAssertions;
using GmailApi.Builders;
using Xunit;

namespace UnitTests.UnitTests.BuilderTests
{
    public class HistoryBuilderTests
    {
        [Fact]
        public void CannotBuild_WithoutHistoryId()
        {
            // Act
            Action action = () => new HistoryQueryStringBuilder().Build();

            // Assert
            action.ShouldThrow<Exception>();
        }
    }
}
