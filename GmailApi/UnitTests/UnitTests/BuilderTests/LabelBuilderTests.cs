using System;
using FluentAssertions;
using GmailApi.Builders;
using GmailApi.DTO;
using Xunit;

namespace UnitTests.UnitTests.BuilderTests
{
    public class LabelBuilderTests
    {
        [Fact]
        public void EmptyLabel_ThrowsError()
        {
            // Act
            Action action = () => new LabelQueryStringBuilder()
                .SetRequestAction(LabelRequestAction.Get, string.Empty)
                .Build();

            // Assert
            action.ShouldThrow<ArgumentException>();
        }
    }
}
