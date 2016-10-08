using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KP.GmailApi.Managers;
using KP.GmailApi.Models;
using KP.GmailApi.ServiceExtensions;

namespace KP.GmailApi.WinFormsSample
{
    public class GmailServiceHelper
    {
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly GmailClient _gmailClient;
        //private readonly OAuth2TokenManager _tokenManager;

        public GmailServiceHelper(string clientId, string clientSecret)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            //_tokenManager = new OAuth2TokenManager(_clientId, _clientSecret);
            var tokenStore = new InMemoryTokenStore(clientId, clientSecret, null);
            _gmailClient = new GmailClient(tokenStore);
        }

        public bool IsUserLoggedIn()
        {
            throw new NotImplementedException();
            //return _tokenManager.HasTokenSetup();
        }

        public void RequestUserToLogin()
        {
            throw new NotImplementedException();
            /*TokenAccessHelper tokenHelper = new TokenAccessHelper(_clientId, _clientSecret);
            string authorizationCode = tokenHelper.GetAuthorizationCode();
            string refreshToken = tokenHelper.GetRefreshToken(authorizationCode);

            _tokenManager.Setup(refreshToken, true);*/
        }

        public async Task<IList<Message>> GetMailAsync()
        {
            Label keepLabel = (await _gmailClient.Labels.GetByNameAsync("Keep"));
            return (await _gmailClient.Messages.ListByLabelAsync(keepLabel.Id))
                .ToList();
        }

        public async Task<IList<Label>> GetLabelsAsync()
        {
            return await _gmailClient.Labels.ListAsync(LabelType.User);
        }

        public void Logout()
        {
            throw new NotImplementedException();
            //_tokenManager.Delete();
        }
    }
}
