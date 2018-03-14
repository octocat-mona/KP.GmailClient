using System.Collections.Generic;
using Newtonsoft.Json;

namespace KP.GmailClient.Models
{
    public class History
    {
        public History()
        {
            Id = string.Empty;
            Messages = new List<Message>(0);
            AddedMessages = new List<Message>(0);
            DeletedMessages = new List<Message>(0);
            AddedLabels = new List<Message>(0);
            RemovedLabels = new List<Message>(0);
        }

        /// <summary>
        /// The mailbox sequence ID.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// List of messages changed in this history record.
        /// The fields for specific change types, such as messagesAdded may duplicate messages in this field.
        /// We recommend using the specific change-type fields instead of this.
        /// </summary>
        [JsonProperty("messages")]
        public List<Message> Messages { get; set; }

        /// <summary>
        /// Messages added to the mailbox in this history record.
        /// </summary>
        [JsonProperty("messagesAdded")]
        public List<Message> AddedMessages { get; set; }

        /// <summary>
        /// Messages deleted (not Trashed) from the mailbox in this history record.
        /// </summary>
        [JsonProperty("messagesDeleted")]
        public List<Message> DeletedMessages { get; set; }

        /// <summary>
        /// Labels added to messages in this history record.
        /// </summary>
        [JsonProperty("labelsAdded")]
        public List<Message> AddedLabels { get; set; }

        /// <summary>
        /// Labels removed from messages in this history record.
        /// </summary>
        [JsonProperty("labelsRemoved")]
        public List<Message> RemovedLabels { get; set; }
    }
}
