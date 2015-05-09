using System.Collections.Generic;
using System.IO;
using System.Linq;
using KP.GmailApi.Models;

namespace KP.GmailApi.UnitTests.Extensions
{
    internal static class TokenManagerExtensions
    {
        public static Oauth2Token Token(this TokenManager tokenManager)
        {
            return ReflectionHelper.GetFieldValue<Oauth2Token>(tokenManager, "_token");
        }

        public static void DeleteFolder(this TokenManager tokenManager)
        {
            IDictionary<string, Oauth2Token> tokens = StaticTokens(tokenManager);
            string tokenFile = tokens.First().Key;
            var directory = new FileInfo(tokenFile).Directory;
            if (directory != null && directory.Exists)
                directory.Delete(true);
        }

        public static IDictionary<string, Oauth2Token> StaticTokens(this TokenManager tokenManager)
        {
            return ReflectionHelper.GetStaticFieldValue<IDictionary<string, Oauth2Token>>(typeof(TokenManager), "Tokens");
        }

        public static string TokenFile(this TokenManager tokenManager)
        {
            return ReflectionHelper.GetFieldValue<string>(tokenManager, "_tokenFile");
        }
    }
}
