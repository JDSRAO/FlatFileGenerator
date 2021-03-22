// <copyright file="ColumnTypeDefaultConfiguration.cs" company="KJDS Srinivasa Rao">
// Copyright (c) KJDS Srinivasa Rao. All rights reserved.
// </copyright>

namespace FlatFileGenerator.Core.Models
{
    /// <summary>
    /// Describes <see cref="ColumnType.DefaultType"/> column configuration.
    /// </summary>
    internal static class ColumnTypeDefaultConfiguration
    {
        /// <summary>
        /// Default value to be populated for all the rows. This is a mandatory field.
        /// </summary>
        public const string DefaultValue = "defaultValue";
    }
}
