using GmailApi.Builders;

namespace GmailApi.Services
{
    public class LabelService
    {
        private readonly GmailClient _client;

        public LabelService(GmailClient client)
        {
            _client = client;
        }

        public void List()
        {
            /*string queryString = new LabelQueryStringBuilder()
            .SetFields()
    .SetRequestAction(RequestAction.Trash, id)
    .Build();

            return _client.Post<Message>(queryString);*/
        }
    }

    public class LabelQueryStringBuilder: QueryStringBuilder
    {
    }
}
