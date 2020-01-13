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
}
