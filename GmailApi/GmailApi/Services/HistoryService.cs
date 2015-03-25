namespace GmailApi.Services
{
    public class HistoryService
    {
        private readonly GmailClient _client;

        public HistoryService(GmailClient client)
        {
            _client = client;
        }
    }
}
