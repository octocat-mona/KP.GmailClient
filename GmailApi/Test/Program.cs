using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using GmailApi;
using GmailApi.Models;
using GmailApi.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string clientId = ConfigurationManager.AppSettings["ClientId"];
            string clientSecret = ConfigurationManager.AppSettings["ClientSecret"];
            string emailAddress = ConfigurationManager.AppSettings["EmailAddress"];

            try
            {
                // Cliend ID and secret of your project, see your Dev Console (https://console.developers.google.com/project)
                var tokenManager = new TokenManager(clientId, clientSecret);

                // First time required only, launches a browser for user interaction.
                //string authCode = tokenManager.GetAuthorizationCode();
                //var oauth2Token = tokenManager.GetToken(authCode);
                //tokenManager.Setup(oauth2Token.RefreshToken, false);

                // Setup 
                var client = new GmailClient("https://www.googleapis.com/gmail/v1/users/", emailAddress, tokenManager);
                var service = new GmailService(client);

                var messages = service.Messages.ListMessages(Label.Inbox);
                var labels = service.Labels.List();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
            }
        }
    }
}
