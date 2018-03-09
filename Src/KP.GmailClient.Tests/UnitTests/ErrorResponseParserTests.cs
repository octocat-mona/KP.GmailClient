using System.Net;
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
            const HttpStatusCode statusCode = HttpStatusCode.OK;
            const string content = "{\"error\":{\"errors\":[{\"domain\":\"global\",\"reason\":\"notFound\",\"message\":\"Not Found\"}],\"code\":404,\"message\":\"Not Found\"}}";

            // Act
            var exception = await ErrorResponseParser.ParseAsync(statusCode, content);

            // Assert
            exception.StatusCode.Should().Be(HttpStatusCode.NotFound);
            exception.Errors.Should().ContainSingle()
                .Which.Message.Should().Be("Not Found");
        }

        [Fact]
        public async Task WithInvalidContent_ReturnsException()
        {
            // Arrange
            const string content = "[]";
            const HttpStatusCode statusCode = HttpStatusCode.SeeOther;
            var ex = new GmailException(statusCode, content);

            // Act
            var exception = await ErrorResponseParser.ParseAsync(statusCode, content);

            // Assert
            exception.Should().BeEquivalentTo(ex);
        }
    }
}
