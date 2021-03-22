// <copyright file="ConsoleLogger.cs" company="KJDS Srinivasa Rao">
// Copyright (c) KJDS Srinivasa Rao. All rights reserved.
// </copyright>

using System;

namespace FlatFileGenerator
{
    /// <summary>
    /// Constains Console logger methods.
    /// </summary>
    public static class ConsoleLogger
    {
        /// <summary>
        /// Logs information to console.
        /// </summary>
        /// <param name="message">Message to log.</param>
        public static void LogInformation(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{DateTime.Now} : {message}");
            Console.ResetColor();
        }

        /// <summary>
        /// Logs error to console.
        /// </summary>
        /// <param name="message">Message to log.</param>
        /// <param name="ex">Exception to log.</param>
        public static void LogError(string message, Exception ex)
        {
            LogError(ex, message);
        }

        /// <summary>
        /// Logs error to console.
        /// </summary>
        /// <param name="ex">Exception to log.</param>
        public static void LogError(Exception ex)
        {
            LogError(ex, null);
        }

        private static void LogError(Exception ex, string message = null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (string.IsNullOrEmpty(message) || string.IsNullOrWhiteSpace(message))
            {
                Console.WriteLine($"{DateTime.Now} : {ex.Message}");
            }
            else
            {
                Console.WriteLine($"{DateTime.Now} : {message}");
                Console.WriteLine($"Message : {ex.Message}");
                Console.WriteLine($"StackTrace : {ex.StackTrace}");

                if (ex.InnerException != null)
                {
                    LogError(ex.InnerException);
                }
            }

            Console.ResetColor();
        }
    }
}
