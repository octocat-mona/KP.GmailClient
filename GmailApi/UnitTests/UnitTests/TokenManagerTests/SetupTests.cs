using FluentAssertions;
using GmailApi;
using GmailApi.Models;
using UnitTests.Extensions;
using Xunit;

namespace UnitTests.UnitTests.TokenManagerTests
{
    public class SetupTests
    {
        private const string ClientIdName = "ClientId1";
        private const string ClientSecretName = "ClientSecret1";
        private const string RefreshTokenName = "RefreshToken1";

        [Fact]
        public void CanSetup_WithoutForce()
        {
            // Arrange
            var tokenManager = new TokenManager(ClientIdName, ClientSecretName);
            tokenManager.DeleteFolder();

            // Act
            tokenManager.Setup(RefreshTokenName, false);

            // Assert
            Oauth2Token setupToken = tokenManager.Token();
            setupToken.Should().NotBeNull();
        }

        [Fact]
        public void CanSetup_WithForce()
        {
            // Arrange
            var tokenManager = new TokenManager(ClientIdName, ClientSecretName);
            tokenManager.DeleteFolder();

            // Act
            tokenManager.Setup(RefreshTokenName, true);

            // Assert
            Oauth2Token setupToken = tokenManager.Token();
            setupToken.Should().NotBeNull();
        }

        [Fact]
        public void WithToken_HasTokenSetup()
        {
            // Arrange
            var tokenManager = new TokenManager(ClientIdName, ClientSecretName);
            tokenManager.Setup(RefreshTokenName, false);

            // Act
            bool hasTokenSetup = tokenManager.HasTokenSetup();

            // Assert
            hasTokenSetup.Should().BeTrue();
        }
    }
}
