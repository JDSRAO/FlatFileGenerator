// <copyright file="ColumnTypeStringConfiguration.cs" company="KJDS Srinivasa Rao">
// Copyright (c) KJDS Srinivasa Rao. All rights reserved.
// </copyright>

namespace FlatFileGenerator.Core.Models
{
    /// <summary>
    /// Describes <see cref="ColumnType.StringType"/> column configuration.
    /// </summary>
    internal static class ColumnTypeStringConfiguration
    {
        /// <summary>
        /// String length.
        /// </summary>
        public const string Length = "length";

        /// <summary>
        /// String prefix.
        /// </summary>
        public const string Prefix = "prefix";

        /// <summary>
        /// String suffix.
        /// </summary>
        public const string Suffix = "suffix";

        /// <summary>
        /// String case supported, for cases <see cref="ColumnTypeStringCaseConfiguration"/> .
        /// </summary>
        public const string Case = "case";
    }
}
