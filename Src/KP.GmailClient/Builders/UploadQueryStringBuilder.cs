using KP.GmailClient.Common;
using KP.GmailClient.Common.Enums;

namespace KP.GmailClient.Builders
{
    internal abstract class UploadQueryStringBuilder : QueryStringBuilder
    {
        public UploadQueryStringBuilder SetUploadType(UploadType uploadType)
        {
            string uploadTypeString = uploadType.GetAttribute<StringValueAttribute, UploadType>().Text;
            SetField("uploadType", uploadTypeString);
            return this;
        }
    }
}
