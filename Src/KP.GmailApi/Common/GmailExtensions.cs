using System;

namespace KP.GmailApi.Common
{
    internal static class GmailExtensions
    {
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
                        scopeValueStrings[i] = "https://www.googleapis.com/auth/gmail.readonly";
                        break;
                    case GmailScopes.Modify:
                        scopeValueStrings[i] = "https://www.googleapis.com/auth/gmail.modify";
                        break;
                    case GmailScopes.Compose:
                        scopeValueStrings[i] = "https://www.googleapis.com/auth/gmail.compose";
                        break;
                    case GmailScopes.Insert:
                        scopeValueStrings[i] = "https://www.googleapis.com/auth/gmail.insert";
                        break;
                    case GmailScopes.Labels:
                        scopeValueStrings[i] = "https://www.googleapis.com/auth/gmail.labels";
                        break;
                    case GmailScopes.Send:
                        scopeValueStrings[i] = "https://www.googleapis.com/auth/gmail.send";
                        break;
                    case GmailScopes.ManageBasicSettings:
                        scopeValueStrings[i] = "https://www.googleapis.com/auth/gmail.settings.basic";
                        break;
                    case GmailScopes.ManageSensitiveSettings:
                        scopeValueStrings[i] = "https://www.googleapis.com/auth/gmail.settings.sharing";
                        break;
                    case GmailScopes.FullAccess:
                        scopeValueStrings[i] = "https://mail.google.com/";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(gmailScopes));
                }
            }

            return string.Join(" ", scopeValueStrings);
        }
    }
}
