// <copyright file="Column.cs" company="KJDS Srinivasa Rao">
// Copyright (c) KJDS Srinivasa Rao. All rights reserved.
// </copyright>

using FlatFileGenerator.Core.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlatFileGenerator.Core.Models
{
    /// <summary>
    /// Column configuration.
    /// </summary>
    public class Column
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Column"/> class.
        /// </summary>
        public Column()
        {
            this.Config = new Dictionary<string, object>();
        }

        /// <summary>
        /// Gets or sets Column name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Column type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets Column configuration.
        /// </summary>
        public Dictionary<string, object> Config { get; set; }

        /// <summary>
        /// Generate column value from column configuration.
        /// </summary>
        /// <param name="column">Column configuration.</param>
        /// <returns>Value based on the column configuration.</returns>
        public static dynamic GetColumnValue(Column column)
        {
            return GetColumnValueFromConfiguration(column);
        }

        /// <summary>
        /// Generate column value for current column configuration.
        /// </summary>
        /// <returns>Value based on the column configuration.</returns>
        public dynamic GetColumnValue()
        {
            return GetColumnValueFromConfiguration(this);
        }

        /// <summary>
        /// Generate column value from configuration.
        /// </summary>
        /// <param name="column">Column configuration.</param>
        /// <returns>Value based on the column configuration.</returns>
        private static dynamic GetColumnValueFromConfiguration(Column column)
        {
            var columnType = EnumExtensions<ColumnType>.ParseDisplayName(column.Type);
            dynamic columnValue = null;
            switch (columnType)
            {
                case ColumnType.StringType:
                    columnValue = RandomGenerator.RandomString(column.Config);
                    break;
                case ColumnType.DateType:
                    columnValue = RandomGenerator.RandomDate(column.Config);
                    break;
                case ColumnType.IntergerType:
                    columnValue = RandomGenerator.RandomInt(ref column);
                    break;
                case ColumnType.DecimalType:
                    columnValue = RandomGenerator.RandomDecimal(column.Config);
                    break;
                case ColumnType.EmailType:
                    columnValue = RandomGenerator.RandomEmail(column.Config);
                    break;
                case ColumnType.BooleanType:
                    columnValue = RandomGenerator.RandomBool(column.Config);
                    break;
                case ColumnType.DefaultType:
                    columnValue = RandomGenerator.RandomDefault(column.Config);
                    break;
                case ColumnType.ListType:
                    columnValue = RandomGenerator.RandomValueFromList(column.Config);
                    break;
                case ColumnType.GuidType:
                    columnValue = RandomGenerator.RandomGuid();
                    break;
                default:
                    throw new InvalidOperationException($"Column of type {columnType} is not valid");
            }

            return columnValue;
        }
    }
}
