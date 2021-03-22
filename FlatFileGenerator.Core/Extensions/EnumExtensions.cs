// <copyright file="EnumExtensions.cs" company="KJDS Srinivasa Rao">
// Copyright (c) KJDS Srinivasa Rao. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FlatFileGenerator.Core.Extensions
{
    /// <summary>
    /// Contains extension methods for <see cref="System.Enum"/>.
    /// </summary>
    /// <typeparam name="T">Class.</typeparam>
    public static class EnumExtensions<T>
        where T : struct, IConvertible
    {
        /// <summary>
        /// Gets all the values present in the given enum.
        /// </summary>
        /// <param name="value">Enum value.</param>
        /// <returns>Enum values.</returns>
        public static IList<T> GetValues(Enum value)
        {
            var enumValues = new List<T>();

            foreach (FieldInfo fi in value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                enumValues.Add((T)Enum.Parse(value.GetType(), fi.Name, false));
            }

            return enumValues;
        }

        /// <summary>
        /// Parses an string value to enum.
        /// </summary>
        /// <param name="value">string enum value.</param>
        /// <returns>Parsed enum value.</returns>
        public static T Parse(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        /// <summary>
        /// Parses the given value wrt diplay attribute name property.
        /// </summary>
        /// <param name="value">string value.</param>
        /// <param name="ignoreCase">Should ignore string case.</param>
        /// <returns>Parsed enum values.</returns>
        public static T ParseDisplayName(string value, bool ignoreCase = true)
        {
            List<EnumResults> enumMetadata = GetEnumMetadata();

            T parsedEnum = default;
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

        /// <summary>
        /// Gets names of all the enum values for a given type.
        /// </summary>
        /// <param name="value">Enum type value.</param>
        /// <returns>List of all enum values.</returns>
        public static IList<string> GetNames(Enum value)
        {
            return value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public).Select(fi => fi.Name).ToList();
        }

        /// <summary>
        /// Gets a list of all <see cref="DisplayAttribute"/> <see cref="DisplayAttribute.Name"/> property values.
        /// </summary>
        /// <param name="value">Enum value.</param>
        /// <returns>List of all <see cref="DisplayAttribute"/> <see cref="DisplayAttribute.Name"/> property values. </returns>
        public static IList<string> GetDisplayValues(Enum value)
        {
            return GetNames(value).Select(obj => GetDisplayValue(Parse(obj))).ToList();
        }

        /// <summary>
        /// Gets the name of <see cref="DisplayAttribute.Name"/> property value.
        /// </summary>
        /// <param name="value">Enum value.</param>
        /// <returns>Associated <see cref="DisplayAttribute.Name"/> property value.</returns>
        public static string GetDisplayValue(T value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            var descriptionAttributes = fieldInfo.GetCustomAttributes(
                typeof(DisplayAttribute), false) as DisplayAttribute[];

            if (descriptionAttributes[0].ResourceType != null)
            {
                return LookupResource(descriptionAttributes[0].ResourceType, descriptionAttributes[0].Name);
            }

            if (descriptionAttributes == null)
            {
                return string.Empty;
            }

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
                        FieldInfo = fieldInfo,
                    };
                    enumMetadata.Add(enumResult);
                }
            }

            return enumMetadata;
        }

        /// <summary>
        /// Describes enum results.
        /// </summary>
        internal class EnumResults
        {
            /// <summary>
            /// Gets or sets DisplayAttribute.
            /// </summary>
            public DisplayAttribute DisplayAttribute { get; set; }

            /// <summary>
            /// Gets or sets FieldInfo.
            /// </summary>
            public FieldInfo FieldInfo { get; set; }
        }
    }
}
