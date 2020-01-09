using FlatFileGenerator.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlatFileGenerator.Models
{
    internal class RandomGenerator
    {
        private static Random random { get; } = new Random();

        public static dynamic Value<T>(Dictionary<string, string> config)
        {
            var currentType = typeof(T);
            dynamic value;
            if (currentType == typeof(string))
            {
                value = RandomString(config);
            }
            else if(currentType == typeof(int))
            {
                value = RandomInt(config);
            }
            else
            {
                throw new InvalidOperationException($"Type of {currentType} is invalid");
            }
            return value;
        }

        private static string RandomString(Dictionary<string, string> config)
        {
            int size = config.GetValueAndCompare<int>(StringConfig.Length, 5);
            bool lowerCase = config.GetValueAndCompare<bool>(StringConfig.Length, true);

            var builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            var randomString = builder.ToString();
            var prefix = config.GetValueOrDefault(StringConfig.Prefix);
            var suffix = config.GetValueOrDefault(StringConfig.Suffix);
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

        private static int RandomInt(Dictionary<string, string> config)
        {
            int min = config.GetValueAndCompare<int>(IntConfig.Min, 1);
            int max = config.GetValueAndCompare<int>(IntConfig.Min, 1000);
            if(min > max)
            {
                max = min + 1000;
            }
            return random.Next(min, max);
        }
    }
}
