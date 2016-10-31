using System;
using System.Configuration;
using System.Text;
using KP.GmailClient.Common;
using KP.GmailClient.Models;

namespace KP.GmailClient.Tests.IntegrationTests
{
    internal class SettingsManager
    {
        private const string SettingsPrefix = "KP.GmailClient.";

        public static string GetPrivateKey()
        {
            string base64String = GetSetting($"{SettingsPrefix}PrivateKey");
            byte[] bytes = Convert.FromBase64String(base64String);
            return Encoding.UTF8.GetString(bytes);
        }

        public static string GetTokenUri()
        {
            return GetSetting($"{SettingsPrefix}TokenUri");
        }

        public static string GetClientEmail()
        {
            return GetSetting($"{SettingsPrefix}ClientEmail");
        }

        public static string GetEmailAddress()
        {
            return GetSetting($"{SettingsPrefix}EmailAddress");
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
            // Environment variables are used on Travis CI and AppVeyor
            string value = Environment.GetEnvironmentVariable(key) ?? ConfigurationManager.AppSettings[key];
            if (value == null)
            {
                throw new Exception($"Key '{key}' has not been set in neither the environment variables or config file.");
            }

            return value;
        }
    }
}
