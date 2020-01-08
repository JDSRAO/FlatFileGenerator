﻿using FlatFileGenerator.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FlatFileGenerator.Models
{
    internal class ColumnGenerator
    {
        public static dynamic GetColumnValue(Column column)
        {
            var columnType = EnumExtensions<ColumnType>.Parse(column.Type);
            dynamic columnValue = null;
            switch (columnType)
            {
                case ColumnType.StringType:
                    break;
                case ColumnType.DateType:
                    break;
                case ColumnType.IntergerType:
                    break;
                case ColumnType.DecimalType:
                    break;
                case ColumnType.FloatType:
                    break;
                default:
                    break;
            }

            return columnValue;
        }
    }

    internal enum ColumnType
    {
        [Display(Name = "string")]
        StringType,
        [Display(Name = "datetime")]
        DateType,
        [Display(Name = "int")]
        IntergerType,
        [Display(Name = "decimal")]
        DecimalType,
        [Display(Name = "float")]
        FloatType
    }
}
