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
                    throw new InvalidOperationException($"Column of type {columnType} is not valid");
            }

            return columnValue;
        }

        public static string GenerateFlatFile(Configuration config)
        {
            var fileContent = new StringBuilder();
            fileContent.AppendLine(string.Join(',', config.Columns.Select(x => x.Name).ToArray()));
            var values = new List<dynamic>(config.Columns.Count);
            for (int i = 1; i <= config.Rows; i++)
            {
                foreach (var item in config.Columns)
                {

                }

                values.Clear();
            }

            return fileContent.ToString();
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
