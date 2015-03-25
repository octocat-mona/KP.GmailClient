using Newtonsoft.Json;

namespace ConsoleApplication1
{
    public class MessageId//TODO: GmailMessageBase instead?
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("threadId")]
        public string ThreadId { get; set; }
    }
}