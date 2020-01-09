﻿using FlatFileGenerator.Models;
using System;
using System.IO;

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
                Console.WriteLine("-- Generating flat file --");
                var flatFileContent = ColumnGenerator.GenerateFlatFile(config);
                var flatFilePath = Path.Combine(Directory.GetCurrentDirectory(), flatFileName);
                File.WriteAllText(flatFilePath, flatFileContent);
                Console.WriteLine($"-- Flat file with name {flatFileName} generated--");
            }
            catch (Exception ex)
            {
                Console.WriteLine("-- Error occurred while generating flat file --");
            }

            Console.WriteLine("Completed. Please press enter to exit");
            Console.ReadLine();
        }
    }
}
