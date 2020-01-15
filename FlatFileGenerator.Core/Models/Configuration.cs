using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatFileGenerator.Core.Models
{
    public class Configuration
    {
        public string FileName { get; set; }
        public bool ShowRowNumber { get; set; }
        public string Seperator { get; set; }
        public long Rows { get; set; }
        public List<Column> Columns { get; set; }

        public static string GenerateFlatFile(Configuration config)
        {   
            return GenerateFlatFileContent(config);
        }

        public static string WriteFlatFileToDisk(Configuration config, string path = null)
        {
            string flatFilePath = null;
            if (string.IsNullOrEmpty(path))
            {
                var flatFileName = $"{DateTime.UtcNow.ToString("yyyyMMddHHmmssffff")}_{config.FileName}";
                var baseFilePath = Path.Combine(Directory.GetCurrentDirectory(), "files");
                flatFilePath = Path.Combine(baseFilePath, flatFileName);
                if (!Directory.Exists(baseFilePath))
                {
                    Directory.CreateDirectory(baseFilePath);
                }
            }
            else
            {
                flatFilePath = path;
            }

            string flatFileContent = GenerateFlatFileContent(config);

            File.WriteAllText(flatFilePath, flatFileContent);
            return flatFilePath;
        }

        private static string GenerateFlatFileContent(Configuration config)
        {
            var fileContent = new StringBuilder();
            if (config.ShowRowNumber)
            {
                fileContent.Append("rowNumber,");
            }
            fileContent.Append(string.Join(",", config.Columns.Select(x => x.Name).ToArray()) + "\n");
            for (int i = 1; i <= config.Rows; i++)
            {
                if (config.ShowRowNumber)
                {
                    fileContent.Append(i + ",");
                }
                foreach (var column in config.Columns)
                {
                    fileContent.Append(ColumnGenerator.GetColumnValue(column) + ",");
                }
                fileContent.Remove(fileContent.Length - 1, 1);
                fileContent.Append("\n");
            }

            var flatFileContent = fileContent.ToString().Trim();
            return flatFileContent;
        }
    }
}
