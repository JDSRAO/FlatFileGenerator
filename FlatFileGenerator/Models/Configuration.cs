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
        public int Rows { get; set; }
        public List<Columns> Columns { get; set; }

        public static async Task<Configuration> GetCurrentConfiguration()
        {
            var fileName = "config.json";
            var fileNameWithPath = Path.Join(Directory.GetCurrentDirectory(), fileName);
            var configJson = await File.ReadAllTextAsync(fileNameWithPath);
            return JsonConvert.DeserializeObject<Configuration>(configJson);
        }
    }

    internal class Columns
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
