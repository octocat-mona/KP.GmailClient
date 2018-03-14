using System;

namespace KP.GmailClient.Common
{
    internal static class GmailExtensions
    {
        private const string ReadOnlyScope = "https://www.googleapis.com/auth/gmail.readonly";
        private const string ModifyScope = "https://www.googleapis.com/auth/gmail.modify";
        private const string ComposeScope = "https://www.googleapis.com/auth/gmail.compose";
        private const string InsertScope = "https://www.googleapis.com/auth/gmail.insert";
        private const string LabelsScope = "https://www.googleapis.com/auth/gmail.labels";
        private const string SendScope = "https://www.googleapis.com/auth/gmail.send";
        private const string ManageBasicSettingsScope = "https://www.googleapis.com/auth/gmail.settings.basic";
        private const string ManageSensitiveSettingsScope = "https://www.googleapis.com/auth/gmail.settings.sharing";
        private const string FullAccessScope = "https://mail.google.com/";

        /// <summary>
        /// Convert <see cref="GmailScopes"/> to a by space separated string.
        /// </summary>
        /// <returns></returns>
        public static string ToScopeString(this GmailScopes gmailScopes)
        {
            GmailScopes[] scopes = gmailScopes.GetFlagEnumValues();
            string[] scopeValueStrings = new string[scopes.Length];
            for (int i = 0; i < scopes.Length; i++)
            {
                switch (scopes[i])
                {
                    case GmailScopes.Readonly:
                        scopeValueStrings[i] = ReadOnlyScope;
                        break;
                    case GmailScopes.Modify:
                        scopeValueStrings[i] = ModifyScope;
                        break;
                    case GmailScopes.Compose:
                        scopeValueStrings[i] = ComposeScope;
                        break;
                    case GmailScopes.Insert:
                        scopeValueStrings[i] = InsertScope;
                        break;
                    case GmailScopes.Labels:
                        scopeValueStrings[i] = LabelsScope;
                        break;
                    case GmailScopes.Send:
                        scopeValueStrings[i] = SendScope;
                        break;
                    case GmailScopes.ManageBasicSettings:
                        scopeValueStrings[i] = ManageBasicSettingsScope;
                        break;
                    case GmailScopes.ManageSensitiveSettings:
                        scopeValueStrings[i] = ManageSensitiveSettingsScope;
                        break;
                    case GmailScopes.FullAccess:
                        scopeValueStrings[i] = FullAccessScope;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(gmailScopes));
                }
            }

            return string.Join(" ", scopeValueStrings);
        }
    }
}
