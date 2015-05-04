using Newtonsoft.Json;

namespace GmailApi.Models
{
    public class CreateLabelInput
    {
        public CreateLabelInput(string name)
        {
            Name = name;
        }

        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; private set; }

        [JsonProperty("messageListVisibility")]
        public MessageListVisibility MessageListVisibility { get; set; }

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