using System.Collections.Generic;
using System.Threading.Tasks;
using KP.GmailClient.Builders;
using KP.GmailClient.Common;
using KP.GmailClient.Common.Enums;
using KP.GmailClient.Models;

namespace KP.GmailClient.Services
{
    /// <summary>
    /// Service to get, create, update and delete email labels.
    /// </summary>
    public class LabelService
    {
        private readonly GmailProxy _proxy;

        internal LabelService(GmailProxy proxy)
        {
            _proxy = proxy;
        }

        /// <summary>
        /// Gets the specified label.
        /// </summary>
        /// <param name="id">The ID of the label to retrieve.</param>
        /// <returns></returns>
        public async Task<Label> GetAsync(string id)
        {
            string queryString = new LabelQueryStringBuilder()
                .SetRequestAction(LabelRequestAction.Get, id)
                .Build();

            return await _proxy.Get<Label>(queryString);
        }

        /// <summary>
        /// Lists all labels in the user's mailbox.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<Label>> ListAsync()
        {
            string queryString = new LabelQueryStringBuilder()
                .Build();

            return await _proxy.Get<IList<Label>>(queryString, new ParseOptions { Path = "labels" });
        }

        /// <summary>
        /// Creates a new label.
        /// </summary>
        public async Task<Label> CreateAsync(CreateLabelInput labelInput)
        {
            string queryString = new LabelQueryStringBuilder()
                .Build();

            return await _proxy.Post<Label>(queryString, labelInput);
        }

        /// <summary>
        /// WARNING: Immediately and permanently deletes the specified label and removes it from any messages and threads that it is applied to.
        /// </summary>
        /// <param name="id">The ID of the label to delete.</param>
        public async Task DeleteAsync(string id)
        {
            string queryString = new LabelQueryStringBuilder()
                .SetRequestAction(LabelRequestAction.Delete, id)
                .Build();

            await _proxy.Delete(queryString);
        }

        /// <summary>
        /// Updates the specified label.
        /// </summary>
        /// <param name="labelInput"></param>
        public async Task<Label> UpdateAsync(UpdateLabelInput labelInput)
        {
            string queryString = new LabelQueryStringBuilder()
                .SetRequestAction(LabelRequestAction.Update, labelInput.Id)
                .Build();

            return await _proxy.Put<Label>(queryString, labelInput);
        }

        /// <summary>
        /// Updates the specified label. This method supports patch semantics.
        /// </summary>
        /// <param name="label"></param>
        public async Task<Label> PatchAsync(Label label)
        {
            string queryString = new LabelQueryStringBuilder()
                .SetRequestAction(LabelRequestAction.Update, label.Id)
                .Build();

            return await _proxy.Patch<Label>(queryString, label);
        }
    }
}
