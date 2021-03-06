﻿// <copyright file="ColumnTypeDateConfiguration.cs" company="KJDS Srinivasa Rao">
// Copyright (c) KJDS Srinivasa Rao. All rights reserved.
// </copyright>

namespace FlatFileGenerator.Core.Models
{
    /// <summary>
    /// Describes <see cref="ColumnType.DateType"/> column configuration.
    /// </summary>
    internal static class ColumnTypeDateConfiguration
    {
        /// <summary>
        /// Format of the date to be formed. By default this is set to <see cref="ColumnTypeDateConfiguration.DefaultFormat"/>.
        /// </summary>
        public const string Format = "format";

        /// <summary>
        /// Default date format.
        /// </summary>
        public const string DefaultFormat = "yyyyMMddHHmmssffff";

        /// <summary>
        /// Minimum value of year from which the random date must be generated. By default this is set to 1.
        /// </summary>
        public const string MinYear = "minYear";

        /// <summary>
        /// Maximum value of year from which the random date must be generated. By default this is set to current year.
        /// </summary>
        public const string MaxYear = "maxYear";
    }
}
