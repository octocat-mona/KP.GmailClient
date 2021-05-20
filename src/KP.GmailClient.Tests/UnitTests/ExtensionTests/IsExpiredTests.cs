using System;
using FluentAssertions;
using KP.GmailClient.Extensions;
using KP.GmailClient.Models;
using Xunit;

namespace KP.GmailClient.Tests.UnitTests.ExtensionTests
{
    public class IsExpiredTests
    {
        [Fact]
        public void WithNullToken_ReturnsTrue()
        {
            // Arrange
            OAuth2Token token = null;

            // Act
            bool isExpired = token.IsExpired();

            // Assert
            isExpired.Should().BeTrue();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void WithoutAccessToken_ReturnsTrue(string accessToken)
        {
            // Arrange
            var token = new OAuth2Token
            {
                AccessToken = accessToken,
                ExpirationDate = DateTimeOffset.MaxValue
            };

            // Act
            bool isExpired = token.IsExpired();

            // Assert
            isExpired.Should().BeTrue();
        }

        [Fact]
        public void WithPastExpiryDate_ReturnsTrue()
        {
            // Arrange
            var token = new OAuth2Token
            {
                AccessToken = Guid.NewGuid().ToString("N"),
                ExpirationDate = DateTimeOffset.UtcNow.AddSeconds(-1)
            };

            // Act
            bool isExpired = token.IsExpired();

            // Assert
            isExpired.Should().BeTrue();
        }

        [Fact]
        public void WithFutureExpiryDate_ReturnsFalse()
        {
            // Arrange
            var token = new OAuth2Token
            {
                AccessToken = Guid.NewGuid().ToString("N"),
                ExpirationDate = DateTimeOffset.UtcNow.AddSeconds(1)
            };

            // Act
            bool isExpired = token.IsExpired();

            // Assert
            isExpired.Should().BeFalse();
        }
    }
}
