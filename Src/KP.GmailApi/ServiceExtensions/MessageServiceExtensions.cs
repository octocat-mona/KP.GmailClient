using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KP.GmailApi.Models;
using KP.GmailApi.Services;

namespace KP.GmailApi.ServiceExtensions
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
    }
}
