namespace KP.GmailClient.Common.Enums
{
    internal enum MessageFormat
    {
        /// <summary>
        /// Returns the full email message data with body content parsed in the payload field; the raw field is not used. (default)
        /// </summary>
        Full,
        /// <summary>
        /// Returns only email message ID, labels, and email headers.
        /// </summary>
        Metadata,
        /// <summary>
        /// Returns only email message ID and labels; does not return the email headers, body, or payload.
        /// </summary>
        Minimal,
        /// <summary>
        /// Returns the full email message data with body content in the raw field as a base64url encoded string; the payload field is not used.
        /// </summary>
        Raw
    }
}