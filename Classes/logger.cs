using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using System.IO;


namespace ADUser.Classes
{
    class logger
    {
        
        public static void readLog()
        {
            if ( !File.Exists(variablen.enviroment + @"\status.log"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Log Datei nicht gefunden!");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
                try
                {
                    StreamReader _rreader = new StreamReader(variablen.enviroment + @"\status.log");
                    string line = "";

                    while ((line = _rreader.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                    _rreader.Close();


                    Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);

                    Console.ReadLine();
                }
               

            }
        }

        public static void _logger(string message)
        {

            /* Stream file = File.Create("E:\\Testbereich\\status.log");
             TextWriterTraceListener textListen = new TextWriterTraceListener(file);
             Trace.Listeners.Add(textListen);
             Trace.TraceInformation(DateTime.Now + " | " + message);
             Trace.Flush();
             Trace.Close();*/

            StreamWriter _logger = new StreamWriter(variablen.appdata + @"\UserData\Logs\log.log", true, ASCIIEncoding.UTF8, 12);
            _logger.WriteLine(DateTime.Now + " | " + message);
            _logger.Flush();
            _logger.Close();
          

        }

        public static void _eLogger(Exception ex)
        {
            StreamWriter _exlogger = new StreamWriter(variablen.appdata + @"\UserData\Logs\log.log", true, ASCIIEncoding.UTF8, 12);
            _exlogger.WriteLine(DateTime.Now + " | " + ex.Message);
            _exlogger.WriteLine("----------------------------------------------------------------------------------------------");
            _exlogger.WriteLine(DateTime.Now + " | " + ex.StackTrace);
            _exlogger.WriteLine("----------------------------------------------------------------------------------------------");
            _exlogger.Flush();
            _exlogger.Close();

        }

        public static void _tlogger(string message)
        {
            Stream file = File.Create(@"E:\Testbereich\test.log");
            TextWriterTraceListener listen = new TextWriterTraceListener(file);
            Trace.Listeners.Add(listen);
            Trace.TraceInformation(message);
            Trace.TraceInformation("Dies ist eine Test Information...");

            Trace.Flush();
            Trace.Close();
        }
    }
}
