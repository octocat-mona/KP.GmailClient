using System.Threading.Tasks;
using KP.GmailApi.Builders;
using KP.GmailApi.Common;
using KP.GmailApi.Models;

namespace KP.GmailApi.Services
{
    /// <summary>
    /// Service for getting email attachments.
    /// </summary>
    public class AttachmentService
    {
        private readonly GmailProxy _proxy;

        internal AttachmentService(GmailProxy proxy)
        {
            _proxy = proxy;
        }

        /// <summary>
        /// Gets the specified message attachment.
        /// </summary>
        /// <param name="messageId">The ID of the message containing the attachment</param>
        /// <param name="id">The ID of the attachment</param>
        /// <returns></returns>
        public async Task<Attachment> GetAsync(string messageId, string id)
        {
            string queryString = new AttachmentQueryStringBuilder(messageId, id)
                .Build();

            return await _proxy.Get<Attachment>(queryString);
        }
    }
}