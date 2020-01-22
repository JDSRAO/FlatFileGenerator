using FlatFileGenerator.Core.Extensions;
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
            int size = config.GetValueOrExpected<int>(StringConfig.Length, 5);
            bool lowerCase = config.GetValueOrExpected<bool>(StringConfig.Length, true);

            string randomString = GenerateString(size);
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

            if (lowerCase)
                return randomString.ToLower();
            return randomString;
        }

        public static int RandomInt(Dictionary<string, object> config)
        {
            int min = config.GetValueOrExpected<int>(IntConfig.Min, 1);
            int max = config.GetValueOrExpected<int>(IntConfig.Min, 1001);
            if(min > max)
            {
                max = min + 1000;
            }
            return random.Next(min, max);
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
            var index = random.Next(1, config.Count + 1);
            return config[index.ToString()];
        }

        private static string GenerateString(int size)
        {
            var builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            var randomString = builder.ToString();
            return randomString;
        }
    }
}
