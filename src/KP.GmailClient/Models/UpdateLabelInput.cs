using System.Text.Json.Serialization;

namespace KP.GmailClient.Models
{
    /// <summary>The input required to update a label.</summary>
    public class UpdateLabelInput
    {
        /// <summary>The input required to update a label.</summary>
        /// <param name="id">The ID of the label to update</param>
        public UpdateLabelInput(string id)
        {
            Id = id;
        }

        /// <summary>The ID of the label to update.</summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>The display name of the label.</summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>The visibility of messages with this label in the message list in the Gmail web interface.</summary>
        [JsonPropertyName("messageListVisibility")]
        public MessageListVisibility MessageListVisibility { get; set; }

        /// <summary>The visibility of the label in the label list in the Gmail web interface.</summary>
        [JsonPropertyName("labelListVisibility")]
        public LabelListVisibility LabelListVisibility { get; set; }
    }
}