using KP.GmailApi.Builders;
using KP.GmailApi.Models;

namespace KP.GmailApi.Services
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

        /// <summary>
        /// Gets the specified message attachment.
        /// </summary>
        /// <param name="messageId">The ID of the message containing the attachment</param>
        /// <param name="id">The ID of the attachment</param>
        /// <returns></returns>
        public Attachment Get(string messageId, string id)
        {
            string queryString = new AttachmentQueryStringBuilder(messageId, id)
                .Build();

            return _client.Get<Attachment>(queryString);
        }
    }
}