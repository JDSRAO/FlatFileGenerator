using System;
using System.Collections.Generic;
using System.Text;

namespace FlatFileGenerator.Models
{
    internal class RandomGenerator
    {
        public static dynamic Value(Column column)
        {
            return null;
        }

        private string RandomString(int size, bool lowerCase)
        {
            var random = new Random();
            var builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        private int RandomInt(int min, int max)
        {
            var random = new Random();
            return random.Next(min, max);
        }
    }
}
