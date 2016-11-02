using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using FluentAssertions;
using KP.GmailClient.Builders;
using Xunit;

namespace KP.GmailClient.Tests.UnitTests.BuilderTests
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

        private static void AssertMessage(string parsedMessage, bool isBodyHtml = false)
        {
            string[] fields = parsedMessage.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, string> headers = fields
                .Select(field => field.Split(new[] { ": " }, StringSplitOptions.None))
                .ToDictionary(strings => strings[0], strings => strings[1]);

            string to = headers["To"];
            string subject = headers["Subject"];
            string contentTypeString = headers["Content-Type"];
            var contentType = new ContentType(contentTypeString);

            to.ShouldBeEquivalentTo(To);
            subject.ShouldBeEquivalentTo(Subject);

            // Charset is 'utf-8' with Mono
            contentType.CharSet.Should().BeOneOf("us-ascii", "utf-8");
            contentType.MediaType.ShouldBeEquivalentTo(isBodyHtml ? MediaTypeNames.Text.Html : MediaTypeNames.Text.Plain);
        }
    }
}
