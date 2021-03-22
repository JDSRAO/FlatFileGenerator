// <copyright file="Configuration.cs" company="KJDS Srinivasa Rao">
// Copyright (c) KJDS Srinivasa Rao. All rights reserved.
// </copyright>

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
    /// Describes configuration file.
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// Gets or sets File name.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to shows row number in the generated file.
        /// </summary>
        public bool ShowRowNumber { get; set; }

        /// <summary>
        /// Gets or sets Seperator to be used by generator.
        /// </summary>
        public string Seperator { get; set; }

        /// <summary>
        /// Gets or sets number of rows to generate.
        /// </summary>
        public long Rows { get; set; }

        /// <summary>
        /// Gets or sets Columns definitions.
        /// </summary>
        public List<Column> Columns { get; set; }

        /// <summary>
        /// Generate flat file content based on the configuration.
        /// </summary>
        /// <param name="config">File configuration.</param>
        /// <returns>Flat file content.</returns>
        public static string GenerateFlatFile(Configuration config)
        {
            return GenerateFlatFileContent(config);
        }

        /// <summary>
        /// Generate and write flat file to the <paramref name="path"/> provided based on the <paramref name="config"/>.
        /// </summary>
        /// <param name="config">Flat file configuration.</param>
        /// <param name="path">Path to generate file.</param>
        /// <returns>Path where the file is written.</returns>
        public static string WriteFlatFileToDisk(Configuration config, string path = null)
        {
            string flatFilePath = GenerateFilePath(config.FileName, path);
            string flatFileContent = GenerateFlatFileContent(config);

            File.WriteAllText(flatFilePath, flatFileContent);
            return flatFilePath;
        }

        /// <summary>
        /// Generate and write flat file to the <paramref name="path"/> provided based on the current configuration.
        /// </summary>
        /// <param name="path">Path to generate file.</param>
        /// <returns>Path where the file is written.</returns>
        public string WriteFlatFileToDisk(string path = null)
        {
            string flatFilePath = GenerateFilePath(this.FileName, path);
            string flatFileContent = GenerateFlatFileContent(this);

            File.WriteAllText(flatFilePath, flatFileContent);
            return flatFilePath;
        }

        /// <summary>
        /// Generate flat file content based on the configuration.
        /// </summary>
        /// <returns>Flat file content.</returns>
        public string GenerateFlatFile()
        {
            return GenerateFlatFileContent(this);
        }

        /// <summary>
        /// Generates flat file path.
        /// </summary>
        /// <param name="fileName">File name.</param>
        /// <param name="path">Directory location.</param>
        /// <returns>File path.</returns>
        private static string GenerateFilePath(string fileName, string path)
        {
            string flatFilePath;
            if (string.IsNullOrEmpty(path))
            {
                var flatFileName = $"{DateTime.UtcNow:yyyyMMddHHmmssffff}_{fileName}";
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

            return flatFilePath;
        }

        /// <summary>
        /// Generates file content.
        /// </summary>
        /// <param name="config">File configuration.</param>
        /// <returns>File content.</returns>
        private static string GenerateFlatFileContent(Configuration config)
        {
            if (string.IsNullOrEmpty(config.Seperator) && config.FileName.EndsWith(".csv"))
            {
                config.Seperator = ",";
            }
            else if (string.IsNullOrEmpty(config.Seperator) && config.FileName.EndsWith(".tsv"))
            {
                config.Seperator = "    ";
            }
            else if (string.IsNullOrEmpty(config.Seperator))
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
                    if (config.FileName.EndsWith(".csv"))
                    {
                        dynamic value = column.GetColumnValue();
                        if (value is string && ((string)value).Contains(","))
                        {
                            value = "\"" + value + "\"";
                        }

                        fileContent.Append(value + config.Seperator);
                    }
                    else
                    {
                        fileContent.Append(column.GetColumnValue() + config.Seperator);
                    }
                }

                fileContent.Remove(fileContent.Length - 1, 1);
                fileContent.Append("\n");
            }

            var flatFileContent = fileContent.ToString().Trim();
            return flatFileContent;
        }
    }
}
