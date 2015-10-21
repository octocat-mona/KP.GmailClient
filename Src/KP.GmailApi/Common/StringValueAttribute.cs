using System;

namespace KP.GmailApi.Common
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    internal class StringValueAttribute : Attribute
    {
        public StringValueAttribute(string text)
        {
            Text = text;
        }

        /// <summary>
        /// the Text set on the Field
        /// </summary>
        public string Text { get; set; }
    }
}