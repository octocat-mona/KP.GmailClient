using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KP.GmailClient.Builders;
using KP.GmailClient.Common;
using KP.GmailClient.Models;

namespace KP.GmailClient.Services.Extensions
{
    /// <summary>
    /// Extensions for <see cref="MessageService"/>.
    /// </summary>
    public static class MessageServiceExtensions
    {
        /// <summary>
        /// Get the number of estimated messages of the specified label.
        /// </summary>
        /// <param name="service">Gmail API service instance</param>
        /// <param name="labelIds">Only return messages with labels that match all of the specified label IDs</param>
        /// <returns>The number of messages</returns>
        public static async Task<uint> CountAsync(this MessageService service, params string[] labelIds)
        {
            return (await service.ListIdsAsync(labelIds: labelIds)).ResultSizeEstimate;
        }

        /// <summary>
        /// Get the number of estimated messages in the user's inbox.
        /// </summary>
        /// <param name="service">Gmail API service instance</param>
        /// <returns>The number of messages</returns>
        public static async Task<uint> CountAsync(this MessageService service)
        {
            return await CountAsync(service, Label.Inbox);
        }

        /// <summary>
        /// Lists the message IDs of the user's inbox.
        /// </summary>
        /// <param name="service">Gmail API service instance</param>
        /// <returns>A <see cref="MessageList"/> instance</returns>
        public static async Task<MessageList> ListIdsAsync(this MessageService service)
        {
            return await service.ListIdsAsync(labelIds: Label.Inbox);
        }

        /// <summary>
        /// Lists the messages of the user's inbox.
        /// </summary>
        /// <param name="service">Gmail API service instance</param>
        /// <returns>A list of Messages</returns>
        public static async Task<IList<Message>> ListAsync(this MessageService service)
        {
            return await ListAsync(service, null, labelIds: Label.Inbox);
        }

        /// <summary>
        /// Lists the messages filtered with a query.
        /// </summary>
        /// <param name="service">Gmail API service instance</param>
        /// <param name="query">Only return messages matching the specified query.
        /// Supports the same query format as the Gmail search box. For example, "from:someuser@example.com rfc822msgid: is:unread".</param>
        /// <param name="maxResults">Maximum number of messages to return</param>
        /// <param name="includeSpamAndTrash">Include messages from SPAM and TRASH in the results.</param>
        /// <param name="labelIds">Only return messages with labels that match all of the specified label IDs</param>
        /// <returns>A list of Messages</returns>
        public static async Task<IList<Message>> ListAsync(this MessageService service, string query, ushort maxResults = 0, bool includeSpamAndTrash = false, params string[] labelIds)
        {
            var messageList = await service.ListIdsAsync(query, maxResults, includeSpamAndTrash, labelIds);

            var tasks = messageList.Messages.Select(id => service.GetAsync(id.Id));// TODO: do one batch request?
            return (await Task.WhenAll(tasks)).ToList();
        }

        /// <summary>
        /// Lists the messages in the specified label.
        /// </summary>
        /// <param name="service">Gmail API service instance</param>
        /// <param name="labelId">Only return messages with the specified label ID</param>
        /// <param name="query">Only return messages matching the specified query.
        /// Supports the same query format as the Gmail search box. For example, "from:someuser@example.com rfc822msgid: is:unread".</param>
        /// <param name="maxResults">Maximum number of messages to return</param>
        /// <param name="includeSpamAndTrash">Include messages from SPAM and TRASH in the results.</param>
        /// <returns>A list of Messages</returns>
        public static async Task<IList<Message>> ListByLabelAsync(this MessageService service, string labelId, string query = null, ushort maxResults = 0, bool includeSpamAndTrash = false)
        {
            var messageList = await service.ListIdsAsync(query, maxResults, includeSpamAndTrash, labelId);

            var tasks = messageList.Messages.Select(message => service.GetAsync(message.Id));
            return (await Task.WhenAll(tasks)).ToList();
        }

        /// <summary>
        /// Lists the messages in the specified labels.
        /// </summary>
        /// <param name="service">Gmail API service instance</param>
        /// <param name="labelIds">Only return messages with labels that match all of the specified label IDs</param>
        /// <param name="query">Only return messages matching the specified query.
        /// Supports the same query format as the Gmail search box. For example, "from:someuser@example.com rfc822msgid: is:unread".</param>
        /// <param name="maxResults">Maximum number of messages to return</param>
        /// <param name="includeSpamAndTrash">Include messages from SPAM and TRASH in the results.</param>
        /// <returns>A list of Messages</returns>
        public static async Task<IList<Message>> ListByLabelsAsync(this MessageService service, string[] labelIds, string query = null, ushort maxResults = 0, bool includeSpamAndTrash = false)
        {
            var messageList = await service.ListIdsAsync(query, maxResults, includeSpamAndTrash, labelIds);

            var tasks = messageList.Messages.Select(id => service.GetAsync(id.Id));
            return (await Task.WhenAll(tasks)).ToList();
        }

        /// <summary>
        /// Sends the specified message to the recipients in the To, Cc, and Bcc headers.
        /// </summary>
        /// <param name="service">Gmail API service instance</param>
        /// <param name="toAddresses">One or more email addresses separated with a comma character (",").</param>
        /// <param name="subject">The subject line for this email message</param>
        /// <param name="body">The message body</param>
        /// <param name="ccAddresses">One or more email addresses separated with a comma character (",").</param>
        /// <param name="bccAddresses">One or more email addresses separated with a comma character (",").</param>
        /// <param name="replyToAddresses">One or more email addresses separated with a comma character (",").</param>
        /// <param name="isBodyHtml">True for HTML message ('text/html'). Defaults to 'text/plain'.</param>
        /// <returns></returns>
        public static async Task<Message> SendAsync(this MessageService service, string toAddresses, string subject, string body, string ccAddresses = null, string bccAddresses = null, string replyToAddresses = null, bool isBodyHtml = false)
        {
            string rfc2822 = new EmailMessageBuilder()
                .AddTo(toAddresses)
                .AddReplyTo(replyToAddresses)
                .AddCc(ccAddresses)
                .AddBcc(bccAddresses)
                .SetSubject(subject)
                .SetBody(body, isBodyHtml)
                .Build();

            string raw = rfc2822.ToBase64UrlString();
            return await service.SendAsync(raw, null);
        }
    }
}
