namespace KP.GmailClient.Common
{
    internal class ParseOptions
    {
        public ParseOptions(string path)
        {
            Path = path;
        }

        /// <summary>
        /// A JPath expression.
        /// </summary>
        public string Path { get; set; }
    }
}