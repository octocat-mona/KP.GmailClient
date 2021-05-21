using System.Net;
using FluentAssertions;
using KP.GmailClient.Common;
using Xunit;

namespace KP.GmailClient.Tests.UnitTests
{
    public class ErrorResponseParserTests
    {
        [Fact]
        public void CanParse()
        {
            // Arrange
            const HttpStatusCode errorCode = HttpStatusCode.Forbidden;
            const int errorNumber = (int)errorCode;
            const string mainMessage = "User Rate Limit Exceeded.";
            const string errorMessage = "User Rate Limit Exceeded";
            const string errorReason = "userRateLimitExceeded";
            const string errorDomain = "usageLimits";

            string content = @"
{
    ""error"": {
        ""errors"": [{
                ""domain"": """ + errorDomain + @""",
                ""reason"": """ + errorReason + @""",
                ""message"": """ + errorMessage + @"""
            }
        ],
        ""code"": " + errorNumber + @",
        ""message"": """ + mainMessage + @"""
    }
}";

            // Act
            var exception = ErrorResponseParser.Parse(HttpStatusCode.OK, content);

            // Assert
            exception.StatusCode.Should().Be(errorCode);
            exception.Message.Should().Be($"{errorNumber}: {mainMessage}");

            var gmailError = exception.Errors.Should().ContainSingle().Which;
            gmailError.Message.Should().Be(errorMessage);
            gmailError.Reason.Should().Be(errorReason);
            gmailError.Domain.Should().Be(errorDomain);
        }

        [Fact]
        public void WithInvalidContent_ReturnsOriginalInput()
        {
            // Arrange
            const HttpStatusCode statusCode = HttpStatusCode.BadGateway;
            const string content = @"
{
    ""error"": {
        ""errors"": [{
                ""domain"": ""usageLimits"",
                ""reason"": ""userRateLimitExceeded"",
                ""message"": ""User Rate Limit Exceeded""
";

            var ex = new GmailApiException(statusCode, content);

            // Act
            var exception = ErrorResponseParser.Parse(statusCode, content);

            // Assert
            exception.Should().BeEquivalentTo(ex);
        }

        [Fact]
        public void WithoutErrorRoot_ReturnsOriginalInput()
        {
            // Arrange
            const HttpStatusCode statusCode = HttpStatusCode.BadGateway;
            const string content = @"
{
    ""errors"": [{
            ""domain"": ""usageLimits"",
            ""reason"": ""userRateLimitExceeded"",
            ""message"": ""User Rate Limit Exceeded""
        }
    ],
    ""code"": 403,
    ""message"": ""User Rate Limit Exceeded""
}";

            var ex = new GmailApiException(statusCode, content);

            // Act
            var exception = ErrorResponseParser.Parse(statusCode, content);

            // Assert
            exception.Should().BeEquivalentTo(ex);
        }

        [Fact]
        public void WithInvalidJsonContent_ReturnsOriginalInput()
        {
            // Arrange
            const string content = "{}";
            const HttpStatusCode statusCode = HttpStatusCode.BadGateway;
            var ex = new GmailApiException(statusCode, content);

            // Act
            var exception = ErrorResponseParser.Parse(statusCode, content);

            // Assert
            exception.Should().BeEquivalentTo(ex);
        }
    }
}
