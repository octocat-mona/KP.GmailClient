using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KP.GmailClient.Models
{
    public class History
    {
        /// <summary>The mailbox sequence ID.</summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// List of messages changed in this history record.
        /// The fields for specific change types, such as messagesAdded may duplicate messages in this field.
        /// We recommend using the specific change-type fields instead of this.
        /// </summary>
        [JsonPropertyName("messages")]
        public List<Message> Messages { get; set; } = new List<Message>();

        /// <summary>Messages added to the mailbox in this history record.</summary>
        [JsonPropertyName("messagesAdded")]
        public List<Message> AddedMessages { get; set; } = new List<Message>();

        /// <summary>Messages deleted (not Trashed) from the mailbox in this history record.</summary>
        [JsonPropertyName("messagesDeleted")]
        public List<Message> DeletedMessages { get; set; } = new List<Message>();

        /// <summary>Labels added to messages in this history record.</summary>
        [JsonPropertyName("labelsAdded")]
        public List<Message> AddedLabels { get; set; } = new List<Message>();

        /// <summary>Labels removed from messages in this history record.</summary>
        [JsonPropertyName("labelsRemoved")]
        public List<Message> RemovedLabels { get; set; } = new List<Message>();
    }
}
