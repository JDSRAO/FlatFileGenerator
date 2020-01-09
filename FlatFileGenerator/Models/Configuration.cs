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
            var configJson = await File.ReadAllTextAsync(fileNameWithPath);
            return JsonConvert.DeserializeObject<Configuration>(configJson);
        }
    }

    internal class Column
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public ColumnConfig Config { get; set; }
    }

    internal class ColumnConfig
    {
        public int Length { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public string Format { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public string DefaultValue { get; set; }
    }
}
