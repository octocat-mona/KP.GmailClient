namespace GmailApi.Services
{
    public class DraftService
    {
        private readonly GmailClient _client;

        public DraftService(GmailClient client)
        {
            _client = client;
        }
    }
}
