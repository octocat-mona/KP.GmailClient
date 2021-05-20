using System;
using System.IO;
using KP.GmailClient.Authentication.TokenClients;
using KP.GmailClient.Authentication.TokenStores;
using KP.GmailClient.Common;
using KP.GmailClient.Models;
using Microsoft.Extensions.Configuration;

namespace KP.GmailClient.Tests.IntegrationTests
{
    internal class SettingsManager
    {
        private static readonly IConfigurationRoot ConfigurationRoot;
        private const string SettingsPrefix = "KP_GmailClient_";
        public static GmailProxy GmailProxy { get; }

        static SettingsManager()
        {
            ConfigurationRoot = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", false)
                 .AddJsonFile("appsettings.Private.json", true)
                 .Build();

            GmailProxy = GetGmailProxy();
        }

        public static string GetTokenUri() => GetSetting($"{SettingsPrefix}TokenUri");

        public static string GetClientId() => GetSetting($"{SettingsPrefix}ClientId");

        public static string GetClientSecret() => GetSetting($"{SettingsPrefix}ClientSecret");

        public static string GetRefreshToken() => GetSetting($"{SettingsPrefix}RefreshToken");

        public static string GetEmailAddress() => GetSetting($"{SettingsPrefix}EmailAddress");

        private static GmailProxy GetGmailProxy()
        {
            var credentials = new OAuth2ClientCredentials
            {
                TokenUri = GetTokenUri(),
                ClientId = GetClientId(),
                ClientSecret = GetClientSecret()
            };

            var tokenClient = new TokenClient(credentials);
            var tokenStore = new InMemoryTokenStore(GetRefreshToken());
            return new GmailProxy(new AuthorizationDelegatingHandler(tokenClient, tokenStore));
        }

        private static string GetSetting(string key)
        {
            // Environment variables are used on AppVeyor
            string value = Environment.GetEnvironmentVariable(key) ?? ConfigurationRoot[key];
            if (value == null)
            {
                throw new Exception($"Key '{key}' has not been set in neither the environment variables or config file.");
            }

            return value;
        }
    }
}
