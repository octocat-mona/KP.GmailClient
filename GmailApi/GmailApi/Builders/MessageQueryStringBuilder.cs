using System;
using System.Linq;
using System.Text;
using GmailApi.DTO;

namespace GmailApi.Builders
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

        public MessageQueryStringBuilder SetMaxResults(ushort maxResults)
        {
            SetField("maxResults", maxResults);
            return this;
        }

        public MessageQueryStringBuilder SetIncludeSpamTrash()
        {
            SetField("includeSpamTrash", "true"); // false is default, no need to include
            return this;
        }

        public MessageQueryStringBuilder SetLabelIds(params string[] labelIds)
        {
            if (labelIds.Any())
                SetField("labelIds", labelIds);

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

        public MessageQueryStringBuilder SetQuery(string query)//TODO: query builder?
        {
            if (!string.IsNullOrWhiteSpace(query))// Ignore when not set
                SetField("q", query);

            return this;
        }

        public override string Build()
        {
            if (_fieldsAction != null)
                _fieldsAction();

            return base.Build();
        }
    }
}
