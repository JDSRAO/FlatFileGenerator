// <copyright file="RandomGenerator.cs" company="KJDS Srinivasa Rao">
// Copyright (c) KJDS Srinivasa Rao. All rights reserved.
// </copyright>

using FlatFileGenerator.Core.Extensions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace FlatFileGenerator.Core.Models
{
    /// <summary>
    /// Describes random value generator based on the column configuration.
    /// </summary>
    internal class RandomGenerator
    {
        private static Random random = new Random();

        // Gets a NumberFormatInfo associated with the en-US culture.
        private static NumberFormatInfo nfi = CultureInfo.InvariantCulture.NumberFormat;

        /// <summary>
        /// Generates randome string based on the <see cref="ColumnTypeStringConfiguration"/>.
        /// </summary>
        /// <param name="config">String configuration.</param>
        /// <returns>Ramdom string.</returns>
        public static string RandomString(Dictionary<string, object> config)
        {
            string lenghtConfig = config.GetValueOrExpected<string>(ColumnTypeStringConfiguration.Length, null);
            string textCaseConfig = config.GetValueOrExpected<string>(ColumnTypeStringConfiguration.Case, null);

            ColumnTypeStringCaseConfiguration textCase;
            if (string.IsNullOrEmpty(textCaseConfig))
            {
                textCase = ColumnTypeStringCaseConfiguration.Sentence;
            }
            else
            {
                textCase = EnumExtensions<ColumnTypeStringCaseConfiguration>.Parse(textCaseConfig);
            }

            var textSeperator = EnumExtensions<ColumnTypeStringCaseConfiguration>.GetDisplayValue(textCase);

            var generatedString = new StringBuilder();
            var textInfo = CultureInfo.CurrentCulture.TextInfo;
            if (string.IsNullOrEmpty(lenghtConfig) || string.IsNullOrWhiteSpace(lenghtConfig))
            {
                int size = config.GetValueOrExpected<int>(ColumnTypeStringConfiguration.Length, 5);
                var text = GenerateString(size);
                var formattedText = StringCaseFormatter(textCase, text, textInfo, true);
                generatedString.Append(formattedText);
            }
            else
            {
                var wordLengths = lenghtConfig.Split('|');
                for (int i = 0; i < wordLengths.Length; i++)
                {
                    var text = GenerateString(Convert.ToInt32(wordLengths[i]));
                    var formattedText = StringCaseFormatter(textCase, text, textInfo, i == 0);
                    generatedString.Append(formattedText + textSeperator);
                }

                generatedString.Remove(generatedString.Length - 1, 1);
            }

            var randomString = new StringBuilder(generatedString.ToString().Trim());

            var prefix = config.GetValueOrExpected<string>(ColumnTypeStringConfiguration.Prefix, string.Empty);
            var suffix = config.GetValueOrExpected<string>(ColumnTypeStringConfiguration.Suffix, string.Empty);
            if (!string.IsNullOrEmpty(prefix))
            {
                randomString.Prepend(prefix);
            }

            if (!string.IsNullOrEmpty(suffix))
            {
                randomString.Append(suffix);
            }

            return randomString.ToString();
        }

        /// <summary>
        /// Generates ramdom integer based on <see cref="ColumnTypeIntegerConfiguration"/>.
        /// </summary>
        /// <param name="column">Integer configuration.</param>
        /// <returns>Ramdon Integer.</returns>
        public static string RandomInt(ref Column column)
        {
            int min = column.Config.GetValueOrExpected<int>(ColumnTypeIntegerConfiguration.Min, 1);
            bool increment = column.Config.GetValueOrExpected<bool>(ColumnTypeIntegerConfiguration.Increment, false);
            int value;
            if (increment)
            {
                var interval = column.Config.GetValueOrExpected<int>(ColumnTypeIntegerConfiguration.Interval, 1);
                value = min;
                min += interval;
                column.Config[ColumnTypeIntegerConfiguration.Min] = min;
            }
            else
            {
                int max = column.Config.GetValueOrExpected<int>(ColumnTypeIntegerConfiguration.Max, 1001);
                if (min > max)
                {
                    max = min + 1000;
                }

                value = random.Next(min, max);
            }

            var prefix = column.Config.GetValueOrExpected<string>(ColumnTypeIntegerConfiguration.Prefix, string.Empty);
            var suffix = column.Config.GetValueOrExpected<string>(ColumnTypeIntegerConfiguration.Suffix, string.Empty);
            var number = new StringBuilder(value);
            if (!string.IsNullOrEmpty(prefix))
            {
                number.Prepend(prefix);
            }

            if (!string.IsNullOrEmpty(suffix))
            {
                number.Append(suffix);
            }

            return number.ToString();
        }

        /// <summary>
        /// Generates random date.
        /// </summary>
        /// <param name="config">Date configuration.</param>
        /// <returns>Random Date.</returns>
        public static string RandomDate(Dictionary<string, object> config)
        {
            string format = config.GetValueOrExpected<string>(ColumnTypeDateConfiguration.Format, ColumnTypeDateConfiguration.DefaultFormat);
            var minYear = config.GetValueOrExpected<int>(ColumnTypeDateConfiguration.MinYear, 1);
            var maxYear = config.GetValueOrExpected<int>(ColumnTypeDateConfiguration.MaxYear, DateTime.Now.Year);

            var currentYear = random.Next(minYear, maxYear + 1);
            var currentMonth = random.Next(1, 13);
            var currentDay = random.Next(1, DateTime.DaysInMonth(currentYear, currentMonth) + 1);
            return new DateTime(currentYear, currentMonth, currentDay).ToString(format);
        }

        /// <summary>
        /// Generates either true/false.
        /// </summary>
        /// <param name="config">Configuration.</param>
        /// <returns>True/False.</returns>
        public static bool RandomBool(Dictionary<string, object> config)
        {
            var defaultBoolean = new bool[] { true, false };
            var index = random.Next(0, 2);
            return defaultBoolean[index];
        }

        /// <summary>
        /// Returns default value specified in the column configuration.
        /// </summary>
        /// <param name="config">Column Configuration.</param>
        /// <returns>Specified default value.</returns>
        public static object RandomDefault(Dictionary<string, object> config)
        {
            var containsDefaultValue = config.ContainsKey(ColumnTypeDefaultConfiguration.DefaultValue);
            if (containsDefaultValue)
            {
                return config[ColumnTypeDefaultConfiguration.DefaultValue];
            }
            else
            {
                throw new ArgumentNullException(ColumnTypeDefaultConfiguration.DefaultValue);
            }
        }

        /// <summary>
        /// Generates a ramdom email of the format {}@{}.{}.
        /// </summary>
        /// <param name="config">Column Configuration.</param>
        /// <returns>Ramdom email.</returns>
        public static string RandomEmail(Dictionary<string, object> config)
        {
            var email = new StringBuilder();
            email.Append(GenerateString(5));
            email.Append("@");
            email.Append(GenerateString(5));
            email.Append(".");
            email.Append(GenerateString(3));
            return email.ToString().ToLower();
        }

        /// <summary>
        /// Generates a ramdom decimal number.
        /// </summary>
        /// <param name="config">Column Configuration.</param>
        /// <returns>Ramdon decimal.</returns>
        public static string RandomDecimal(Dictionary<string, object> config)
        {
            string format = "{0:F" + config.GetValueOrExpected<int>(ColumnTypeDecimalConfiguration.DecimalPart, ColumnTypeDecimalConfiguration.DefaultDecimalPart) + "}";
            int min = config.GetValueOrExpected<int>(ColumnTypeIntegerConfiguration.Min, 1);
            int max = config.GetValueOrExpected<int>(ColumnTypeIntegerConfiguration.Min, 1000);
            var value = (random.NextDouble() * (max - min)) + min;
            return string.Format(format, value);
        }

        /// <summary>
        /// Picks a random value from the list.
        /// </summary>
        /// <param name="config">Column Configuration.</param>
        /// <returns>Ramdom value from list.</returns>
        public static object RandomValueFromList(Dictionary<string, object> config)
        {
            if (config.Count == 0 || !config.ContainsKey(ColumnTypeListConfiguration.Items))
            {
                throw new ArgumentNullException(ColumnTypeListConfiguration.Items);
            }
            else
            {
                dynamic items;
                int itemsCount;
                if (config[ColumnTypeListConfiguration.Items] is object[] listItems)
                {
                    items = listItems;
                    itemsCount = items.Length;
                }
                else if (config[ColumnTypeListConfiguration.Items] is JArray array)
                {
                    items = array;
                    itemsCount = items.Count;
                }
                else
                {
                    throw new InvalidOperationException("Unidentified data type");
                }

                var index = random.Next(0, itemsCount);
                return items[index];
            }
        }

        /// <summary>
        /// Generates randome guid.
        /// </summary>
        /// <returns>Ramdon guid.</returns>
        public static string RandomGuid()
        {
            return Guid.NewGuid().ToString();
        }

        private static string GenerateString(int size)
        {
            var builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor((26 * random.NextDouble()) + 97)));
                builder.Append(ch);
            }

            var randomString = builder.ToString();
            return randomString;
        }

        private static string StringCaseFormatter(ColumnTypeStringCaseConfiguration stringCase, string value, TextInfo textInfo, bool isFirst)
        {
            string formattedText;
            switch (stringCase)
            {
                case ColumnTypeStringCaseConfiguration.Upper:
                    formattedText = textInfo.ToUpper(value);
                    break;
                case ColumnTypeStringCaseConfiguration.Lower:
                    formattedText = textInfo.ToLower(value);
                    break;
                case ColumnTypeStringCaseConfiguration.Title:
                    formattedText = textInfo.ToTitleCase(value);
                    break;
                case ColumnTypeStringCaseConfiguration.Sentence:
                    formattedText = isFirst ? textInfo.ToTitleCase(value) : textInfo.ToLower(value);
                    break;
                case ColumnTypeStringCaseConfiguration.Camel:
                    formattedText = isFirst ? textInfo.ToLower(value) : textInfo.ToTitleCase(value);
                    break;
                case ColumnTypeStringCaseConfiguration.Pascal:
                    formattedText = textInfo.ToTitleCase(value);
                    break;
                case ColumnTypeStringCaseConfiguration.Snake:
                case ColumnTypeStringCaseConfiguration.Kebab:
                    formattedText = textInfo.ToLower(value);
                    break;
                default:
                    throw new InvalidOperationException($"String case of type {stringCase} is not valid");
            }

            return formattedText;
        }
    }
}
