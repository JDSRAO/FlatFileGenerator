using FlatFileGenerator.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlatFileGenerator.Core.Models
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
}
