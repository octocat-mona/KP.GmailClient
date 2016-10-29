using System;
using System.Configuration;
using KP.GmailClient;
using KP.GmailClient.Common;
using KP.GmailClient.Models;

namespace KP.GmailClient.Tests.IntegrationTests
{
    internal class SettingsManager
    {
        private static readonly bool UseConfig;

        static SettingsManager()
        {
            UseConfig = ConfigurationManager.AppSettings["UseConfig"] != null;
        }

        public static string GetPrivateKey()
        {
            return GetSetting("PrivateKey");
        }

        public static string GetTokenUri()
        {
            return GetSetting("TokenUri");
        }

        public static string GetClientEmail()
        {
            return GetSetting("ClientEmail");
        }

        public static string GetEmailAddress()
        {
            return GetSetting("EmailAddress");
        }

        public static GmailProxy GetGmailProxy()
        {
            string privateKey = GetPrivateKey();
            string tokenUri = GetTokenUri();
            string clientEmail = GetClientEmail();
            string emailAddress = GetEmailAddress();
            ServiceAccountCredential accountCredential = new ServiceAccountCredential
            {
                PrivateKey = privateKey,
                TokenUri = tokenUri,
                ClientEmail = clientEmail
            };

            //TODO: get GmailClient.ConvertToScopes using reflection in ReflectionHelper
            string scope = GmailHelper.GetGmailScopesField("ModifyScope");
            return new GmailProxy(new AuthorizationDelegatingHandler(accountCredential, emailAddress, scope));
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
