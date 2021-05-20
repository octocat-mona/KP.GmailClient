using System.Threading.Tasks;
using KP.GmailClient.Builders;
using KP.GmailClient.Common;
using KP.GmailClient.Common.Enums;
using KP.GmailClient.Models;

namespace KP.GmailClient.Services
{
    /// <summary>Service to get, create, update and delete email drafts.</summary>
    public class DraftService
    {
        private readonly GmailProxy _proxy;

        internal DraftService(GmailProxy proxy)
        {
            _proxy = proxy;
        }

        /// <summary>Gets the specified draft.</summary>
        /// <param name="id">The ID of the draft to retrieve.</param>
        public async Task<Draft> GetAsync(string id)
        {
            string queryString = new DraftQueryStringBuilder()
                .SetRequestAction(DraftRequestAction.Get, id)
                .Build();

            return await _proxy.Get<Draft>(queryString);
        }

        /// <summary>Creates a new draft with the DRAFT label.</summary>
        public async Task<Draft> CreateAsync(Draft draftInput)
        {
            string queryString = new DraftQueryStringBuilder()
                .SetRequestAction(DraftRequestAction.Create)
                //.SetUploadType(UploadType.Media)//TODO:
                .Build();

            return await _proxy.Post<Draft>(queryString, draftInput);
        }

        /// <summary>Lists the drafts ID's in the user's mailbox.</summary>
        public async Task<DraftList> ListIdsAsync()
        {
            string queryString = new DraftQueryStringBuilder()
                .SetRequestAction(DraftRequestAction.List)
                .Build();

            return await _proxy.Get<DraftList>(queryString);
        }

        /// <summary>Immediately and permanently deletes the specified draft. Does not simply trash it.</summary>
        /// <param name="id">The ID of the draft to delete</param>
        public async Task DeleteAsync(string id)
        {
            string queryString = new DraftQueryStringBuilder()
                .SetRequestAction(DraftRequestAction.Delete, id)
                .Build();

            await _proxy.Delete(queryString);
        }
    }
}
