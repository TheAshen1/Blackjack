using System;
using System.IO;

namespace BlackJack.Utility.Loggers
{
    public static class Logger
    {
        public static void WriteLogToFile(string message, string className, string methodName = "not specified")
        {
            try
            {
                var pathFile = AppDomain.CurrentDomain.BaseDirectory + "logFile.txt";

                if (!File.Exists(pathFile))
                {
                    using (var writer = File.CreateText(pathFile))
                    {
                        writer.WriteLine(DateTime.Now + " " + message + "; class: " + className + "; method: " + methodName + "</br>");
                        return;
                    }
                }

                using (var writer = File.AppendText(pathFile))
                {
                    writer.WriteLine(DateTime.Now + " " + message + "; class: " + className + "; method: " + methodName + "</br>");
                }
            }
            catch
            {
                WriteLogToFile(message, className, methodName);
            }
        }
    }
}
