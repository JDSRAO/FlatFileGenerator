using FlatFileGenerator.Models;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace FlatFileGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-- Welcome to flat file generator --");
            Console.WriteLine("-- Reading configuration --");
            try
            {
                var config = Configuration.GetCurrentConfiguration().Result;
                var flatFileName = $"{DateTime.UtcNow.ToString("yyyyMMddHHmmssffff")}_{config.FileName}";
                var baseFilePath = Path.Combine(Directory.GetCurrentDirectory(), "files");
                var flatFilePath = Path.Combine(baseFilePath, flatFileName);
                if (!Directory.Exists(baseFilePath))
                {
                    Directory.CreateDirectory(baseFilePath);
                }

                Console.WriteLine("-- Generating flat file --");
                GenerateFlatFile(config, flatFilePath);
                Console.WriteLine($"-- Flat file with name {flatFileName} generated--");
            }
            catch (Exception ex)
            {
                Console.WriteLine("-- Error occurred while generating flat file --");
            }

            Console.WriteLine("Completed. Please press enter to exit");
            Console.ReadLine();
        }

        private static void GenerateFlatFile(Configuration config, string flatFilePath)
        {
            var fileContent = new StringBuilder();
            fileContent.AppendLine(string.Join(',', config.Columns.Select(x => x.Name).ToArray()));
            for (int i = 1; i <= config.Rows; i++)
            {
                foreach (var column in config.Columns)
                {
                    fileContent.Append(ColumnGenerator.GetColumnValue(column) + ",");
                }
                fileContent.Remove(fileContent.Length - 1, 1);
                fileContent.Append("\n");
            }

            var flatFileContent = fileContent.ToString().Trim();

            
            File.WriteAllText(flatFilePath, flatFileContent);
        }
    }
}
