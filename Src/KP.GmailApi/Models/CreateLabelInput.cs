using Newtonsoft.Json;

namespace KP.GmailApi.Models
{
    /// <summary>
    /// The input required to create a label.
    /// </summary>
    public class CreateLabelInput
    {
        /// <summary>
        /// The input required to create a label.
        /// </summary>
        /// <param name="name">The display name of the label</param>
        public CreateLabelInput(string name)
        {
            Name = name;
        }

        /// <summary>
        /// The display name of the label.
        /// </summary>
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; private set; }

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

        /// <summary>
        /// A string with the values of the properties from this <see cref="CreateLabelInput"/>
        /// </summary>
        /// <returns>A string</returns>
        public override string ToString()
        {
            return string.Concat("Name: ", Name, ", MessageListVisibility: ", MessageListVisibility, ", LabelListVisibility: ", LabelListVisibility);
        }
    }
}