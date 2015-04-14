using GmailApi.DTO;

namespace GmailApi.Builders
{
    internal class DraftQueryStringBuilder : QueryStringBuilder
    {
        public DraftQueryStringBuilder()
        {
            Path = "drafts";
        }

        public DraftQueryStringBuilder SetFormat(DraftFormat format)
        {
            base.SetFormat(format);

            return this;
        }

        public DraftQueryStringBuilder SetRequestAction(DraftRequestAction action, string id)
        {
            base.SetRequestAction(action, id);

            return this;
        }

        public DraftQueryStringBuilder SetRequestAction(DraftRequestAction action)
        {
            base.SetRequestAction(action);

            return this;
        }
    }
}