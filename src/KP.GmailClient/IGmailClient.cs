using System;
using System.Threading.Tasks;
using KP.GmailClient.Models;
using KP.GmailClient.Services;

namespace KP.GmailClient
{
    /// <summary>Contains all services provided by Gmail.</summary>
    public interface IGmailClient : IDisposable
    {
        /// <summary>Service to get, create, update and delete emails.</summary>
        MessageService Messages { get; }

        /// <summary>Service to get, create, update and delete email drafts.</summary>
        DraftService Drafts { get; }

        /// <summary>Service to get, create, update and delete email labels.</summary>
        LabelService Labels { get; }

        /// <summary>Service for getting email threads.</summary>
        ThreadService Threads { get; }

        /// <summary>Service for getting the history of emails.</summary>
        HistoryService History { get; }

        /// <summary>Gets the current user's Gmail profile.</summary>
        Task<Profile> GetProfileAsync();
    }
}