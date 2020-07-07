using System;
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
        public Dictionary<string, object> Config { get; set; }

        public Column()
        {
            Config = new Dictionary<string, object>();
        }

        /// <summary>
        /// Generate column value from current column configuration
        /// </summary>
        /// <returns></returns>
        public dynamic GetColumnValue()
        {
            return GetColumnValueFromConfiguration(new Column { Type = Type, Config = Config, Name = Name });
        }

        /// <summary>
        /// Generate column value from column configuration
        /// </summary>
        /// <param name="column">Column configuration</param>
        /// <returns></returns>
        public static dynamic GetColumnValue(Column column)
        {
            return GetColumnValueFromConfiguration(column);
        }

        /// <summary>
        /// Generate column value from configuration
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
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
                    columnValue = RandomGenerator.RandomInt(column.Config);
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
        [Display(Name = "bool")]
        BooleanType,
        [Display(Name = "default")]
        DefaultType,
        [Display(Name = "email")]
        EmailType,
        [Display(Name = "list")]
        ListType,
        [Display(Name = "guid")]
        GuidType
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
        public const string Prefix = "prefix";
        public const string Suffix = "suffix";
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

    /// <summary>
    /// Column Type == decimal config options 
    /// </summary>
    internal class DecimalConfig : IntConfig
    {
        public const string DecimalPart = "decimalPart";
        public const int DefaultDecimalPart = 2;
    }

    internal class ListConfig
    {
        public const string Items = "items";
    }
}
