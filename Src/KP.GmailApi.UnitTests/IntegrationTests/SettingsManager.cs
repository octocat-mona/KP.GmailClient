using System;
using System.Configuration;
using KP.GmailApi.Common;
using KP.GmailApi.Managers;
using KP.GmailApi.UnitTests.Extensions;

namespace KP.GmailApi.UnitTests.IntegrationTests
{
    internal class SettingsManager
    {
        private static readonly bool UseConfig;

        static SettingsManager()
        {
            bool.TryParse(ConfigurationManager.AppSettings["UseConfig"], out UseConfig);
        }

        public static string GetClientId()
        {
            return GetSetting("ClientId");
        }

        public static string GetClientSecret()
        {
            return GetSetting("ClientSecret");
        }

        public static string GetRefreshToken()
        {
            return GetSetting("RefreshToken");
        }

        public static GmailProxy GetGmailProxy()
        {
            string clientId = GetClientId();
            string clientSecret = GetClientSecret();
            string refreshToken = GetRefreshToken();

            var tokenManager = new OAuth2TokenManager(clientId, clientSecret);
            //tokenManager.DeleteFolder();
            tokenManager.Setup(refreshToken, false);

            return new GmailProxy(new AuthorizationDelegatingHandler(tokenManager));
        }

        private static string GetSetting(string key)
        {
            // Environment variables are used on Travis / AppVeyor
            string value = UseConfig
                ? ConfigurationManager.AppSettings[key]
                : Environment.GetEnvironmentVariable(key);

            if (value == null)
                throw new Exception(string.Concat("Key '", key, "' has not been set in the ", UseConfig ? "config file." : "environment variables."));

            return value;
        }
    }
}
