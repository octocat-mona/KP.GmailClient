using System;
using System.Reflection;

namespace KP.GmailApi.UnitTests
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

        public static T GetFieldValue<T>(object objectInstance, string fieldName)
        {
            FieldInfo field = objectInstance.GetType().GetField(fieldName, Flags);
            if (ReferenceEquals(field, null))
                throw new Exception();

            return (T)field.GetValue(objectInstance);
        }

        public static T GetStaticFieldValue<T>(Type objecType, string fieldName)
        {
            FieldInfo field = objecType.GetField(fieldName, Flags);
            if (ReferenceEquals(field, null))
                throw new Exception();

            return (T)field.GetValue(null);
        }
    }
}
