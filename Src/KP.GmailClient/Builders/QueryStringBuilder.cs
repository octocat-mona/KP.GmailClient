using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KP.GmailClient.Common;
using KP.GmailClient.Common.Enums;

namespace KP.GmailClient.Builders
{
    internal abstract class QueryStringBuilder
    {
        protected readonly Dictionary<string, List<object>> ParametersDictionary;
        protected string Path { get; set; }

        protected QueryStringBuilder()
        {
            Path = string.Empty;
            ParametersDictionary = new Dictionary<string, List<object>>();
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
            {
                throw new ArgumentException("ID is required", nameof(id));
            }

            RequestAction requestAction = action.ParseEnumValue<RequestAction>();
            switch (requestAction)
            {
                case RequestAction.Patch:
                case RequestAction.Update:
                case RequestAction.Delete:
                case RequestAction.Get:
                    Path += "/" + id;
                    break;
                case RequestAction.Modify:
                    Path += $"/{id}/modify";
                    break;
                case RequestAction.Trash:
                    Path += $"/{id}/trash";
                    break;
                case RequestAction.Untrash:
                    Path += $"/{id}/untrash";
                    break;
                case RequestAction.Send:
                case RequestAction.Import:
                case RequestAction.List:
                case RequestAction.Insert:
                    throw new ArgumentException($"Action '{action}' does not require an ID", nameof(action));
                default:
                    throw new ArgumentOutOfRangeException(nameof(action));
            }
        }

        /// <summary>
        /// Set action which doesn't require an ID.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        protected void SetRequestAction(Enum action)
        {
            RequestAction requestAction = action.ParseEnumValue<RequestAction>();
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
                    throw new ArgumentException($"Action \'{action}\' requires an ID", nameof(action));
                default:
                    throw new ArgumentOutOfRangeException(nameof(action));
            }
        }

        protected void SetFormat(Enum format)
        {
            // Validate the passed argument
            Format f = format.ParseEnumValue<Format>();

            if (f != Format.Full)// Full is default
            {
                SetParameter("format", f);
            }
        }

        /// <summary>
        /// Add field(s) to the query string. Empty and null values are ignored.
        /// </summary>
        /// <param name="key">The key of the field</param>
        /// <param name="values">Zero or more nullable values.</param>
        protected void SetParameter(string key, params object[] values)
        {
            // Remove null values from collection
            List<object> nonNullValues = values
                .Where(value => !string.IsNullOrWhiteSpace(value?.ToString()))
                .ToList();

            if (!nonNullValues.Any())
            {
                return;
            }

            ParametersDictionary[key] = new List<object>(nonNullValues);
        }

        public virtual string Build()
        {
            var encodedValues = ParametersDictionary
                .SelectMany(kvp => kvp.Value, (d, value) => new { d.Key, Value = value.ToString() })
                .Select(kvp => string.Concat(kvp.Key, "=", HttpUtility.UrlEncode(kvp.Value)))
                .ToList();

            // Only set a questionmark if any values
            string questionMarkOrEmpty = encodedValues.Any() ? "?" : "";
            return string.Concat(Path, questionMarkOrEmpty, string.Join("&", encodedValues));
        }
    }
}