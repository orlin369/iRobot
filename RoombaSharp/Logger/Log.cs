/*

Copyright (c) [2016] [Orlin Dimitrov]

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Logger.Events;

namespace Logger
{
    public static class Log
    {

        #region Variables

        /// <summary>
        /// Specify a name for your applicatin folder.
        /// </summary>
        private static string logFolderPath = @"C:\";

        /// <summary>
        /// List of messages that must be write to a file.
        /// </summary>
        private static List<String> logMessages = new List<String>();

        /// <summary>
        /// Maximum messages count in the message list.
        /// </summary>
        private static int colectionSize = 1;

        /// <summary>
        /// Enable loging.
        /// </summary>
        public static bool Enable = true;

        #endregion

        #region Events

        public static event EventHandler<StringEventArgs> OnLoggedMessage;

        #endregion

        #region Public Methods

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="logFolderPath">Path for the application logs.</param>
        public static void SetLogPath(string logFolderPath)
        {
            // The folder for the roaming current user. 
            Log.logFolderPath = logFolderPath;
        }

        /// <summary>
        /// Set colection message size.
        /// </summary>
        /// <param name="colectionSize">Size of messages to be writen after this count.</param>
        public static void SetColectionSize(int colectionSize)
        {
            Log.colectionSize = colectionSize;
        }

        /// <summary>
        /// This method will create automaticly.
        /// Log file in folder with staic path.
        /// Every new day will be create a one new file.
        /// </summary>
        /// <param name="source">Who send this message log.</param>
        /// <param name="message">Concreet message.</param>
        public static void CreateRecord(string source, string message, LogMessageTypes type, bool end = false)
        {
            // Write LOG record to the message buffer if is enabled.
            if (Log.Enable)
            {
                // Structre of the message.
                // LogSource\tYear.Month.Day/Hour:Minute:Seconds.Miliseconds\tType\tMessageText
                string dateAndTime = DateTime.Now.ToString("yyyy.MM.dd/HH:mm:ss.fff", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                string fullMessage = source + "\t" + dateAndTime + "\t" + type.ToString() + "\t" + message;

                // Add message to the message buffer.
                Log.logMessages.Add(fullMessage);
                
                // Write end of log line
                if (end)
                {
                    Log.logMessages.Add("\r\n===================================================================================\r\n");
                }

                // If filr are critical count, just write it to a file.
                if ((Log.logMessages.Count > Log.colectionSize) || end)
                {
                    Log.WtriteToLogFile();
                }

                // Log the message.
                Log.OnLoggedMessage?.Invoke(null, new StringEventArgs(message));
            }
        }

        #endregion

        #region Private

        /// <summary>
        /// Write messages to LOG file.
        /// </summary>
        private static void WtriteToLogFile()
        {
            // Write buffer to the file if is enabled.
            if (Enable)
            {
                // Create Log file name.
                // Structure of file name.
                // Log_DateAndTime.txt
                string dateAndTime = DateTime.Now.ToString("yyyy.MM.dd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                string fileName = "Log_" + dateAndTime + ".txt";

                // Combine the base AppData folder with your specific folder (AppFolderName).

                // Check if folder exists and if not, create it.
                if (!Directory.Exists(Log.logFolderPath))
                {
                    Directory.CreateDirectory(Log.logFolderPath);
                }

                // Generate full path log file folder.
                string fullPath = Path.Combine(Log.logFolderPath, fileName);

                // Check if file exists and if not, create it.
                if (!System.IO.File.Exists(fullPath))
                {
                    try
                    {
                        // File writer it use for writing a LOG file.
                        // Create the file.
                        System.IO.StreamWriter theFile = new System.IO.StreamWriter(fullPath);

                        // Write header.
                        string header = "This file is automatic generated.\r\n";
                        header += String.Format("This file belongs to: \"{0}\"\r\n\r\n", Log.logFolderPath);
                        header += "LOG SOURCE\tDATE & TIME        \tTYPE\tMESSAGE\r\n";
                        theFile.WriteLine(header);

                        for (int messageCount = 0; messageCount < Log.logMessages.Count; messageCount++)
                        {

                            // Write the string to a file.
                            theFile.WriteLine(Log.logMessages[messageCount]);
                        }
                        // Close the log file.
                        theFile.Close();
                    }
                    catch (Exception exception)
                    {
                        throw new Exception("Internal exception.", exception);
                    }
                }
                else
                {
                    // Append data to file.
                    try
                    {
                        // File writer it use for writing a LOG file.
                        // Create the file.
                        StreamWriter theFile = new StreamWriter(fullPath, true);

                        for (int messageCount = 0; messageCount < Log.logMessages.Count; messageCount++)
                        {
                            // Write the string to a file.
                            theFile.WriteLine(Log.logMessages[messageCount]);
                        }

                        // Close the log file.
                        theFile.Close();

                    }
                    catch (Exception exception)
                    {
                        throw new Exception("Internal exception.", exception);
                    }
                }

                // Clear the message tail.
                Log.logMessages.Clear();
            }
        }

        #endregion

    }
}