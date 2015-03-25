using System;

namespace ConsoleApplication1
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