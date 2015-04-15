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
        /// <param name="service"></param>
        /// <param name="labelId"></param>
        /// <returns></returns>
        public static uint Count(this MessageService service, string labelId)
        {
            return service.ListIds(labelId).ResultSizeEstimate;
        }

        /// <summary>
        /// Get the number of estimated messages in the user's inbox.
        /// </summary>
        /// <returns></returns>
        public static uint Count(this MessageService service)
        {
            return Count(service, Label.Inbox);
        }

        /// <summary>
        /// Lists the messages in the specified label.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="query"></param>
        /// <param name="labelId"></param>
        /// <returns></returns>
        public static IEnumerable<Message> List(this MessageService service, string query, string labelId)
        {
            var messageList = service.ListIds(query, labelId);

            return messageList.Messages.Select(id => service.Get(id.Id));// TODO: do one batch request?
        }

        /// <summary>
        /// Lists the messages in the specified label.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="labelId"></param>
        /// <returns></returns>
        public static IEnumerable<Message> List(this MessageService service, string labelId)
        {
            return service.List(null, labelId);
        }

        /// <summary>
        /// Lists the messages of the user's inbox
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static IEnumerable<Message> List(this MessageService service)
        {
            return List(service, Label.Inbox);
        }

        /// <summary>
        /// Lists the message IDs of the user's inbox
        /// </summary>
        /// <returns></returns>
        public static MessageList ListIds(this MessageService service)
        {
            return service.ListIds(Label.Inbox);
        }
    }
}
