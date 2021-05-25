using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using KP.GmailClient.Common;
using Xunit;

namespace KP.GmailClient.Tests.UnitTests
{
    public class ErrorResponseParserTests
    {
        [Fact]
        public async Task CanParse()
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
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));

            // Act
            var exception = await ErrorResponseParser.ParseAsync(HttpStatusCode.OK, stream);

            // Assert
            exception.StatusCode.Should().Be(errorCode);
            exception.Message.Should().Be($"{errorNumber}: {mainMessage}");

            var gmailError = exception.Errors.Should().ContainSingle().Which;
            gmailError.Message.Should().Be(errorMessage);
            gmailError.Reason.Should().Be(errorReason);
            gmailError.Domain.Should().Be(errorDomain);
        }

        [Fact]
        public async Task WithInvalidContent_ReturnsOriginalInput()
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

            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
            var ex = new GmailApiException(statusCode, content);

            // Act
            var exception = await ErrorResponseParser.ParseAsync(statusCode, stream);

            // Assert
            exception.Should().BeEquivalentTo(ex);
        }

        [Fact]
        public async Task WithoutErrorRoot_ReturnsOriginalInput()
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

            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
            var ex = new GmailApiException(statusCode, content);

            // Act
            var exception = await ErrorResponseParser.ParseAsync(statusCode, stream);

            // Assert
            exception.Should().BeEquivalentTo(ex);
        }

        [Fact]
        public async Task WithInvalidJsonContent_ReturnsOriginalInput()
        {
            // Arrange
            const string content = "{}";
            const HttpStatusCode statusCode = HttpStatusCode.BadGateway;
            var ex = new GmailApiException(statusCode, content);
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));

            // Act
            var exception = await ErrorResponseParser.ParseAsync(statusCode, stream);

            // Assert
            exception.Should().BeEquivalentTo(ex);
        }
    }
}
