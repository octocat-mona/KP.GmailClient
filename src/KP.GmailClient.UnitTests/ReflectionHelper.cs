using System;
using System.Reflection;

namespace KP.GmailClient.UnitTests
{
    public class ReflectionHelper
    {
        private const BindingFlags Flags =
            BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.Instance |
            BindingFlags.Static |
            BindingFlags.Public |
            BindingFlags.Default;

        public static T GetStaticFieldValue<T>(Type objecType, string fieldName)
        {
            FieldInfo field = objecType.GetField(fieldName, Flags);
            if (field == null)
            {
                throw new Exception();
            }

            return (T) field.GetValue(null);
        }
    }
}