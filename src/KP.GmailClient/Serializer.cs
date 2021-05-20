using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KP.GmailClient
{
    internal static class Serializer
    {
        private static JsonSerializer _jsonSerializer = CreateDefaultSerializer();

        public static JsonSerializer JsonSerializer
        {
            get => _jsonSerializer;
            set => _jsonSerializer = value ?? throw new ArgumentNullException(nameof(JsonSerializer));
        }

        internal static void Serialize(string path, object value)
        {
            using (var streamReader = new StreamWriter(path, false))
            using (var jsonWriter = new JsonTextWriter(streamReader))
            {
                JsonSerializer.Serialize(jsonWriter, value);
            }
        }

        internal static T Deserialize<T>(string path)
        {
            using (var stream = File.OpenRead(path))
            {
                return Deserialize<T>(stream);
            }
        }

        internal static T Deserialize<T>(Stream stream)
        {
            using (var streamReader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                return JsonSerializer.Deserialize<T>(jsonReader);
            }
        }

        private static JsonSerializer CreateDefaultSerializer()
        {
            return new JsonSerializer
            {
                Converters = { new StringEnumConverter { CamelCaseText = true } }
            };
        }
    }
}
