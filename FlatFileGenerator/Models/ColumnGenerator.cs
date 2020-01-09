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
                    break;
                case ColumnType.IntergerType:
                    columnValue = RandomGenerator.Value<int>(column.Config);
                    break;
                case ColumnType.DecimalType:
                    break;
                case ColumnType.FloatType:
                    break;
                default:
                    throw new InvalidOperationException($"Column of type {columnType} is not valid");
            }

            return columnValue;
        }

        public static string GenerateFlatFile(Configuration config)
        {
            var fileContent = new StringBuilder();
            fileContent.AppendLine(string.Join(',', config.Columns.Select(x => x.Name).ToArray()));
            for (int i = 1; i <= config.Rows; i++)
            {
                foreach (var column in config.Columns)
                {
                    fileContent.Append(GetColumnValue(column) + ",");
                }
                fileContent.Remove(fileContent.Length - 1, 1);
                fileContent.Append("\n");
            }

            return fileContent.ToString().Trim();
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
        FloatType,
        [Display(Name = "bool")]
        BooleanType
    }
}
