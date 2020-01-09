using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FlatFileGenerator.Models
{
    internal class Configuration
    {
        public string FileName { get; set; }
        public string Seperator { get; set; }
        public int Rows { get; set; }
        public List<Column> Columns { get; set; }

        public static async Task<Configuration> GetCurrentConfiguration()
        {
            var fileName = "config.json";
            var fileNameWithPath = Path.Join(Directory.GetCurrentDirectory(), fileName);
            if(!File.Exists(fileNameWithPath))
            {
                throw new FileNotFoundException("Configuration file not found", fileName);
            }
            var configJson = await File.ReadAllTextAsync(fileNameWithPath);
            return JsonConvert.DeserializeObject<Configuration>(configJson);
        }
    }

    internal class Column
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public Dictionary<string, string>  Config { get; set; }

        public Column()
        {
            Config = new Dictionary<string, string>();
        }
    }

    internal class ColumnConfig
    {
        public int Length { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public string Format { get; set; }
        public bool LowerCase { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public string DefaultValue { get; set; }
    }

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
    }
    internal class DefaultConfig
    {
        public const string DefaultValue = "defaultValue";
    }
}
