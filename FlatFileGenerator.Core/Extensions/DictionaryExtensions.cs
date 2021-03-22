// <copyright file="DictionaryExtensions.cs" company="KJDS Srinivasa Rao">
// Copyright (c) KJDS Srinivasa Rao. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;

namespace FlatFileGenerator.Core.Extensions
{
    /// <summary>
    /// Contains extension methods for <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"/>.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Gets the a non null value from the dictionary else returns <paramref name="expectedValue"/>.
        /// </summary>
        /// <typeparam name="T">The type of the value that is expected in return.</typeparam>
        /// <param name="dict">The source dictionary.</param>
        /// <param name="key">Key to search.</param>
        /// <param name="expectedValue">The value to return in case the dictionary contains a null value for the specified key.</param>
        /// <returns>Exisitng value or expected value.</returns>
        public static T GetValueOrExpected<T>(this Dictionary<string, object> dict, string key, object expectedValue)
        {
            T value;
            dict.TryGetValue(key, out object dictValue);
            if (dictValue == null)
            {
                value = (T)expectedValue;
            }
            else
            {
                value = (T)Convert.ChangeType(dictValue, typeof(T));
            }

            return value;
        }

        /// <summary>
        /// Gets the a non null value from the dictionary else executes and returns the value returned by <paramref name="defaultValueProvider"/>.
        /// </summary>
        /// <typeparam name="TKey">Dictionary key type.</typeparam>
        /// <typeparam name="TValue">Dictionary value type.</typeparam>
        /// <param name="dictionary">The source dictionary.</param>
        /// <param name="key">Key to search.</param>
        /// <param name="defaultValueProvider">A function to return the default value.</param>
        /// <returns>Value or default value.</returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> defaultValueProvider)
        {
            return dictionary.TryGetValue(key, out TValue value) ? value
                 : defaultValueProvider();
        }
    }
}
