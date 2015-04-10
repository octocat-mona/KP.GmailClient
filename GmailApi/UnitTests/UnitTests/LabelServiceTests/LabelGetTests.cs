using System;
using GmailApi.Builders;
using GmailApi.DTO;
using Xunit;

namespace UnitTests.UnitTests.LabelServiceTests
{
    public class LabelGetTests
    {
        [Fact]
        public void EmptyLabel_ThrowsError()
        {
            // Act
            Action action = () => new LabelQueryStringBuilder()
                .SetRequestAction(LabelRequestAction.Get, string.Empty)
                .Build();

            // Assert
            Assert.Throws<ArgumentException>(new Assert.ThrowsDelegate(action));
        }
    }
}
