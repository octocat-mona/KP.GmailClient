using System.Collections.Generic;
using System.Linq;
using GmailApi.Models;
using GmailApi.Services;

namespace GmailApi.ServiceExtensions
{
    public static class MessageServiceExtensions
    {
        /// <summary>
        /// Get the number of estimated messages of the specified label.
        /// </summary>
        /// <param name="service">Gmail API service instance</param>
        /// <param name="labelIds">Only return messages with labels that match all of the specified label IDs</param>
        /// <returns>The number of Labels</returns>
        public static uint Count(this MessageService service, params string[] labelIds)
        {
            return service.ListIds(null, labelIds: labelIds).ResultSizeEstimate;
        }

        /// <summary>
        /// Get the number of estimated messages in the user's inbox.
        /// </summary>
        /// <param name="service">Gmail API service instance</param>
        /// <returns>The number of Labels</returns>
        public static uint Count(this MessageService service)
        {
            return Count(service, Label.Inbox);
        }

        /// <summary>
        /// Lists the message IDs of the user's inbox.
        /// </summary>
        /// <param name="service">Gmail API service instance</param>
        /// <returns>A <see cref="MessageList"/> instance</returns>
        public static MessageList ListIds(this MessageService service)
        {
            return service.ListIds(null, labelIds: Label.Inbox);
        }

        /// <summary>
        /// Lists the messages in the specified label.
        /// </summary>
        /// <param name="service">Gmail API service instance</param>
        /// <param name="query">Only return messages matching the specified query.
        /// Supports the same query format as the Gmail search box. For example, "from:someuser@example.com rfc822msgid: is:unread".</param>
        /// <param name="maxResults">Maximum number of messages to return</param>
        /// <param name="includeSpamAndTrash">Include messages from SPAM and TRASH in the results.</param>
        /// <param name="labelIds">Only return messages with labels that match all of the specified label IDs</param>
        /// <returns>A list of Messages</returns>
        public static IEnumerable<Message> List(this MessageService service, string query, ushort maxResults = 0, bool includeSpamAndTrash = false, params string[] labelIds)
        {
            var messageList = service.ListIds(query, maxResults, includeSpamAndTrash, labelIds);

            return messageList.Messages.Select(id => service.Get(id.Id));// TODO: do one batch request?
        }

        /// <summary>
        /// Lists the messages of the user's inbox.
        /// </summary>
        /// <param name="service">Gmail API service instance</param>
        /// <returns>A list of Messages</returns>
        public static IEnumerable<Message> List(this MessageService service)
        {
            return List(service, null, labelIds: Label.Inbox);
        }
    }
}
