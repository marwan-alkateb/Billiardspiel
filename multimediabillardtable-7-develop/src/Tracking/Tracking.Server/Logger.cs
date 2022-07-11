using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Tracking.Server
{
    /// <summary>
    /// Provides simple console logging functionality for the Tracking Server
    /// </summary>
    internal class Logger
    {
        private enum LogLevel
        {
            Info,
            Warn,
            Error
        }

        private static void PrintLog(LogLevel level, string message, string callerFile, int callerLine)
        {
            var timestamp = DateTime.Now.ToShortTimeString();
            var callerDescription = $"{Path.GetFileName(callerFile)}:{callerLine}";

            switch (level)
            {
                case LogLevel.Info:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case LogLevel.Warn:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogLevel.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }


            Console.WriteLine($"[{timestamp}] [{callerDescription}] [{level}]: {message}");
            Console.ResetColor();
        }

        /// <summary>
        /// Writes an information log record to the console
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="callerFile">Always set by the compiler</param>
        /// <param name="callerLine">Always set by the compiler</param>
        public static void Info(string message, [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
        {
            PrintLog(LogLevel.Info, message, callerFile, callerLine);
        }

        /// <summary>
        /// Writes a warning log record to the console
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="callerFile">Always set by the compiler</param>
        /// <param name="callerLine">Always set by the compiler</param>
        public static void Warn(string message, [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
        {
            PrintLog(LogLevel.Warn, message, callerFile, callerLine);
        }

        /// <summary>
        /// Writes an error log record to the console
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="callerFile">Always set by the compiler</param>
        /// <param name="callerLine">Always set by the compiler</param>
        public static void Error(string message, [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
        {
            PrintLog(LogLevel.Error, message, callerFile, callerLine);
        }
    }
}
