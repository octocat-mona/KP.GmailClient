using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GmailApi.DTO;

namespace GmailApi.Builders
{
    public class MessageQueryStringBuilder : QueryStringBuilder
    {
        private Action _fieldsAction;

        public MessageQueryStringBuilder()
        {
            Path = "messages";
        }

        /// <summary>
        /// Set action which doesn't require an ID
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public MessageQueryStringBuilder SetRequestAction(MessageRequestAction action)
        {
            switch (action)
            {
                case MessageRequestAction.Import:
                    Path += "/import";
                    break;
                case MessageRequestAction.List:
                case MessageRequestAction.Insert:
                    break;
                default:
                    throw new Exception("Action '" + action + "' requires an ID");
            }

            return this;
        }

        /// <summary>
        /// Set action which requires an ID
        /// </summary>
        /// <param name="action"></param>
        /// <param name="id">Id of the message</param>
        /// <returns></returns>
        public MessageQueryStringBuilder SetRequestAction(MessageRequestAction action, string id)
        {
            switch (action)
            {
                //List, Insert: no extra path
                case MessageRequestAction.Delete:
                case MessageRequestAction.Get:
                    Path += "/" + id;
                    break;
                case MessageRequestAction.Modify:
                    Path += "/" + id + "/modify";
                    break;
                case MessageRequestAction.Send:
                    Path += "/send";
                    break;
                case MessageRequestAction.Trash:
                    Path += "/" + id + "/trash";
                    break;
                case MessageRequestAction.Untrash:
                    Path += "/" + id + "/untrash";
                    break;
            }

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

                Dictionary["fields"] = new List<string>(new[] { sb.ToString() });
            };

            return this;
        }

        public MessageQueryStringBuilder SetMaxResults(ushort maxResults)
        {
            Dictionary["maxResults"] = new List<string>(new[] { Convert.ToString(maxResults) });
            return this;
        }

        public MessageQueryStringBuilder SetIncludeSpamTrash()
        {
            Dictionary["includeSpamTrash"] = new List<string>(new[] { "true" }); // false is default, no need to include
            return this;
        }

        public MessageQueryStringBuilder SetLabelIds(params string[] labelIds)
        {
            if (labelIds.Any())
                Dictionary["labelIds"] = new List<string>(labelIds);

            return this;
        }

        public MessageQueryStringBuilder SetFormat(MessageFormat format)
        {
            if (format != MessageFormat.Full)// Full is default
                Dictionary["format"] = new List<string>(new[] { format.ToString() });

            return this;
        }

        public MessageQueryStringBuilder SetMetadataHeaders(string metaData)//TODO: what is this?
        {
            // Required:
            SetFormat(MessageFormat.Metadata);

            Dictionary["metadataHeaders"] = new List<string>(new[] { metaData });
            return this;
        }

        public MessageQueryStringBuilder SetQuery(string query)//TODO: query builder?
        {
            if (!string.IsNullOrWhiteSpace(query))// Ignore when not set
                Dictionary["q"] = new List<string>(new[] { query });

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
