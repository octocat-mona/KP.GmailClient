namespace KP.GmailClient.Builders
{
    internal class AttachmentQueryStringBuilder : QueryStringBuilder
    {
        public AttachmentQueryStringBuilder(string messageid, string id)
        {
            Path = string.Concat("messages/", messageid, "/attachments/" + id);
        }
    }
}
