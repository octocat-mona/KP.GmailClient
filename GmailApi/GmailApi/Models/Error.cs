using Newtonsoft.Json;

namespace GmailApi.Models
{
    public class Error
    {
        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("locationType")]
        public string LocationType { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }
    }
}