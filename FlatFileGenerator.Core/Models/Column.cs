﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using FlatFileGenerator.Core.Extensions;

namespace FlatFileGenerator.Core.Models
{
    /// <summary>
    /// Column configuration
    /// </summary>
    public class Column
    {
        /// <summary>
        /// Column name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Column type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Column configuration
        /// </summary>
        public Dictionary<string, string> Config { get; set; }

        public Column()
        {
            Config = new Dictionary<string, string>();
        }

        /// <summary>
        /// Generate column value from column configuration
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public dynamic GetColumnValue()
        {
            var columnType = EnumExtensions<ColumnType>.ParseDisplayName(Type);
            dynamic columnValue = null;
            switch (columnType)
            {
                case ColumnType.StringType:
                    columnValue = RandomGenerator.RandomString(Config);
                    break;
                case ColumnType.DateType:
                    columnValue = RandomGenerator.RandomDate(Config);
                    break;
                case ColumnType.IntergerType:
                    columnValue = RandomGenerator.RandomInt(Config);
                    break;
                case ColumnType.DecimalType:
                    break;
                case ColumnType.FloatType:
                    break;
                case ColumnType.EmailType:
                    columnValue = RandomGenerator.RandomEmail(Config);
                    break;
                case ColumnType.BooleanType:
                    columnValue = RandomGenerator.RandomBool(Config);
                    break;
                case ColumnType.DefaultType:
                    columnValue = RandomGenerator.RandomDefault(Config);
                    break;
                case ColumnType.ListType:
                    break;
                default:
                    throw new InvalidOperationException($"Column of type {columnType} is not valid");
            }

            return columnValue;
        }

        /// <summary>
        /// Generate column value from column configuration
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public static dynamic GetColumnValue(Column column)
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
                    columnValue = RandomGenerator.RandomInt(column.Config);
                    break;
                case ColumnType.DecimalType:
                    break;
                case ColumnType.FloatType:
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
                    break;
                default:
                    throw new InvalidOperationException($"Column of type {columnType} is not valid");
            }

            return columnValue;
        }
    }

    /// <summary>
    /// Column Types Enum
    /// </summary>
    internal enum ColumnType
    {
        [Display(Name = "string")]
        StringType,
        [Display(Name = "date")]
        DateType,
        [Display(Name = "int")]
        IntergerType,
        [Display(Name = "decimal")]
        DecimalType,
        [Display(Name = "float")]
        FloatType,
        [Display(Name = "bool")]
        BooleanType,
        [Display(Name = "default")]
        DefaultType,
        [Display(Name = "email")]
        EmailType,
        [Display(Name = "list")]
        ListType
    }

    /// <summary>
    /// Column Type == string config options
    /// </summary>
    internal class StringConfig
    {
        public const string Length = "length";
        public const string Prefix = "prefix";
        public const string Suffix = "suffix";
        public const string LowerCase = "lowerCase";
    }

    /// <summary>
    /// Column Type == int config options
    /// </summary>
    internal class IntConfig
    {
        public const string Min = "min";
        public const string Max = "max";
    }

    /// <summary>
    /// Column Type == date config options
    /// </summary>
    internal class DateConfig
    {
        public const string Format = "format";
        public const string DefaultFormat = "yyyyMMddHHmmssffff";
    }

    /// <summary>
    /// Column Type == default config options
    /// </summary>
    internal class DefaultConfig
    {
        public const string DefaultValue = "defaultValue";
    }
}