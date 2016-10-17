using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KP.GmailApi.Models;
using KP.GmailApi.Services.Extensions;

namespace KP.GmailApi.WinFormsSample
{
    public class GmailServiceHelper
    {
        private readonly GmailClient _gmailClient;

        public GmailServiceHelper(string keyFile, string emailAddress, GmailScopes scopes)
        {
            _gmailClient = new GmailClient(keyFile, emailAddress, scopes);
        }

        public bool IsUserLoggedIn()
        {
            throw new NotImplementedException();
        }

        public void RequestUserToLogin()
        {
            throw new NotImplementedException();//TODO: not required anymore, delete
        }

        public async Task<IList<Message>> GetMailAsync()
        {
            Label keepLabel = await _gmailClient.Labels.GetByNameAsync("Keep");
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
        }
    }
}
