using System.Collections.Generic;
using System.Linq;
using KP.GmailApi.Managers;
using KP.GmailApi.Models;
using KP.GmailApi.ServiceExtensions;
using KP.GmailApi.Services;

namespace KP.GmailApi.WinFormsSample
{
    public class GmailServiceHelper
    {
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly GmailService _gmailService;
        private readonly OAuth2TokenManager _tokenManager;

        public GmailServiceHelper(string clientId, string clientSecret)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _tokenManager = new OAuth2TokenManager(_clientId, _clientSecret);
            _gmailService = new GmailService(_tokenManager);
        }

        public bool IsUserLoggedIn()
        {
            return _tokenManager.HasTokenSetup();
        }

        public void RequestUserToLogin()
        {
            TokenAccessHelper tokenHelper = new TokenAccessHelper(_clientId, _clientSecret);
            string authorizationCode = tokenHelper.GetAuthorizationCode();
            string refreshToken = tokenHelper.GetRefreshToken(authorizationCode);

            _tokenManager.Setup(refreshToken, true);
        }

        public List<Message> GetMail()
        {
            Label keepLabel = _gmailService.Labels.GetByName("Keep");
            return _gmailService.Messages.ListByLabel(keepLabel.Id)
                .ToList();
        }

        public List<Label> GetLabels()
        {
            return _gmailService.Labels.List(LabelType.User);
        }

        public void Logout()
        {
            _tokenManager.Delete();
        }
    }
}
