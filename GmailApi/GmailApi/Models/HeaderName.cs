using Newtonsoft.Json;

namespace GmailApi.Models
{
    /// <summary>
    /// Header according to the RFC 2822 Internet Message Format standard.
    /// </summary>
    public enum HeaderName
    {
        [JsonProperty("")]
        Unknown,

        [JsonProperty("from")]
        From,

        [JsonProperty("trace")]
        Trace,

        [JsonProperty("resent-date")]
        ResentDate,

        [JsonProperty("keywords")]
        Keywords,

        [JsonProperty("optional-field")]
        OptionalField,

        [JsonProperty("sender")]
        Sender,

        [JsonProperty("reply-to")]
        ReplyTo,

        [JsonProperty("subject")]
        Subject,

        [JsonProperty("comments")]
        Comments,

        [JsonProperty("references")]
        References,

        [JsonProperty("to")]
        To,

        [JsonProperty("cc")]
        Cc,

        [JsonProperty("in-reply-to")]
        InReplyTo,

        [JsonProperty("bcc")]
        Bcc,

        [JsonProperty("message-id")]
        MessageId,

        [JsonProperty("resent-from")]
        ResentFrom,

        [JsonProperty("resent-sender")]
        ResentSender,

        [JsonProperty("resent-to")]
        ResentTo,

        [JsonProperty("resent-cc")]
        ResentCc,

        [JsonProperty("resent-bcc")]
        ResentBcc,

        [JsonProperty("resentMsgId")]
        ResentMsgId,

        [JsonProperty("orig-date")]
        OrigDate,

        //TODO: NOT a spec item?
        [JsonProperty("Delivered-To")]
        DeliveredTo
    }
}