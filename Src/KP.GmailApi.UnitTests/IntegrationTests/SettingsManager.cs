using System;
using System.Configuration;
using KP.GmailApi.UnitTests.Extensions;

namespace KP.GmailApi.UnitTests.IntegrationTests
{
    public class SettingsManager
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

        public static string GetEmailAddress()
        {
            return GetSetting("EmailAddress");
        }

        public static string GetRefreshToken()
        {
            return GetSetting("RefreshToken");
        }

        public static GmailClient GetGmailClient()
        {
            string emailAddress = GetEmailAddress();
            string clientId = GetClientId();
            string clientSecret = GetClientSecret();
            string refreshToken = GetRefreshToken();

            var tokenManager = new TokenManager(clientId, clientSecret);
            tokenManager.DeleteFolder();
            tokenManager.Setup(refreshToken, false);

            return new GmailClient(emailAddress, tokenManager);
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
