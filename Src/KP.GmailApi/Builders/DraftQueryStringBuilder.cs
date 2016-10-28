using KP.GmailApi.Common;
using KP.GmailApi.Common.Enums;

namespace KP.GmailApi.Builders
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

        public DraftQueryStringBuilder SetUploadType(UploadType uploadType)
        {
            //TODO: remove duplication with MessageQueryStringBuilder?
            string uploadTypeString = uploadType.GetAttribute<StringValueAttribute, UploadType>().Text;
            SetField("uploadType", uploadTypeString);
            return this;
        }
    }
}