using System.Text.Json;

namespace KP.GmailClient.Extensions
{
    internal static class JsonExtensions
    {
        internal static T ToObject<T>(this JsonElement element)
        {
            string json = element.GetRawText();
            return JsonSerializer.Deserialize<T>(json);
        }

        internal static T ToObject<T>(this JsonDocument document)
        {
            string json = document.RootElement.GetRawText();
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
