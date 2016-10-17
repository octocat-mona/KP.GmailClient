using System;
using System.Configuration;
using KP.GmailApi.Common;

namespace KP.GmailApi.UnitTests.IntegrationTests
{
    internal class SettingsManager
    {
        private static readonly bool UseConfig;

        static SettingsManager()
        {
            bool.TryParse(ConfigurationManager.AppSettings["UseConfig"], out UseConfig);
        }

        public static string GetGoogleAccountCredentialsFile()
        {
            return GetSetting("GoogleAccountCredentialsFile");
        }

        public static string GetEmailAddress()
        {
            return GetSetting("EmailAddress");
        }

        public static GmailProxy GetGmailProxy()
        {
            string keyFile = GetGoogleAccountCredentialsFile();
            string emailAddress = GetEmailAddress();

            //TODO: get GmailClient.ConvertToScopes using reflection in ReflectionHelper
            return new GmailProxy(new AuthorizationDelegatingHandler(keyFile, emailAddress, "https://www.googleapis.com/auth/gmail.modify"));
        }

        private static string GetSetting(string key)
        {
            // Environment variables are used on Travis CI / AppVeyor
            string value = UseConfig
                ? ConfigurationManager.AppSettings[key]
                : Environment.GetEnvironmentVariable(key);

            if (value == null)
            {
                throw new Exception(string.Concat("Key '", key, "' has not been set in the ", UseConfig ? "config file." : "environment variables."));
            }

            return value;
        }
    }
}
