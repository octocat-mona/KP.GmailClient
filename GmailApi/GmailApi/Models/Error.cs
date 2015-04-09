using Newtonsoft.Json;

namespace GmailApi.Models
{
    public class Error
    {
        public Error()
        {
            Domain = string.Empty;
            Reason = string.Empty;
            Message = string.Empty;
            Location = string.Empty;
            LocationType = string.Empty;
        }

        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("locationType")]
        public string LocationType { get; set; }

        public override string ToString()
        {
            return string.Concat("Domain: ", Domain, ", Reason: ", Reason, ", Message: ", Message, ", Location: ", Location, "LocationType: ", LocationType);
        }
    }
}