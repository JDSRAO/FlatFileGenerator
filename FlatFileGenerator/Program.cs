using FlatFileGenerator.Core.Models;
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
                var flatFileName = Configuration.GenerateFlatFile();
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
