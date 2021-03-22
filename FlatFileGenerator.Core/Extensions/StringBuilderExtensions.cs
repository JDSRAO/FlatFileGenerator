// <copyright file="StringBuilderExtensions.cs" company="KJDS Srinivasa Rao">
// Copyright (c) KJDS Srinivasa Rao. All rights reserved.
// </copyright>

using System;
using System.Text;

namespace FlatFileGenerator.Core.Extensions
{
    /// <summary>
    /// Contains extension methods for <see cref="StringBuilder"/>.
    /// </summary>
    public static class StringBuilderExtensions
    {
        /// <summary>
        /// Prepends a value to the beginning of the string builder.
        /// </summary>
        /// <param name="text">String Builder instance.</param>
        /// <param name="content">Content to prepent.</param>
        /// <returns>Value after prependeding.</returns>
        public static StringBuilder Prepend(this StringBuilder text, string content)
        {
            return text.Insert(0, content);
        }

        /// <summary>
        /// Prepends a line to the beginning of the string builder.
        /// </summary>
        /// <param name="text">String Builder instance.</param>
        /// <param name="content">Line Content to prepent.</param>
        /// <returns>Value after prependeding.</returns>
        public static StringBuilder PrependLine(this StringBuilder text, string content)
        {
            text.Prepend(content);
            return text.Append(Environment.NewLine);
        }
    }
}
