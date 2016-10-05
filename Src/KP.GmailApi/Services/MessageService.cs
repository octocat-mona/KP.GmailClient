using System.Threading.Tasks;
using KP.GmailApi.Builders;
using KP.GmailApi.Common;
using KP.GmailApi.Common.Enums;
using KP.GmailApi.Models;

namespace KP.GmailApi.Services
{
    /// <summary>
    /// Service to get, create, update and delete emails.
    /// </summary>
    public class MessageService
    {
        private readonly GmailProxy _proxy;

        /// <summary>
        /// Service for getting email attachments.
        /// </summary>
        public AttachmentService Attachments { get; set; }

        internal MessageService(GmailProxy proxy)
        {
            _proxy = proxy;
            Attachments = new AttachmentService(proxy);
        }

        /// <summary>
        /// Lists the message IDs.
        /// </summary>
        /// <param name="query">Only return messages matching the specified query.
        /// Supports the same query format as the Gmail search box. For example, "from:someuser@example.com rfc822msgid: is:unread".</param>
        /// <param name="maxResults">Maximum number of messages to return</param>
        /// <param name="includeSpamAndTrash">Include messages from SPAM and TRASH in the results.</param>
        /// <param name="labelIds">Only return messages with labels that match all of the specified label IDs</param>
        /// <returns>A <see cref="MessageList"/> containing the message IDs</returns>
        public async Task<MessageList> ListIdsAsync(string query = null, ushort maxResults = 0, bool includeSpamAndTrash = false, params string[] labelIds)
        {
            string queryString = new MessageQueryStringBuilder()
                .SetRequestAction(MessageRequestAction.List)
                .SetFields(MessageFields.Id | MessageFields.ResultSizeEstimate | MessageFields.NextPageToken)
                .SetQuery(query)
                .SetLabelIds(labelIds)
                .SetMaxResults(maxResults)
                .SetIncludeSpamAndTrash(includeSpamAndTrash)
                .Build();

            return await _proxy.Get<MessageList>(queryString);
        }

        /// <summary>
        /// Gets the specified message
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Message> GetAsync(string id)
        {
            string queryString = new MessageQueryStringBuilder()
               .SetRequestAction(MessageRequestAction.Get, id)
               .Build();

            return await _proxy.Get<Message>(queryString);
        }

        /// <summary>
        /// Immediately and permanently deletes the specified message. WARNING: This operation CANNOT be undone. Prefer messages.trash instead.
        /// </summary>
        public async Task DeleteAsync(string id)
        {
            string queryString = new MessageQueryStringBuilder()
               .SetRequestAction(MessageRequestAction.Delete, id)
               .Build();

            await _proxy.Delete(queryString);
        }

        /// <summary>
        /// Moves the specified message to the trash.
        /// </summary>
        /// <param name="id">The ID of the message to Trash.</param>
        /// <returns></returns>
        public async Task<Message> TrashAsync(string id)
        {
            string queryString = new MessageQueryStringBuilder()
                .SetRequestAction(MessageRequestAction.Trash, id)
                .Build();

            return await _proxy.Post<Message>(queryString);
        }

        /// <summary>
        /// Removes the specified message from the trash.
        /// </summary>
        /// <param name="id">The ID of the message to remove from Trash</param>
        public async Task<Message> UnTrashAsync(string id)
        {
            string queryString = new MessageQueryStringBuilder()
               .SetRequestAction(MessageRequestAction.Untrash, id)
               .Build();

            return await _proxy.Post<Message>(queryString);
        }

        /*/// <summary>
        /// Sends the specified message to the recipients in the To, Cc, and Bcc headers.
        /// </summary>
        public void Send()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Directly inserts a message into only this user's mailbox similar to IMAP APPEND, bypassing most scanning and classification. Does not send a message.
        /// </summary>
        public void Insert()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Modifies the labels on the specified message.
        /// </summary>
        /// <param name="id">The ID of the message to modify</param>
        /// <param name="addLabelIds">A list of IDs of labels to add to this message</param>
        /// <param name="removeLabelIds">A list IDs of labels to remove from this message</param>
        public void Modify(string id, string[] addLabelIds, string[] removeLabelIds)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Imports a message into only this user's mailbox, with standard email delivery scanning and classification similar to receiving via SMTP. Does not send a message.
        /// </summary>
        public void Import()
        {
            throw new NotImplementedException();
        }*/
    }
}