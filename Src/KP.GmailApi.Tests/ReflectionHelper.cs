using System;
using System.IO;
using System.Reflection;

namespace KP.GmailApi.Tests
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
            var field = GetFieldName<T>(objectInstance, fieldName);
            return (T)field.GetValue(objectInstance);
        }

        public static T GetStaticFieldValue<T>(Type objecType, string fieldName)
        {
            FieldInfo field = objecType.GetField(fieldName, Flags);
            if (ReferenceEquals(field, null))
                throw new Exception();

            return (T)field.GetValue(null);
        }

        public static void SetFieldValue<T>(object objectInstance, string fieldName, object value)
        {
            var field = GetFieldName<T>(objectInstance, fieldName);
            field.SetValue(objectInstance, value);
        }

        private static FieldInfo GetFieldName<T>(object objectInstance, string fieldName)
        {
            FieldInfo field = objectInstance.GetType().GetField(fieldName, Flags);
            if (ReferenceEquals(field, null))
                throw new Exception($"Could not find field with name '{fieldName}'");
            return field;
        }

        /// <summary>
        /// Get the path of the executing assembly.
        /// Use this to get the reliable location also for the unit test runner.
        /// </summary>
        /// <returns></returns>
        public static string GetExecutingAssemblyPath()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }

    }
}
