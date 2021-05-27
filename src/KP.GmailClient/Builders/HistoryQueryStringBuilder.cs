using System;

namespace KP.GmailClient.Builders
{
    internal class HistoryQueryStringBuilder : QueryStringBuilder
    {
        private const string StarthistoryidName = "startHistoryId";

        public HistoryQueryStringBuilder()
        {
            Path = "history";
        }

        /// <summary>
        /// Only return messages with a label matching the ID.
        /// </summary>
        /// <param name="labelId">the label ID</param>
        /// <returns></returns>
        public HistoryQueryStringBuilder SetLabelId(string labelId)
        {
            SetParameter("labelId", labelId);
            return this;
        }

        /// <summary>
        /// The maximum number of history records to return.
        /// </summary>
        /// <param name="maxResults"></param>
        /// <returns></returns>
        public HistoryQueryStringBuilder SetMaxResults(uint maxResults)
        {
            // Only set when value is not 'default' (like '0' or MaxValue)
            if (maxResults != uint.MaxValue && maxResults != 0)
                SetParameter("maxResults", maxResults);

            return this;
        }

        /// <summary>
        /// Page token to retrieve a specific page of results in the list.
        /// </summary>
        /// <param name="pageToken"></param>
        /// <returns></returns>
        public HistoryQueryStringBuilder SetPageToken(string pageToken)
        {
            SetParameter("pageToken", pageToken);
            return this;
        }

        /// <summary>
        /// Required. Returns history records after the specified startHistoryId.
        /// The supplied startHistoryId should be obtained from the historyId of a message, thread, or previous list response.
        /// History IDs increase chronologically but are not contiguous with random gaps in between valid IDs.
        /// Supplying an invalid or out of date startHistoryId typically returns an HTTP 404 error code.
        /// A historyId is typically valid for at least a week, but in some circumstances may be valid for only a few hours.
        /// If you receive an HTTP 404 error response, your application should perform a full sync.
        /// If you receive no nextPageToken in the response, there are no updates to retrieve and you can store the returned historyId for a future request.
        /// </summary>
        /// <param name="startHistoryId"></param>
        /// <returns></returns>
        public HistoryQueryStringBuilder SetStartHistoryId(string startHistoryId)
        {
            SetParameter(StarthistoryidName, startHistoryId);
            return this;
        }

        public override string Build()
        {
            if (!ParametersDictionary.ContainsKey(StarthistoryidName))
                throw new Exception(string.Concat("Required ID ", StarthistoryidName, " not set"));

            return base.Build();
        }
    }
}