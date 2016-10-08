using System;
using System.Threading.Tasks;
using FluentAssertions;
using KP.GmailApi.Managers;
using KP.GmailApi.UnitTests.Extensions;
using Xunit;

namespace KP.GmailApi.UnitTests.UnitTests.TokenManagerTests
{
    /*public class SetupLoadTests : IDisposable
    {
        private const int ThreadCount = 500;
        private readonly OAuth2TokenManager _tokenManager;
        private const string ClientIdName = "ClientId1";
        private const string ClientSecretName = "ClientSecret1";
        private const string RefreshTokenName = "RefreshToken1";

        public SetupLoadTests()
        {
            _tokenManager.StaticTokens().Clear();// Clear static field
            _tokenManager = new OAuth2TokenManager(ClientIdName, ClientSecretName);
            _tokenManager.DeleteFolder();
        }

        [Fact]
        public void OneClientId_HasToken()
        {
            // Arrange
            Task[] tasks = new Task[ThreadCount];

            //Act
            for (int i = 0; i < ThreadCount; i++)
                tasks[i] = Task.Run(() => new OAuth2TokenManager(ClientIdName, ClientSecretName));

            Task.WaitAll(tasks);

            // Assert
            _tokenManager.StaticTokens().Count.Should().Be(1);
        }

        [Fact]
        public void MultipleClientIds_HasTokens()
        {
            // Arrange
            Task[] tasks = new Task[ThreadCount];

            for (int i = 0; i < ThreadCount; i++)
            {
                int i1 = i;

                //Act
                tasks[i] = Task.Run(() => new OAuth2TokenManager(ClientIdName + i1, ClientSecretName));
            }

            Task.WaitAll(tasks);

            // Assert
            _tokenManager.StaticTokens().Count.Should().Be(ThreadCount + 1);
        }

        [Fact]
        public void MultipleClientIds_CanSetupForce()
        {
            // Arrange
            Task[] tasks = new Task[ThreadCount];

            for (int i = 0; i < ThreadCount; i++)
            {
                int i1 = i;

                //Act
                tasks[i] = Task.Run(() => new OAuth2TokenManager(ClientIdName + i1, ClientSecretName).Setup(RefreshTokenName, true));
            }

            Task.WaitAll(tasks);

            // Assert
            _tokenManager.StaticTokens().Count.Should().Be(ThreadCount + 1);
        }

        [Fact]
        public void OneClientId_CanSetupForce()
        {
            // Arrange
            Task[] tasks = new Task[ThreadCount];

            for (int i = 0; i < ThreadCount; i++)
            {
                //Act
                tasks[i] = Task.Run(() => new OAuth2TokenManager(ClientIdName, ClientSecretName).Setup(RefreshTokenName, true));
            }

            Task.WaitAll(tasks);

            // Assert
            _tokenManager.StaticTokens().Count.Should().Be(1);
        }

        public void Dispose()
        {
            _tokenManager.DeleteFolder();
        }
    }*/
}
