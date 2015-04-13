using System.Collections.Generic;
using GmailApi.Builders;
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
        /// Gets the specified label.
        /// </summary>
        /// <param name="id">The ID of the label to retrieve.</param>
        /// <returns></returns>
        public Label Get(string id)
        {
            string queryString = new LabelQueryStringBuilder()
                .SetRequestAction(LabelRequestAction.Get, id)
                .Build();

            return _client.Get<Label>(queryString);
        }

        /// <summary>
        /// Lists all labels in the user's mailbox.
        /// </summary>
        /// <returns></returns>
        public List<Label> List()
        {
            string queryString = new LabelQueryStringBuilder()
                .SetFields(LabelFields.All)
                .Build();

            return _client.Get<List<Label>>(queryString, new ParseOptions { Path = "labels" });
        }

        /// <summary>
        /// Creates a new label.
        /// </summary>
        public Label Create(CreateLabelInput labelInput)
        {
            string queryString = new LabelQueryStringBuilder()
                .Build();

            return _client.Post<Label>(queryString, labelInput);
        }

        /// <summary>
        /// NOTE: Immediately and permanently deletes the specified label and removes it from any messages and threads that it is applied to.
        /// </summary>
        /// <param name="id">The ID of the label to delete.</param>
        public void Delete(string id)
        {
            string queryString = new LabelQueryStringBuilder()
                .SetRequestAction(LabelRequestAction.Delete, id)
                .Build();

            _client.Delete<object>(queryString);
        }

        /// <summary>
        /// Updates the specified label.
        /// </summary>
        /// <param name="labelInput"></param>
        public Label Update(UpdateLabelInput labelInput)
        {
            string queryString = new LabelQueryStringBuilder()
                .SetRequestAction(LabelRequestAction.Update, labelInput.Id)
                .Build();

            return _client.Put<Label>(queryString, labelInput);
        }

        /// <summary>
        /// Updates the specified label. This method supports patch semantics.
        /// </summary>
        /// <param name="label"></param>
        public Label Patch(Label label)
        {
            string queryString = new LabelQueryStringBuilder()
                .SetRequestAction(LabelRequestAction.Update, label.Id)
                .Build();

            return _client.Patch<Label>(queryString, label);
        }
    }
}
