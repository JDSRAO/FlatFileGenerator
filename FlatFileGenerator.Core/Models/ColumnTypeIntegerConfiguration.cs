// <copyright file="ColumnTypeIntegerConfiguration.cs" company="KJDS Srinivasa Rao">
// Copyright (c) KJDS Srinivasa Rao. All rights reserved.
// </copyright>

namespace FlatFileGenerator.Core.Models
{
    /// <summary>
    /// Describes <see cref="ColumnType.IntergerType"/> column configuration.
    /// </summary>
    internal static class ColumnTypeIntegerConfiguration
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
        /// Prefix to be added to the generated integer. This is optional.
        /// </summary>
        public const string Prefix = "prefix";

        /// <summary>
        /// Suffix to be added to the generated integer. This is optional.
        /// </summary>
        public const string Suffix = "suffix";

        /// <summary>
        /// A boolean value indicating true/false. When this is set to true all the rows in the generated file will have integers sequentially incremented starting from the value set in min.
        /// </summary>
        public const string Increment = "increment";

        /// <summary>
        /// The incremental value to be used in case when <see cref="ColumnTypeIntegerConfiguration.Increment"/> property is set to true. By default, this is set to 1 hence this is a non mandatory field.
        /// </summary>
        public const string Interval = "interval";
    }
}
