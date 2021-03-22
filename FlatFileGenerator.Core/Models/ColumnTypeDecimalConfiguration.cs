// <copyright file="ColumnTypeDecimalConfiguration.cs" company="KJDS Srinivasa Rao">
// Copyright (c) KJDS Srinivasa Rao. All rights reserved.
// </copyright>

namespace FlatFileGenerator.Core.Models
{
    /// <summary>
    /// Describes <see cref="ColumnType.DefaultType"/> column configuration.
    /// </summary>
    internal static class ColumnTypeDecimalConfiguration
    {
        /// <summary>
        /// Minimum value from which the random integer must be generated. By default this is set to 1.
        /// </summary>
        public const string Min = "min";

        /// <summary>
        /// Maximum value below which the random integer must be generated. By default this is set to 1000.
        /// </summary>
        public const string Max = "max";

        /// <summary>
        /// Number of digits to be shown after the decimal. By default this is set to <see cref="ColumnTypeDecimalConfiguration.DefaultDecimalPart"/>.
        /// </summary>
        public const string DecimalPart = "decimalPart";

        /// <summary>
        /// Default number of digits after decimal.
        /// </summary>
        public const int DefaultDecimalPart = 2;
    }
}
