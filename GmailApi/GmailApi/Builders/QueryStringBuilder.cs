using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using GmailApi.DTO;

namespace GmailApi.Builders
{
    internal abstract class QueryStringBuilder
    {
        protected readonly Dictionary<string, List<string>> Dictionary;
        protected string Path { get; set; }

        protected QueryStringBuilder()
        {
            Path = string.Empty;
            Dictionary = new Dictionary<string, List<string>>();
        }

        /// <summary>
        /// Set action which requires an ID
        /// </summary>
        /// <param name="action"></param>
        /// <param name="id">Id of the message</param>
        /// <returns></returns>
        protected void SetRequestAction(Enum action, string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("ID is required", "id");

            RequestAction requestAction = ParseEnumValue(action);
            switch (requestAction)
            {
                case RequestAction.Patch:
                case RequestAction.Update:
                case RequestAction.Delete:
                case RequestAction.Get:
                    Path += "/" + id;
                    break;
                case RequestAction.Modify:
                    Path += "/" + id + "/modify";
                    break;
                case RequestAction.Trash:
                    Path += "/" + id + "/trash";
                    break;
                case RequestAction.Untrash:
                    Path += "/" + id + "/untrash";
                    break;
                case RequestAction.Send:
                case RequestAction.Import:
                case RequestAction.List:
                case RequestAction.Insert:
                    throw new ArgumentException("Action '" + action + "' does not require an ID");
                default:
                    throw new ArgumentOutOfRangeException("action");
            }
        }

        /// <summary>
        /// Set action which doesn't require an ID.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        protected void SetRequestAction(Enum action)
        {
            RequestAction requestAction = ParseEnumValue(action);
            switch (requestAction)
            {
                case RequestAction.Send:
                    Path += "/send";
                    break;
                case RequestAction.Import:
                    Path += "/import";
                    break;
                case RequestAction.Create:
                case RequestAction.List:
                case RequestAction.Insert:
                    break;
                case RequestAction.Patch:
                case RequestAction.Update:
                case RequestAction.Delete:
                case RequestAction.Get:
                case RequestAction.Modify:
                case RequestAction.Trash:
                case RequestAction.Untrash:
                    throw new ArgumentException("Action '" + action + "' requires an ID");
                default:
                    throw new ArgumentOutOfRangeException("action");
            }
        }

        private static RequestAction ParseEnumValue(Enum action)
        {
            string value = action.ToString();

            if (!Enum.IsDefined(typeof(RequestAction), value))
                throw new InvalidEnumArgumentException(string.Concat("'", value, "' not valid"), Convert.ToInt32(action), typeof(Action));

            return (RequestAction)Enum.Parse(typeof(RequestAction), value);
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