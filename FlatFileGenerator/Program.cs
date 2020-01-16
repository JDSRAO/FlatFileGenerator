using FlatFileGenerator.Core.Models;
using Newtonsoft.Json;
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
                Console.WriteLine("-- Generating flat file --");
                var flatFileName = Configuration.WriteFlatFileToDisk(GetCurrentConfiguration());
                Console.WriteLine($"-- Flat file is generated at {flatFileName} --");
            }
            catch (Exception ex)
            {
                Console.WriteLine("-- Error occurred while generating flat file --");
            }

            Console.WriteLine("Completed. Please press enter to exit");
            Console.ReadLine();
        }

        static Configuration GetCurrentConfiguration()
        {
            var fileName = "config.json";
            var fileNameWithPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            if (!File.Exists(fileNameWithPath))
            {
                throw new FileNotFoundException("Configuration file not found", fileName);
            }
            var configJson = File.ReadAllText(fileNameWithPath);
            return JsonConvert.DeserializeObject<Configuration>(configJson);
        }
    }
}
