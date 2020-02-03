using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatFileGenerator.Core.Models
{
    /// <summary>
    /// Flat file configuration
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// File name
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Show row number, if set to true shows row number in the generated file
        /// </summary>
        public bool ShowRowNumber { get; set; }

        /// <summary>
        /// Seperator to be used by default this is set to ","
        /// </summary>
        public string Seperator { get; set; }

        /// <summary>
        /// Number of rows to generate
        /// </summary>
        public long Rows { get; set; }

        /// <summary>
        /// Columns with their definition
        /// </summary>
        public List<Column> Columns { get; set; }

        /// <summary>
        /// Generate flat file content based on the configuration
        /// </summary>
        /// <param name="config">File config</param>
        /// <returns>Flat file content</returns>
        public static string GenerateFlatFile(Configuration config)
        {   
            return GenerateFlatFileContent(config);
        }

        /// <summary>
        /// Generate and write flat file to disk based on the configuration and path provided
        /// </summary>
        /// <param name="config">Flat file config</param>
        /// <param name="path">Path to write</param>
        /// <returns>Path where the file is written</returns>
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
            if(string.IsNullOrEmpty(config.Seperator) && config.FileName.EndsWith(".csv"))
            {
                config.Seperator = ",";
            }
            else if (string.IsNullOrEmpty(config.Seperator) && config.FileName.EndsWith(".tsv"))
            {
                config.Seperator = "    ";
            }
            else if(string.IsNullOrEmpty(config.Seperator))
            {
                throw new ArgumentNullException("Seperator", "Seperator is either missing or empty");
            }

            var fileContent = new StringBuilder();
            if (config.ShowRowNumber)
            {
                fileContent.Append("rowNumber" + config.Seperator);
            }
            fileContent.Append(string.Join(config.Seperator, config.Columns.Select(x => x.Name).ToArray()) + "\n");
            for (int i = 1; i <= config.Rows; i++)
            {
                if (config.ShowRowNumber)
                {
                    fileContent.Append(i + config.Seperator);
                }
                foreach (var column in config.Columns)
                {
                    fileContent.Append(column.GetColumnValue() + config.Seperator);
                }
                fileContent.Remove(fileContent.Length - 1, 1);
                fileContent.Append("\n");
            }

            var flatFileContent = fileContent.ToString().Trim();
            return flatFileContent;
        }
    }
}
