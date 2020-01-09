using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FlatFileGenerator.Extensions
{
    public static class EnumExtensions<T> where T : struct, IConvertible
    {
        public static IList<T> GetValues(Enum value)
        {
            var enumValues = new List<T>();

            foreach (FieldInfo fi in value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                enumValues.Add((T)Enum.Parse(value.GetType(), fi.Name, false));
            }
            return enumValues;
        }

        public static T Parse(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static T ParseDisplayName(string value, bool ignoreCase = true)
        {
            List<EnumResults> enumMetadata = GetEnumMetadata();

            T parsedEnum = default(T);
            EnumResults currentEnumMetada;
            if (ignoreCase)
            {
                var comparison = StringComparison.InvariantCultureIgnoreCase;
                currentEnumMetada = enumMetadata.Where(x => x.DisplayAttribute.Name.Equals(value, comparison)).FirstOrDefault();
            }
            else
            {
                currentEnumMetada = enumMetadata.Where(x => x.DisplayAttribute.Name.Equals(value)).FirstOrDefault();
            }
            
            if (currentEnumMetada != null)
            {
                if (Enum.TryParse<T>(currentEnumMetada.FieldInfo.Name, out parsedEnum))
                {
                    return parsedEnum;
                }
                else
                {
                    throw new ArgumentNullException(value);
                }
            }
            else
            {
                throw new ArgumentNullException(value);
            }
        }

        public static IList<string> GetNames(Enum value)
        {
            return value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public).Select(fi => fi.Name).ToList();
        }

        public static IList<string> GetDisplayValues(Enum value)
        {
            return GetNames(value).Select(obj => GetDisplayValue(Parse(obj))).ToList();
        }

        public static string GetDisplayValue(T value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            var descriptionAttributes = fieldInfo.GetCustomAttributes(
                typeof(DisplayAttribute), false) as DisplayAttribute[];

            if (descriptionAttributes[0].ResourceType != null)
                return LookupResource(descriptionAttributes[0].ResourceType, descriptionAttributes[0].Name);

            if (descriptionAttributes == null) return string.Empty;
            return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name : value.ToString();
        }

        private static string LookupResource(Type resourceManagerProvider, string resourceKey)
        {
            foreach (PropertyInfo staticProperty in resourceManagerProvider.GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (staticProperty.PropertyType == typeof(System.Resources.ResourceManager))
                {
                    System.Resources.ResourceManager resourceManager = (System.Resources.ResourceManager)staticProperty.GetValue(null, null);
                    return resourceManager.GetString(resourceKey);
                }
            }

            return resourceKey; // Fallback with the key name
        }

        private static List<EnumResults> GetEnumMetadata()
        {
            var fieldsInfo = typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public).ToList();
            var enumMetadata = new List<EnumResults>();
            foreach (var fieldInfo in fieldsInfo)
            {
                var descriptionAttribute = fieldInfo.GetCustomAttribute<DisplayAttribute>();
                if (descriptionAttribute != null)
                {
                    var enumResult = new EnumResults
                    {
                        DisplayAttribute = descriptionAttribute,
                        FieldInfo = fieldInfo
                    };
                    enumMetadata.Add(enumResult);
                }
            }

            return enumMetadata;
        }
    }

    internal class EnumResults
    {
        public DisplayAttribute DisplayAttribute { get; set; }
        public FieldInfo FieldInfo { get; set; }
    }
}
