using GmailApi.Builders;
using GmailApi.Models;

namespace GmailApi.Services
{
    /// <summary>
    /// Service for getting email attachments
    /// </summary>
    public class AttachmentService
    {
        private readonly GmailClient _client;

        internal AttachmentService(GmailClient client)
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