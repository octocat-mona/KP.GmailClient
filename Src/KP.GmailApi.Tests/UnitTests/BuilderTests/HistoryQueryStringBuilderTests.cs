using System;
using FluentAssertions;
using KP.GmailApi.Builders;
using Xunit;

namespace KP.GmailApi.Tests.UnitTests.BuilderTests
{
    public class HistoryQueryStringBuilderTests
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
