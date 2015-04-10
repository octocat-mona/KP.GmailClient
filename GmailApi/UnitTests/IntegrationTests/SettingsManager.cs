using System;
using System.Configuration;

namespace UnitTests.IntegrationTests
{
    public class SettingsManager
    {
        //private static readonly IDictionary<string, string> EnvironmentVariables;
        private static readonly bool UseConfig;

        static SettingsManager()
        {
            bool.TryParse(ConfigurationManager.AppSettings["UseConfig"], out UseConfig);

            /*EnvironmentVariables = Environment.GetEnvironmentVariables()
                .OfType<DictionaryEntry>()
                .ToDictionary(k => (string)k.Key, v => (string)v.Value);*/
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

            /*return EnvironmentVariables.ContainsKey(key)
                ? EnvironmentVariables[key]
                : ConfigurationManager.AppSettings[key];*/
        }
    }
}
