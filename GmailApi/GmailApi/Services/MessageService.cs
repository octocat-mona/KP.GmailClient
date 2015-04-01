using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Lists the message IDs of the user's inbox
        /// </summary>
        /// <returns></returns>
        public MessageList ListMessageIds()
        {
            string queryString = new MessageQueryStringBuilder()
               .SetFields(MessageFields.Id | MessageFields.ResultSizeEstimate | MessageFields.NextPageToken)
               .Build();

            return _client.Get<MessageList>(queryString);
        }

        /// <summary>
        /// Lists the message IDs from messages in the specified label.
        /// </summary>
        /// <param name="labelId"></param>
        /// <returns></returns>
        public MessageList ListMessageIds(string labelId)
        {
            string queryString = new MessageQueryStringBuilder()
                .SetFields(MessageFields.Id | MessageFields.ResultSizeEstimate | MessageFields.NextPageToken)
                .SetLabelIds(labelId)
                .Build();

            return _client.Get<MessageList>(queryString);
        }

        /// <summary>
        /// Lists the message IDs from messages in the specified label and filtered with the specified query.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="labelId"></param>
        /// <returns></returns>
        public MessageList ListMessageIds(string query, string labelId)
        {
            string queryString = new MessageQueryStringBuilder()
                .SetFields(MessageFields.Id)
                .SetQuery(query)
                .SetLabelIds(labelId)
                .Build();

            return _client.Get<MessageList>(queryString);
        }

        /// <summary>
        /// Lists the messages in the specified label.
        /// </summary>
        /// <param name="labelId"></param>
        /// <returns></returns>
        public IEnumerable<Message> ListMessages(string labelId)
        {
            var messageList = ListMessageIds(labelId);

            return messageList.Messages.Select(id => Get(id.Id));
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

        /// <summary>
        /// Get the number of estimated messages in the user's inbox.
        /// </summary>
        /// <returns></returns>
        public uint Count()
        {
            return Count(Label.Inbox);
        }

        /// <summary>
        /// Get the number of estimated messages of the specified label.
        /// </summary>
        /// <param name="labelId"></param>
        /// <returns></returns>
        public uint Count(string labelId)
        {
            return ListMessageIds(labelId).ResultSizeEstimate;
        }
    }
}