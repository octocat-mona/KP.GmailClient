using System.IO;
using KP.GmailClient.Common;
using KP.GmailClient.Models;
using Newtonsoft.Json;

namespace KP.GmailClient
{
    public class GmailClientInitializer
    {
        private static readonly JsonSerializer JsonSerializer;

        static GmailClientInitializer()
        {
            JsonSerializer = new JsonSerializer();
        }

        private GmailClientInitializer() { }

        internal ServiceAccountCredential AccountCredential { get; set; }
        internal string Scopes { get; set; }

        public static GmailClientInitializer Initialize(string path, GmailScopes scopes)
        {
            return Initialize(path, scopes.ToScopeString());
        }

        /// <summary>
        /// Initialize using a JSON file.
        /// </summary>
        /// <param name="path">The location of the JSON file containing API credentials</param>
        /// <param name="scopes">The Gmail scopes required to access the users data</param>
        /// <returns></returns>
        public static GmailClientInitializer Initialize(string path, string scopes)
        {
            using (Stream stream = File.OpenRead(path))
            using (StreamReader streamReader = new StreamReader(stream))
            using (JsonReader jsonReader = new JsonTextReader(streamReader))
            {
                var accountCredential = JsonSerializer.Deserialize<ServiceAccountCredential>(jsonReader);
                return new GmailClientInitializer
                {
                    AccountCredential = accountCredential,
                    Scopes = scopes
                };
            }
        }
    }
}