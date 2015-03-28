using Newtonsoft.Json;

namespace GmailApi.Models
{
    public enum LabelType
    {
        [JsonProperty("system")]
        System,

        [JsonProperty("user")]
        User
    }
}