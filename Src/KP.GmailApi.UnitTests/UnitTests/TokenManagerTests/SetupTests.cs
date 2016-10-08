using System;
using FluentAssertions;
using KP.GmailApi.Managers;
using KP.GmailApi.Models;
using KP.GmailApi.UnitTests.Extensions;
using Xunit;

namespace KP.GmailApi.UnitTests.UnitTests.TokenManagerTests
{
    /*public class SetupTests : IDisposable
    {
        private readonly OAuth2TokenManager _tokenManager;
        private const string ClientIdName = "ClientId1";
        private const string ClientSecretName = "ClientSecret1";
        private const string RefreshTokenName = "RefreshToken1";

        public SetupTests()
        {
            _tokenManager.StaticTokens().Clear();// Clear static field
            _tokenManager = new OAuth2TokenManager(ClientIdName, ClientSecretName);
            _tokenManager.DeleteFolder();
        }

        [Fact]
        public void SetupToken_WithoutForce_HasToken()
        {
            // Act
            _tokenManager.Setup(RefreshTokenName, false);

            // Assert
            OAuth2Token setupToken = _tokenManager.Token();
            setupToken.Should().NotBeNull();
        }

        [Fact]
        public void SetupToken_WithForce_HasToken()
        {
            // Act
            _tokenManager.Setup(RefreshTokenName, true);

            // Assert
            OAuth2Token setupToken = _tokenManager.Token();
            setupToken.Should().NotBeNull();
        }

        [Fact]
        public void SetupToken_WithForce_HasTokenSetup()
        {
            // Arrange
            _tokenManager.Setup(RefreshTokenName, true);

            // Act
            bool hasTokenSetup = _tokenManager.HasTokenSetup();

            // Assert
            hasTokenSetup.Should().BeTrue();
        }

        [Fact]
        public void SetupToken_WithoutForce_HasTokenSetup()
        {
            // Arrange
            _tokenManager.Setup(RefreshTokenName, false);

            // Act
            bool hasTokenSetup = _tokenManager.HasTokenSetup();

            // Assert
            hasTokenSetup.Should().BeTrue();
        }

        public void Dispose()
        {
            _tokenManager.DeleteFolder();
        }
    }*/
}
