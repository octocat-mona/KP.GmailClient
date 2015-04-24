using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using GmailApi;
using GmailApi.Models;

namespace UnitTests.Extensions
{
    public static class TokenManagerExtensions
    {
        public static Oauth2Token Token(this TokenManager tokenManager)
        {
            FieldInfo field = tokenManager.GetType().GetField("_token", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Default);
            if (ReferenceEquals(field, null))
                throw new Exception();

            return (Oauth2Token)field.GetValue(tokenManager);
        }

        public static void DeleteFolder(this TokenManager tokenManager)
        {
            FieldInfo field = tokenManager.GetType().GetField("Tokens", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.Default);
            if (ReferenceEquals(field, null))
                throw new Exception();

            IDictionary<string, Oauth2Token> tokens = (IDictionary<string, Oauth2Token>)field.GetValue(tokenManager);
            string tokenFile = tokens.First().Key;
            var directory = new FileInfo(tokenFile).Directory;
            if (directory != null && directory.Exists)
                directory.Delete(true);
        }
    }
}
