using System;

namespace ConsoleApplication1
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class StringValueAttribute : Attribute
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