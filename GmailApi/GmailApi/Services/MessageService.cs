using System;
using GmailApi.Builders;
using GmailApi.DTO;
using GmailApi.Models;

namespace GmailApi.Services
{
    public class MessageService//TODO: interface
    {
        private readonly GmailClient _client;

        public AttachmentService Attachments { get; set; }

        public MessageService(GmailClient client)
        {
            _client = client;
            Attachments = new AttachmentService(client);
        }

        /// <summary>
        /// Lists the message IDs from messages filtered with the specified query.
        /// </summary>
        /// <param name="query">Only return messages matching the specified query.
        /// Supports the same query format as the Gmail search box. For example, "from:someuser@example.com rfc822msgid: is:unread".</param>
        /// <param name="maxResults">Maximum number of messages to return</param>
        /// <param name="includeSpamAndTrash">Include messages from SPAM and TRASH in the results.</param>
        /// <param name="labelIds">Only return messages with labels that match all of the specified label IDs</param>
        /// <returns>A <see cref="MessageList"/> containing the message IDs</returns>
        public MessageList ListIds(string query, ushort maxResults = 0, bool includeSpamAndTrash = false, params string[] labelIds)
        {
            string queryString = new MessageQueryStringBuilder()
                .SetFields(MessageFields.Id | MessageFields.ResultSizeEstimate | MessageFields.NextPageToken)
                .SetQuery(query)
                .SetLabelIds(labelIds)
                .SetMaxResults(maxResults)
                .SetIncludeSpamAndTrash(includeSpamAndTrash)
                .Build();

            return _client.Get<MessageList>(queryString);
        }

        /// <summary>
        /// Gets the specified message
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Message Get(string id)
        {
            string queryString = new MessageQueryStringBuilder()
               .SetRequestAction(MessageRequestAction.Get, id)
               .Build();

            return _client.Get<Message>(queryString);
        }

        /// <summary>
        /// NOTE: Immediately and permanently deletes the specified message.
        /// This operation CANNOT be undone. Prefer messages.trash instead.
        /// </summary>
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sends the specified message to the recipients in the To, Cc, and Bcc headers.
        /// </summary>
        public void Send()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Moves the specified message to the trash.
        /// </summary>
        /// <param name="id">The ID of the message to Trash.</param>
        /// <returns></returns>
        public Message Trash(string id)
        {
            string queryString = new MessageQueryStringBuilder()
                .SetRequestAction(MessageRequestAction.Trash, id)
                .Build();

            return _client.Post<Message>(queryString);
        }
    }
}