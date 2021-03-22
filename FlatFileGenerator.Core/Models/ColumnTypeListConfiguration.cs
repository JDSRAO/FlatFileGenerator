// <copyright file="ColumnTypeListConfiguration.cs" company="KJDS Srinivasa Rao">
// Copyright (c) KJDS Srinivasa Rao. All rights reserved.
// </copyright>

namespace FlatFileGenerator.Core.Models
{
    /// <summary>
    /// Describes <see cref="ColumnType.ListType"/> column configuration.
    /// </summary>
    internal class ColumnTypeListConfiguration
    {
        /// <summary>
        /// An array of objects from which a item has to be picked randomly. This is a mandatory field.
        /// </summary>
        public const string Items = "items";
    }
}
