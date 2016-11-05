using System;
using System.Linq;
using KP.GmailClient.Common.Enums;

namespace KP.GmailClient.Builders
{
    internal class ThreadQueryStringBuilder : QueryStringBuilder
    {
        public ThreadQueryStringBuilder()
        {
            Path = "threads";
        }

        public ThreadQueryStringBuilder SetFormat(ThreadFormat format)
        {
            base.SetFormat(format);
            return this;
        }

        /// <summary>
        /// Set action which doesn't require an ID
        /// </summary>
        /// <param name="action">The action to set</param>
        /// <returns>This builder</returns>
        public ThreadQueryStringBuilder SetRequestAction(ThreadRequestAction action)
        {
            base.SetRequestAction(action);
            return this;
        }

        public ThreadQueryStringBuilder SetFields(ThreadFields fields)
        {
            throw new NotImplementedException();//TODO:
            //return this;
        }

        /// <summary>
        /// Set action which requires an ID
        /// </summary>
        /// <param name="action">The action to set</param>
        /// <param name="id">Id of the message</param>
        /// <returns>This builder</returns>
        public ThreadQueryStringBuilder SetRequestAction(ThreadRequestAction action, string id)
        {
            base.SetRequestAction(action, id);

            return this;
        }

        public ThreadQueryStringBuilder SetMetadataHeaders(string[] headers)
        {
            if (!headers.Any())
                throw new ArgumentException("Collection can't be empty", nameof(headers));

            SetFormat(ThreadFormat.Metadata);
            SetParameter("metadataHeaders", headers);

            return this;
        }

        /// <summary>
        /// Include threads from SPAM and TRASH in the results.
        /// </summary>
        /// <returns></returns>
        public ThreadQueryStringBuilder SetIncludeSpamAndTrash(bool includeSpamAndTrash = false)
        {
            if (includeSpamAndTrash)// false is default, no need to include
                SetParameter("includeSpamTrash", "true");

            return this;
        }

        /// <summary>
        /// Only return threads with labels that match all of the specified label IDs.
        /// </summary>
        /// <param name="labelIds"></param>
        /// <returns></returns>
        public ThreadQueryStringBuilder SetLabelIds(params string[] labelIds)
        {
            if (labelIds.Any())
                SetParameter("labelIds", labelIds);

            return this;
        }

        /// <summary>
        /// Maximum number of threads to return.
        /// </summary>
        /// <param name="maxResults"></param>
        /// <returns></returns>
        public ThreadQueryStringBuilder SetMaxResults(ushort maxResults)
        {
            if (maxResults != ushort.MaxValue && maxResults != 0)
                SetParameter("maxResults", maxResults);

            return this;
        }

        /// <summary>
        /// Page token to retrieve a specific page of results in the list.
        /// </summary>
        /// <param name="pageToken"></param>
        /// <returns></returns>
        public ThreadQueryStringBuilder SetPageToken(string pageToken)
        {
            SetParameter("pageToken", pageToken);
            return this;
        }

        /// <summary>
        /// Only return threads matching the specified query.
        /// Supports the same query format as the Gmail search box. For example, "from:someuser@example.com rfc822msgid: is:unread".
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ThreadQueryStringBuilder SetQuery(string query)
        {
            SetParameter("q", query);
            return this;
        }
    }
}
