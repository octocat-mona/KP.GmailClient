using Newtonsoft.Json;

namespace GmailApi.Models
{
    public class UpdateLabelInput
    {
        public UpdateLabelInput(string id)
        {
            Id = id;
        }

        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; private set; }

        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("messageListVisibility")]
        public MessageListVisibility MessageListVisibility { get; set; }

        [JsonProperty("labelListVisibility")]
        public LabelListVisibility LabelListVisibility { get; set; }
    }
}