namespace GmailApi.DTO
{
    internal enum ThreadFormat
    {
        /// <summary>
        /// Returns the parsed email message content in the payload field and the raw field is not used. (default)
        /// </summary>
        Full,
        /// <summary>
        /// Returns email headers with message metadata such as identifiers and labels.
        /// </summary>
        Metadata,
        /// <summary>
        /// Only returns email message metadata such as identifiers and labels, it does not return the email headers, body, or payload.
        /// </summary>
        Minimal
    }
}