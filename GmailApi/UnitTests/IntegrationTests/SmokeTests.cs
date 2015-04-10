using System;
using GmailApi;
using NUnit.Framework;

namespace UnitTests.IntegrationTests
{
    public class SmokeTests
    {
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _emailAddress;

        public SmokeTests()
        {
            _clientId = SettingsManager.GetClientId();
            _clientSecret = SettingsManager.GetClientSecret();
            _emailAddress = SettingsManager.GetEmailAddress();
        }

        [Test]
        public void TestTravisEnvVariable()
        {
            string testKey = Environment.GetEnvironmentVariable("TestKey");
            Console.WriteLine(string.Concat("testkey is '", testKey, "'"));

            testKey = Environment.GetEnvironmentVariable("TestKey", EnvironmentVariableTarget.Machine);
            Console.WriteLine(string.Concat("testkey2 is '", testKey, "'"));

            testKey = Environment.GetEnvironmentVariable("TestKey", EnvironmentVariableTarget.User);
            Console.WriteLine(string.Concat("testkey3 is '", testKey, "'"));

            testKey = Environment.GetEnvironmentVariable("TestKey", EnvironmentVariableTarget.Process);
            Console.WriteLine(string.Concat("testkey4 is '", testKey, "'"));

            Assert.IsNotNullOrEmpty(testKey, "testkey not set as environment variable");
        }

        //[Test]
        public void HasSettingsConfigured()
        {
            // Assert
            Assert.IsNotNullOrEmpty(_clientId, "CliendId not set");
            Assert.IsNotNullOrEmpty(_clientSecret, "CliendSecret not set");
            Assert.IsNotNullOrEmpty(_emailAddress, "Email not set");
        }

        //[Test]
        public void HasTokenConfigured()
        {
            // Arrange
            var tokenManager = new TokenManager(_clientId, _clientSecret);

            // Act
            bool hasTokenConfigured = tokenManager.HasTokenConfigured();

            // Assert
            Assert.IsTrue(hasTokenConfigured);
        }
    }
}
