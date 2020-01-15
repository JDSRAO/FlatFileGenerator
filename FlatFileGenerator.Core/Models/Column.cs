using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FlatFileGenerator.Core.Models
{
    public class Column
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public Dictionary<string, string> Config { get; set; }

        public Column()
        {
            Config = new Dictionary<string, string>();
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

    //internal class ColumnConfig
    //{
    //    public int Length { get; set; }
    //    public int Min { get; set; }
    //    public int Max { get; set; }
    //    public string Format { get; set; }
    //    public bool LowerCase { get; set; }
    //    public string Prefix { get; set; }
    //    public string Suffix { get; set; }
    //    public string DefaultValue { get; set; }
    //}

    internal class StringConfig
    {
        public const string Length = "length";
        public const string Prefix = "prefix";
        public const string Suffix = "suffix";
        public const string LowerCase = "lowerCase";
    }

    internal class IntConfig
    {
        public const string Min = "min";
        public const string Max = "max";
    }

    internal class DateConfig
    {
        public const string Format = "format";
        public const string DefaultFormat = "yyyyMMddHHmmssffff";
    }

    internal class DefaultConfig
    {
        public const string DefaultValue = "defaultValue";
    }
}
