using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using KP.GmailApi.Builders;
using Xunit;

namespace KP.GmailApi.Tests.UnitTests.BuilderTests
{
    public class EmailMessageBuilderTests
    {
        private const string To = "to.first@domain.com, to.second@domain.com";
        private const string Subject = "Test subject";

        [Fact]
        public void CanBuild()
        {
            // Act
            string parsedMessage = new EmailMessageBuilder()
                .AddTo(To)
                .SetSubject(Subject)
                .Build();

            // Assert
            AssertMessage(parsedMessage);
        }

        [Fact]
        public void WithEmptyFields_AreNotIncluded()
        {
            // Arrange
            var builder = new EmailMessageBuilder()
                .AddTo(To)
                .SetSubject(Subject);

            // Act
            string parsedMessage = builder
                .AddReplyTo("")
                .AddCc("")
                .AddBcc("")
                .Build();

            // Assert
            AssertMessage(parsedMessage);
        }

        private static void AssertMessage(string parsedMessage)
        {
            string[] fields = parsedMessage.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, string> headers = fields
                .Select(field => field.Split(new[] { ": " }, StringSplitOptions.None))
                .ToDictionary(strings => strings[0], strings => strings[1]);

            string to = headers["To"];
            string subject = headers["Subject"];
            string contentType = headers["Content-Type"];

            to.ShouldBeEquivalentTo(To);
            subject.ShouldBeEquivalentTo(Subject);
            contentType.ShouldBeEquivalentTo("text/plain; charset=us-ascii");
        }
    }
}
