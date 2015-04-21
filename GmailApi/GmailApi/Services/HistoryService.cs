using GmailApi.Builders;
using GmailApi.Models;

namespace GmailApi.Services
{
    public class HistoryService
    {
        private readonly GmailClient _client;

        public HistoryService(GmailClient client)
        {
            _client = client;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startHistoryId">Returns history records after the specified startHistoryId</param>
        /// <param name="labelId">Only return messages with a label matching the ID</param>
        /// <param name="pageToken">Page token to retrieve a specific page of results in the list</param>
        /// <param name="maxResults">The maximum number of history records to return. Use zero ('0') to use the default.</param>
        /// <returns></returns>
        public HistoryList List(ulong startHistoryId, string labelId = null, string pageToken = null, uint maxResults = 0)
        {
            string queryString = new HistoryQueryStringBuilder()
                .SetStartHistoryId(startHistoryId)
                .SetLabelId(labelId)
                .SetPageToken(pageToken)
                .SetMaxResults(maxResults)
                .Build();

            return _client.Get<HistoryList>(queryString);
        }
    }
}