using System;
using KP.GmailApi.Builders;
using KP.GmailApi.Common.Enums;
using KP.GmailApi.Models;

namespace KP.GmailApi.Services
{
    /// <summary>
    /// Service to get, create, update and delete email drafts.
    /// </summary>
    public class DraftService
    {
        private readonly GmailClient _client;

        internal DraftService(GmailClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Creates a new draft with the DRAFT label.
        /// </summary>
        public Draft Create(Draft draftInput)
        {
            string queryString = new DraftQueryStringBuilder()
                .SetRequestAction(DraftRequestAction.Create)
                //.SetUploadType(UploadType.Media)//TODO:
                .Build();

            return _client.Post<Draft>(queryString, draftInput);
        }

        /// <summary>
        /// Immediately and permanently deletes the specified draft. Does not simply trash it.
        /// </summary>
        /// <param name="id">The ID of the draft to delete</param>
        public void Delete(string id)
        {
            string queryString = new DraftQueryStringBuilder()
                .SetRequestAction(DraftRequestAction.Delete, id)
                .Build();

            _client.Delete(queryString);
        }

        /// <summary>
        /// Gets the specified draft.
        /// </summary>
        /// <param name="id">The ID of the draft to retrieve.</param>
        public Draft Get(string id)
        {
            string queryString = new DraftQueryStringBuilder()
                .SetRequestAction(DraftRequestAction.Get, id)
                .Build();

            return _client.Get<Draft>(queryString);
        }

        /// <summary>
        /// Lists the drafts ID's in the user's mailbox.
        /// </summary>
        public DraftList ListIds()
        {
            string queryString = new DraftQueryStringBuilder()
                .SetRequestAction(DraftRequestAction.List)
                .Build();

            return _client.Get<DraftList>(queryString);
        }

        /// <summary>
        /// Replaces a draft's content.
        /// </summary>
        public void Update()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sends the specified, existing draft to the recipients in the To, Cc, and Bcc headers.
        /// </summary>
        public void Send()
        {
            throw new NotImplementedException();
        }
    }
}
