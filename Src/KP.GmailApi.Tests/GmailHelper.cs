using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KP.GmailApi.Common;

namespace KP.GmailApi.Tests
{
    public class GmailHelper
    {
        public static string GetGmailScopesField(string name)
        {
            return ReflectionHelper.GetStaticFieldValue<string>(typeof(GmailExtensions), name);
        }
    }
}
