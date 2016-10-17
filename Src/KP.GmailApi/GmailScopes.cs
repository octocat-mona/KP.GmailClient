using System;

namespace KP.GmailApi
{
    /// <summary>
    /// Scopes defined by Gmail.
    /// </summary>
    [Flags]
    public enum GmailScopes
    {
        /// <summary>
        /// View your emails messages and settings.
        /// </summary>
        Readonly = 1,

        /// <summary>
        /// View and modify but not delete your email.
        /// </summary>
        Modify = 2,

        /// <summary>
        /// Manage drafts and send emails.
        /// </summary>
        Compose = 4,

        /// <summary>
        /// Insert mail into your mailbox.
        /// </summary>
        Insert = 8,

        /// <summary>
        /// Manage mailbox labels.
        /// </summary>
        Labels = 16,

        /// <summary>
        /// Send email on your behalf.
        /// </summary>
        Send = 32,

        /// <summary>
        /// Manage your basic mail settings.
        /// </summary>
        ManageBasicSettings = 64,

        /// <summary>
        /// Manage your sensitive mail settings, including who can manage your mail.
        /// Note: Operations guarded by this scope are restricted to administrative use only.
        /// They are only available to Google Apps customers using a service account with domain-wide delegation.
        /// </summary>
        ManageSensitiveSettings = 128,

        /// <summary>
        /// Full access to the account, including permanent deletion of threads and messages.
        /// This scope should only be requested if your application needs to immediately and permanently delete threads and messages,
        /// bypassing Trash; all other actions can be performed with less permissive scopes.
        /// </summary>
        FullAccess = 256
    }
}