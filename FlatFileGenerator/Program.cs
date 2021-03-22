// <copyright file="Program.cs" company="KJDS Srinivasa Rao">
// Copyright (c) KJDS Srinivasa Rao. All rights reserved.
// </copyright>

using FlatFileGenerator.Core.Models;
using Newtonsoft.Json;
using System;
using System.IO;

namespace FlatFileGenerator
{
    /// <summary>
    /// FlatFileGenerator program.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// FlatFileGenerator main method.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        public static void Main(string[] args)
        {
            ConsoleLogger.LogInformation("-- Welcome to flat file generator --");
            ConsoleLogger.LogInformation("-- Reading configuration --");
            try
            {
                ConsoleLogger.LogInformation("-- Generating flat file --");
                var flatFileName = GetCurrentConfiguration().WriteFlatFileToDisk();
                ConsoleLogger.LogInformation($"-- Flat file is generated at {flatFileName} --");
            }
            catch (Exception ex)
            {
                ConsoleLogger.LogError("-- Error occurred while generating flat file --", ex);
            }

            ConsoleLogger.LogInformation("Completed. Please press enter to exit");
            Console.ReadLine();
        }

        private static Configuration GetCurrentConfiguration()
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
