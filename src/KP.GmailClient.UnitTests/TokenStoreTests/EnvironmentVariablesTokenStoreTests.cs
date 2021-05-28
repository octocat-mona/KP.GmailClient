using System;
using System.Threading.Tasks;
using FluentAssertions;
using KP.GmailClient.Authentication.TokenStores;
using KP.GmailClient.Models;
using Xunit;

namespace KP.GmailClient.UnitTests.TokenStoreTests
{
    public class EnvironmentVariablesTokenStoreTests
    {
        [Fact]
        public async Task CanGetStoredToken()
        {
            // Arrange
            var tokenStore = CreateTokenStore();
            var storedToken = new OAuth2Token
            {
                AccessToken = Guid.NewGuid().ToString("N"),
                RefreshToken = Guid.NewGuid().ToString("N"),
                ExpirationDate = DateTimeOffset.UtcNow.AddHours(1)
            };

            await tokenStore.StoreTokenAsync(storedToken);

            // Act
            var token = await tokenStore.GetTokenAsync();

            // Assert
            token.Should().BeEquivalentTo(storedToken);
        }

        [Fact]
        public async Task WithoutStoredToken_ThrowsException()
        {
            // Arrange
            var tokenStore = CreateTokenStore();

            // Act
            Task<OAuth2Token> GetToken() => tokenStore.GetTokenAsync();

            // Assert
            await Assert.ThrowsAsync<Exception>(GetToken);
        }

        private static EnvironmentVariablesTokenStore CreateTokenStore()
        {
            return new()
            {
                AccessTokenKeyName = "access_token_" + Guid.NewGuid().ToString("N"),
                RefreshTokenKeyName = "refresh_token_" + Guid.NewGuid().ToString("N"),
                ExpiryDateTokenKeyName = "expiry_" + Guid.NewGuid().ToString("N")
            };
        }
    }
}
