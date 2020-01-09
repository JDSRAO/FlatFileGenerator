using System;
using System.Collections.Generic;
using System.Text;

namespace FlatFileGenerator.Extensions
{
    public static class DictionaryExtensions
    {
        public static T GetValueAndCompare<T>(this Dictionary<string, string> dict, string key, object expectedValue)
        {
            T value;
            string dictValue;
            dict.TryGetValue(key, out dictValue);
            if(string.IsNullOrEmpty(dictValue))
            {
                value = (T)expectedValue;
            }
            else
            {
                value = (T)Convert.ChangeType(dictValue, typeof(T));
            }

            return value;
        }
    }
}
