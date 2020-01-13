using FlatFileGenerator.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace FlatFileGenerator.Models
{
    internal class ColumnGenerator
    {
        public static dynamic GetColumnValue(Column column)
        {
            var columnType = EnumExtensions<ColumnType>.ParseDisplayName(column.Type);
            dynamic columnValue = null;
            switch (columnType)
            {
                case ColumnType.StringType:
                    columnValue = RandomGenerator.Value<string>(column.Config);
                    break;
                case ColumnType.DateType:
                    columnValue = RandomGenerator.Value<DateTime>(column.Config);
                    break;
                case ColumnType.IntergerType:
                    columnValue = RandomGenerator.Value<int>(column.Config);
                    break;
                case ColumnType.DecimalType:
                    break;
                case ColumnType.FloatType:
                    break;
                case ColumnType.EmailType:
                    break;
                case ColumnType.DefaultType:
                    var defaultValue = column.Config.GetValueOrDefault(DefaultConfig.DefaultValue);
                    if(string.IsNullOrEmpty(defaultValue))
                    {
                        throw new ArgumentNullException(DefaultConfig.DefaultValue + "should be specified");
                    }
                    else
                    {
                        columnValue = defaultValue;
                    }
                    break;
                default:
                    throw new InvalidOperationException($"Column of type {columnType} is not valid");
            }

            return columnValue;
        }
    }

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
        EmailType
    }
}
