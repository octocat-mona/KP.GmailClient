using System;
using GmailApi.DTO;
using GmailApi.Models;

namespace GmailApi.Services
{
    public class LabelService
    {
        private readonly GmailClient _client;

        public LabelService(GmailClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Lists all labels in the user's mailbox.
        /// </summary>
        /// <returns></returns>
        public Label List()
        {
            string queryString = new LabelQueryStringBuilder()
                .SetFields(LabelFields.All)
                .Build();

            return _client.Get<Label>(queryString);
        }

        public void Create()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The ID of the label to delete.
        /// </summary>
        /// <param name="id"></param>
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The ID of the label to update.
        /// </summary>
        /// <param name="id"></param>
        public void Update(string id)
        {
            throw new NotImplementedException();
        }
    }
}
