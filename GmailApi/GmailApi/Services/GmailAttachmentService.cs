using System;

namespace GmailApi.Services
{
    public class GmailAttachmentService
    {
        private readonly GmailClient _client;

        public GmailAttachmentService(GmailClient client)
        {
            _client = client;
        }

        public object Get(string id)
        {
            throw new NotImplementedException();
        }
    }
}