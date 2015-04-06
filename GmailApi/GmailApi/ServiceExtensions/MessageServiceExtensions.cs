using System.Collections.Generic;
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
        /// <param name="labelId"></param>
        /// <returns></returns>
        public static IEnumerable<Message> ListMessages(this MessageService service, string labelId)
        {
            return service.List(null, labelId);
        }

        /// <summary>
        /// Lists the messages in the specified label.
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static IEnumerable<Message> ListMessages(this MessageService service)
        {
            return ListMessages(service, Label.Inbox);
        }

        /// <summary>
        /// Lists the message IDs of the user's inbox
        /// </summary>
        /// <returns></returns>
        public static MessageList ListMessageIds(this MessageService service)
        {
            return service.ListIds(Label.Inbox);
        }
    }
}
