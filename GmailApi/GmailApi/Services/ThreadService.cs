namespace ConsoleApplication1.Services
{
    public class ThreadService
    {
        private readonly GmailClient _client;

        public ThreadService(GmailClient client)
        {
            _client = client;
        }
    }
}
