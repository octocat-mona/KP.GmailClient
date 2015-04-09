using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace UnitTests.IntegrationTests
{
    public class SettingsManager
    {
        private static readonly IDictionary<string, string> EnvironmentVariables;

        static SettingsManager()
        {
            EnvironmentVariables = Environment.GetEnvironmentVariables()
                .OfType<DictionaryEntry>()
                .ToDictionary(k => (string)k.Key, v => (string)v.Value);
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
            // Env variables are used on Travis
            return EnvironmentVariables.ContainsKey(key)
                ? EnvironmentVariables[key]
                : ConfigurationManager.AppSettings[key];
        }
    }
}
