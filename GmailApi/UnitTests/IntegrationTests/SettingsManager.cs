using System;
using System.Configuration;

namespace UnitTests.IntegrationTests
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

        private static string GetSetting(string key)
        {
            // Environment variables are used on Travis
            return UseConfig
                ? ConfigurationManager.AppSettings[key]
                : Environment.GetEnvironmentVariable(key);
        }
    }
}
