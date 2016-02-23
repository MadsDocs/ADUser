using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.DirectoryServices.ActiveDirectory;
using System.Net.NetworkInformation;

namespace ADUser.Classes
{
    class misc
    {
        public static void ShowStatus (bool stats)
        {
            if (stats)
            {
                Console.WriteLine("Programm wurde erfolgreich gestartet / installiert");
            }
            else
            {
                Console.WriteLine("Programm wurde nicht erfolgreich gestartet / installiert");
            }
        }

        public static string getAppSetting(string key)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string setting = config.AppSettings.Settings["connectionstring"].Value;

            if (setting == string.Empty)
            {
                Console.WriteLine("Key [" + key + "] wurde nicht gefunden!");
                Console.ReadLine();

                logger._ilogger("Key [" + key + "] wurde nicht gefunden!");
                Environment.Exit(-1);
            }


            return config.AppSettings.Settings["connectionstring"].Value;
        }

        public static string getDomainSetting (string key)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string setting = config.AppSettings.Settings["domain"].Value;

            if (setting == string.Empty)
            {
                Console.WriteLine("Key [" + key + "] wurde nicht gefunden!");
                Console.ReadLine();

                logger._ilogger("Key [" + key + "] wurde nicht gefunden!");
                Environment.Exit(-1);
            }


            return config.AppSettings.Settings["domain"].Value;
        }

        public static bool isValid (string connectionstring)
        {
            if (string.IsNullOrEmpty(connectionstring))
            {
                Console.WriteLine("Bitte Connectionstring ändern!");
                return variablen.isValid;
            }

            variablen.isValid = true;
            return variablen.isValid;
        }

        public static void isValidDomain ()
        {
            try
            {
                Ping pingsender = new Ping();
                PingOptions options = new PingOptions(128, true);
                string data = "aaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                PingReply reply = pingsender.Send(Classes.variablen.domain);

                if (reply.Status == IPStatus.Success)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Domain wurde gefunden!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else if (reply.Status == IPStatus.TimedOut)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Domain wurde nicht gefunden!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
            catch (Exception ex)
            {
                Classes.logger._elogger(ex);
            }
          
            
        }

        public static void ShowDomain()
        {
            Console.WriteLine(Environment.UserDomainName);
            Console.ReadLine();
        }
    }
}
