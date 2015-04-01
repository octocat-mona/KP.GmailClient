namespace GmailApi.Builders
{
    public class AttachmentQueryStringBuilder : QueryStringBuilder
    {
        public AttachmentQueryStringBuilder(string messageid, string id)
        {
            Path = string.Concat("messages/", messageid, "/attachments/" + id);
        }
    }
}
