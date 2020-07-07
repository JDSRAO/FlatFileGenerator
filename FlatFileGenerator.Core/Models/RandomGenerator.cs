using FlatFileGenerator.Core.Extensions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace FlatFileGenerator.Core.Models
{
    internal class RandomGenerator
    {
        private static Random random { get; } = new Random();
        
        // Gets a NumberFormatInfo associated with the en-US culture.
        private static NumberFormatInfo nfi { get; set; } = CultureInfo.InvariantCulture.NumberFormat;

        //public static dynamic Value<T>(Dictionary<string, string> config)
        //{
        //    var currentType = typeof(T);
        //    dynamic value;
        //    if (currentType == typeof(string))
        //    {
        //        value = RandomString(config);
        //    }
        //    else if(currentType == typeof(int))
        //    {
        //        value = RandomInt(config);
        //    }
        //    else if(currentType == typeof(DateTime))
        //    {
        //        value = RandomDate(config);
        //    }
        //    else
        //    {
        //        throw new InvalidOperationException($"Type of {currentType} is invalid");
        //    }
        //    return value;
        //}

        public static string RandomString(Dictionary<string, object> config)
        {
            string lenghtConfig = config.GetValueOrExpected<string>(StringConfig.Length, null);
            string textCaseConfig = config.GetValueOrExpected<string>(StringConfig.Case, null);

            StringCase textCase;
            if(string.IsNullOrEmpty(textCaseConfig))
            {
                textCase = StringCase.Sentence;
            }
            else
            {
                textCase = EnumExtensions<StringCase>.Parse(textCaseConfig);
            }

            var textSeperator = EnumExtensions<StringCase>.GetDisplayValue(textCase);

            var generatedString = new StringBuilder();
            var textInfo = CultureInfo.CurrentCulture.TextInfo;
            if(string.IsNullOrEmpty(lenghtConfig) || string.IsNullOrWhiteSpace(lenghtConfig))
            {
                int size = config.GetValueOrExpected<int>(StringConfig.Length, 5);
                var text = GenerateString(size);
                var formattedText = StringCaseFormatter(textCase, text, textInfo, true);
                generatedString.Append(formattedText);
            }
            else
            {
                var wordLengths = lenghtConfig.Split('|');
                int size = 5;
                for (int i = 0; i < wordLengths.Length; i++)
                {
                    size = Convert.ToInt32(wordLengths[i]);
                    var text = GenerateString(size);
                    var formattedText = StringCaseFormatter(textCase, text, textInfo, i == 0);
                    generatedString.Append(formattedText + textSeperator);
                }
                generatedString.Remove(generatedString.Length - 1, 1);
            }

            string randomString = generatedString.ToString().Trim();

            var prefix = config.GetValueOrExpected<string>(StringConfig.Prefix, string.Empty);
            var suffix = config.GetValueOrExpected<string>(StringConfig.Suffix, string.Empty);
            if (!string.IsNullOrEmpty(prefix))
            {
                randomString = $"{prefix}{randomString}";
            }
            if (!string.IsNullOrEmpty(suffix))
            {
                randomString = $"{randomString}{suffix}";
            }

            return randomString;
        }

        public static string RandomInt(ref Column column)
        {
            int min = column.Config.GetValueOrExpected<int>(IntConfig.Min, 1);
            bool increment = column.Config.GetValueOrExpected<bool>(IntConfig.Increment, false);
            int value = 0;
            if(increment)
            {
                var interval = column.Config.GetValueOrExpected<int>(IntConfig.Interval, 1);
                value = min;
                min = min + interval;
                column.Config[IntConfig.Min] = min;
            }
            else
            {
                int max = column.Config.GetValueOrExpected<int>(IntConfig.Max, 1001);
                if(min > max)
                {
                    max = min + 1000;
                }
                value = random.Next(min, max);
            }
            
            var prefix = column.Config.GetValueOrExpected<string>(IntConfig.Prefix, string.Empty);
            var suffix = column.Config.GetValueOrExpected<string>(IntConfig.Suffix, string.Empty);
            var number = $"{value}";
            if (!string.IsNullOrEmpty(prefix))
            {
                number = $"{prefix}{value}";
            }
            if (!string.IsNullOrEmpty(suffix))
            {
                number = $"{number}{suffix}";
            }

            return number;
        }

        public static string RandomDate(Dictionary<string, object> config)
        {
            string format = config.GetValueOrExpected<string>(DateConfig.Format, DateConfig.DefaultFormat);
            return DateTime.UtcNow.ToString(format);
        }

        public static bool RandomBool(Dictionary<string, object> config)
        {
            var defaultBoolean = new bool[] { true, false };
            var index = random.Next(0, 2);
            return defaultBoolean[index];
        }

        public static object RandomDefault(Dictionary<string, object> config)
        {
            var containsDefaultValue = config.ContainsKey(DefaultConfig.DefaultValue);
            if(containsDefaultValue)
            {
                return config[DefaultConfig.DefaultValue];
            }
            else
            {
                throw new ArgumentNullException(DefaultConfig.DefaultValue);
            }
        }

        public static string RandomEmail(Dictionary<string, object> config)
        {
            return $"{GenerateString(5)}@{GenerateString(5)}.{GenerateString(3)}".ToLower();
        }

        public static string RandomDecimal(Dictionary<string, object> config)
        {
            string format = "{0:F" + config.GetValueOrExpected<int>(DecimalConfig.DecimalPart, DecimalConfig.DefaultDecimalPart) + "}";
            int min = config.GetValueOrExpected<int>(IntConfig.Min, 1);
            int max = config.GetValueOrExpected<int>(IntConfig.Min, 1000);
            var value = random.NextDouble() * (max - min) + min;
            return String.Format(format, value);
        }

        public static object RandomValueFromList(Dictionary<string, object> config)
        {
            if(config.Count == 0 || !config.ContainsKey(ListConfig.Items))
            {
                throw new ArgumentNullException(ListConfig.Items);
            }
            else
            {
                dynamic items;
                int itemsCount = 0;
                if(config[ListConfig.Items] is object[])
                {
                    items = (object[])config[ListConfig.Items];
                    itemsCount = items.Length;
                }
                else if(config[ListConfig.Items] is JArray)
                {
                    items = (JArray)config[ListConfig.Items];
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
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 97)));
                builder.Append(ch);
            }

            var randomString = builder.ToString();
            return randomString;
        }

        private static string StringCaseFormatter(StringCase stringCase, string value, TextInfo textInfo, bool isFirst)
        {
            var formattedText = value;
            switch (stringCase)
            {
                case StringCase.Upper:
                    formattedText = textInfo.ToUpper(value);
                    break;
                case StringCase.Lower:
                    formattedText = textInfo.ToLower(value);
                    break;
                case StringCase.Title:
                    formattedText = textInfo.ToTitleCase(value);
                    break;
                case StringCase.Sentence:
                    formattedText = (isFirst ? textInfo.ToTitleCase(value) : textInfo.ToLower(value));
                    break;
                case StringCase.Camel:
                    formattedText = (isFirst ? textInfo.ToLower(value) : textInfo.ToTitleCase(value));
                    break;
                case StringCase.Pascal:
                    formattedText = textInfo.ToTitleCase(value);
                    break;
                case StringCase.Snake:
                case StringCase.Kebab:
                    formattedText = textInfo.ToLower(value);
                    break;
                default:
                    throw new InvalidOperationException($"Case of type {stringCase} is not valid");
            }

            return formattedText;
        }
    }
}
