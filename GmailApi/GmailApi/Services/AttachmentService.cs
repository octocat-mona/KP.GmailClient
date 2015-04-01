using GmailApi.Builders;
using GmailApi.Models;

namespace GmailApi.Services
{
    public class AttachmentService
    {
        private readonly GmailClient _client;

        public AttachmentService(GmailClient client)
        {
            _client = client;
        }

        public Attachment Get(string messageId, string id)
        {
            string queryString = new AttachmentQueryStringBuilder(messageId, id)
                .Build();

            return _client.Get<Attachment>(queryString);
        }
    }
}