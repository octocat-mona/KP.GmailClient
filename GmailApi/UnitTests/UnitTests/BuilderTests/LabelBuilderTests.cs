using System;
using GmailApi.Builders;
using GmailApi.DTO;
using Shouldly;
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
            Should.Throw<ArgumentException>(action);
        }
    }
}
