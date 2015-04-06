using System;
using GmailApi.Builders;
using GmailApi.DTO;
using NUnit.Framework;

namespace UnitTests.UnitTests.LabelServiceTests
{
    class LabelGetTests
    {
        [ExpectedException(typeof(ArgumentException))]
        [Test]
        public void EmptyLabel_ThrowsError()
        {
            // Act
            new LabelQueryStringBuilder()
                .SetRequestAction(LabelRequestAction.Get, string.Empty)
                .Build();
        }
    }
}
