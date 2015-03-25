using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GmailApi.Builders
{
    public abstract class QueryStringBuilder
    {
        protected readonly Dictionary<string, List<string>> Dictionary;
        protected string Path { get; set; }

        protected QueryStringBuilder()
        {
            Path = string.Empty;
            Dictionary = new Dictionary<string, List<string>>();
        }

        public virtual string Build()
        {
            var encodedValues = Dictionary
                .SelectMany(d => d.Value, (d, value) => new { d.Key, value })
                .Select(kvp => string.Concat(kvp.Key, "=", HttpUtility.UrlEncode(kvp.value)))
                .ToList();

            // Only set a questionmark if any values
            string questionMarkOrEmpty = encodedValues.Any() ? "?" : "";
            return string.Concat(Path, questionMarkOrEmpty, string.Join("&", encodedValues));
        }
    }
}