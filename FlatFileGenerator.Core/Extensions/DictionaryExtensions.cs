using System;
using System.Collections.Generic;
using System.Text;

namespace FlatFileGenerator.Core.Extensions
{
    public static class DictionaryExtensions
    {
        public static T GetValueOrExpected<T>(this Dictionary<string, object> dict, string key, object expectedValue)
        {
            T value;
            object dictValue;
            dict.TryGetValue(key, out dictValue);
            if(dictValue == null)
            {
                value = (T)expectedValue;
            }
            else
            {
                value = (T)Convert.ChangeType(dictValue, typeof(T));
            }

            return value;
        }

        public static TValue GetValueOrDefault<TKey, TValue> (this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
        {
            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : defaultValue;
        }

        public static TValue GetValueOrDefault<TKey, TValue> (this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> defaultValueProvider)
        {
            TValue value;
            return dictionary.TryGetValue(key, out value) ? value
                 : defaultValueProvider();
        }
    }
}
