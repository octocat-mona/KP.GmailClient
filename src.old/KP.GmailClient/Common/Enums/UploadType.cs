namespace KP.GmailClient.Common.Enums
{
    /// <summary>
    /// The type of upload request to the /upload URI.
    /// </summary>
    internal enum UploadType
    {
        /// <summary>
        /// Simple upload. Upload the media only, without any metadata.
        /// </summary>
        [StringValue("media")]
        Media,

        /// <summary>
        /// Multipart upload. Upload both the media and its metadata, in a single request.
        /// </summary>
        [StringValue("multipart")]
        Multipart,

        /// <summary>
        /// Resumable upload. Upload the file in a resumable fashion, using a series of at least two requests where the first request includes the metadata.
        /// </summary>
        [StringValue("resumable")]
        Resumable
    }
}
