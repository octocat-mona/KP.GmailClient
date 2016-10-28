using System;
using System.Linq;
using System.Text;
using KP.GmailApi.Common;
using KP.GmailApi.Common.Enums;

namespace KP.GmailApi.Builders
{
    internal class MessageQueryStringBuilder : QueryStringBuilder
    {
        private Action _fieldsAction;

        public MessageQueryStringBuilder()
        {
            Path = "messages";
        }

        /// <summary>
        /// Set action which doesn't require an ID
        /// </summary>
        /// <param name="action">The action to set</param>
        /// <returns>This builder</returns>
        public MessageQueryStringBuilder SetRequestAction(MessageRequestAction action)
        {
            base.SetRequestAction(action);
            return this;
        }

        /// <summary>
        /// Set action which requires an ID
        /// </summary>
        /// <param name="action">The action to set</param>
        /// <param name="id">Id of the message</param>
        /// <returns>This builder</returns>
        public MessageQueryStringBuilder SetRequestAction(MessageRequestAction action, string id)
        {
            base.SetRequestAction(action, id);
            return this;
        }

        public MessageQueryStringBuilder SetUploadType(UploadType uploadType)
        {
            string uploadTypeString = uploadType.GetAttribute<StringValueAttribute, UploadType>().Text;
            SetField("uploadType", uploadTypeString);
            return this;
        }

        public MessageQueryStringBuilder SetThreadId(string threadId)
        {
            SetField("threadId", threadId);
            return this;
        }

        public MessageQueryStringBuilder SetFields(MessageFields fields)
        {
            _fieldsAction = () =>
            {
                var sb = new StringBuilder();
                var allFields = fields.GetFlagEnumValues()
                    .ToList();

                if (fields.HasFlag(MessageFields.Messages))
                {
                    sb.Append("messages");//TODO: dependent on MessageRequestAction.. (eg. get vs list)
                }
                else
                {
                    var messageFields = allFields
                    .Where(f => MessageFields.Messages.HasFlag(f))
                    .Select(f => f.GetAttribute<StringValueAttribute, MessageFields>())
                    .Where(att => att != null)
                    .Select(att => att.Text)
                    .ToList();

                    sb.Append("messages(").Append(string.Join(",", messageFields)).Append(")");
                }

                var otherFields = allFields
                    .Where(f => f == MessageFields.ResultSizeEstimate || f == MessageFields.NextPageToken)
                    .Select(f => f.GetAttribute<StringValueAttribute, MessageFields>())
                    .Where(att => att != null)
                    .Select(att => att.Text)
                    .ToList();

                if (otherFields.Any())
                {
                    if (sb.Length > 0)
                        sb.Append(",");

                    sb.Append(string.Join(",", otherFields));
                }

                SetField("fields", sb.ToString());
            };

            return this;
        }

        /// <summary>
        /// Maximum number of messages to return.
        /// </summary>
        /// <param name="maxResults"></param>
        /// <returns></returns>
        public MessageQueryStringBuilder SetMaxResults(ushort maxResults)
        {
            if (maxResults != ushort.MaxValue && maxResults != 0)
                SetField("maxResults", maxResults);

            return this;
        }

        /// <summary>
        /// Include messages from SPAM and TRASH in the results.
        /// </summary>
        /// <returns></returns>
        public MessageQueryStringBuilder SetIncludeSpamAndTrash(bool includeSpamAndTrash = false)
        {
            if (includeSpamAndTrash)// false is default, no need to include
                SetField("includeSpamTrash", "true");

            return this;
        }

        /// <summary>
        /// Only return messages with labels that match all of the specified label IDs.
        /// </summary>
        /// <param name="labelIds"></param>
        /// <returns></returns>
        public MessageQueryStringBuilder SetLabelIds(params string[] labelIds)
        {
            if (labelIds.Any())
            {
                SetField("labelIds", labelIds);
            }

            return this;
        }

        public MessageQueryStringBuilder SetFormat(MessageFormat format)
        {
            base.SetFormat(format);
            return this;
        }

        public MessageQueryStringBuilder SetMetadataHeaders(string metaData)//TODO: what is this?
        {
            // Required:
            SetFormat(MessageFormat.Metadata);

            SetField("metadataHeaders", metaData);
            return this;
        }

        /// <summary>
        /// Only return messages matching the specified query.
        /// Supports the same query format as the Gmail search box. For example, "from:someuser@example.com rfc822msgid: is:unread".
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public MessageQueryStringBuilder SetQuery(string query)//TODO: query builder?
        {
            SetField("q", query);
            return this;
        }

        /// <summary>
        /// Page token to retrieve a specific page of results in the list.
        /// </summary>
        /// <param name="pageToken"></param>
        /// <returns></returns>
        public MessageQueryStringBuilder SetPageToken(string pageToken)
        {
            SetField("pageToken", pageToken);
            return this;
        }

        public override string Build()
        {
            _fieldsAction?.Invoke();
            return base.Build();
        }
    }
}
