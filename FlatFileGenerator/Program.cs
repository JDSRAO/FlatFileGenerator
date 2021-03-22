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
            Console.WriteLine("-- Welcome to flat file generator --");
            Console.WriteLine("-- Reading configuration --");
            try
            {
                Console.WriteLine("-- Generating flat file --");
                var flatFileName = GetCurrentConfiguration().WriteFlatFileToDisk();
                Console.WriteLine($"-- Flat file is generated at {flatFileName} --");
            }
            catch (Exception ex)
            {
                Console.WriteLine("-- Error occurred while generating flat file --");
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Completed. Please press enter to exit");
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
