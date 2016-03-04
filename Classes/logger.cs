using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;


namespace ADUser.Classes
{
    class logger
    {
        
        public static void _elogger (Exception exe)
        {
            try
            {
                StreamWriter _ewriter = new StreamWriter(variablen.appdata + @"\UserData\Logs\status.log");
                _ewriter.WriteLine(DateTime.Now + " | " + Classes.variablen.version + " | " + Classes.variablen.branch);
                _ewriter.WriteLine(DateTime.Now + " | " + exe.Message);
                _ewriter.WriteLine(DateTime.Now + " | " + exe.StackTrace);
                _ewriter.WriteLine(DateTime.Now + " | " + exe.InnerException);
                _ewriter.Flush();
                _ewriter.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            

        }

        public static void _ilogger (string message)
        {
            try
            {
                StreamWriter _iwriter = new StreamWriter(variablen.appdata + @"\UserData\Logs\status.log");
                _iwriter.WriteLine(DateTime.Now + " | " + Classes.variablen.version + " | " + Classes.variablen.branch);
                _iwriter.WriteLine(DateTime.Now + " | " + message);
                _iwriter.Flush();
                _iwriter.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        public static void _wlogger ( string message)
        {
            try
            {
                StreamWriter _wwriter = new StreamWriter(variablen.appdata + @"\UserData\Logs\status.log");
                _wwriter.WriteLine(DateTime.Now + " | " + Classes.variablen.version + " | " + Classes.variablen.branch);
                _wwriter.WriteLine(DateTime.Now + " | " + message);
                _wwriter.Flush();
                _wwriter.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        public static void _sLogger(bool status)
        {
            StreamWriter _swriter = new StreamWriter(variablen.appdata + @"\UserData\Logs\status.log");
            _swriter.WriteLine(status);
            _swriter.Close();
            _swriter.Flush();
        }

        public static void readLog()
        {
            if ( !Directory.Exists (variablen.appdata + @"\UserData\Logs\"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Log Ordner nicht gefunden!");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
                StreamReader _rreader = new StreamReader(variablen.appdata + @"\UserData\Logs\status.log");
                string line = "";

                while ((line = _rreader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }

                Console.ReadLine();

            }
        }
    }
}
