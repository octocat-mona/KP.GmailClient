using KP.GmailClient.Common;

namespace KP.GmailClient.Tests
{
    public class GmailHelper
    {
        public static string GetGmailScopesField(string name)
        {
            return ReflectionHelper.GetStaticFieldValue<string>(typeof(GmailExtensions), name);
        }
    }
}
