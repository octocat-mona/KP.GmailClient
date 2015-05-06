using Newtonsoft.Json;

namespace KP.GmailApi.Models
{
    /// <summary>
    /// The input required to update a label.
    /// </summary>
    public class UpdateLabelInput
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">The ID of the label to update</param>
        public UpdateLabelInput(string id)
        {
            Id = id;
        }

        /// <summary>
        /// The ID of the label to update.
        /// </summary>
        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; private set; }

        /// <summary>
        /// The display name of the label.
        /// </summary>
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        /// <summary>
        /// The visibility of messages with this label in the message list in the Gmail web interface.
        /// </summary>
        [JsonProperty("messageListVisibility")]
        public MessageListVisibility MessageListVisibility { get; set; }

        /// <summary>
        /// The visibility of the label in the label list in the Gmail web interface.
        /// </summary>
        [JsonProperty("labelListVisibility")]
        public LabelListVisibility LabelListVisibility { get; set; }
    }
}