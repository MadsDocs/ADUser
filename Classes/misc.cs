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
            //string setting = config.AppSettings.Settings["connectionstring"].Value;
            return config.AppSettings.Settings["connectionstring"].Value;
        }

        public static string getMSAPPSetting (string key)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string setting = config.AppSettings.Settings["mssconnectionstring"].Value;

            return setting;
        }

        public static string getDomainSetting (string key)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string setting = config.AppSettings.Settings["domain"].Value;

            if (setting == string.Empty)
            {
                Console.WriteLine("Key [" + key + "] wurde nicht gefunden!");
                Console.ReadLine();

                logger._logger("Key [" + key + "] wurde nicht gefunden!");
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
                Classes.logger._eLogger(ex);
            }
          
            
        }

        public static void ShowDomain()
        {
            Console.WriteLine(Environment.UserDomainName);
            Console.ReadLine();
        }

        public static void ShowHelp()
        {
            Console.WriteLine("Willkommen auf den Hilfeseiten!");
            Console.WriteLine("Folgende Befehle sind benutzbar:");
            Console.WriteLine("\r\n");
            Console.WriteLine("add -- Füllt die Datenbank mit Daten");
            Console.WriteLine("truncate -- Löscht die Daten aus der Tabelle tracking");
            Console.WriteLine("select -- Zeigt den Inhalt der Tablle tracking");
            Console.WriteLine("logs --show  --Zeigt den Inhalt der Datei 'status.log' an");
            Console.WriteLine("cls -- Löscht den Konsolen Inhalt");
            Console.WriteLine("display -- Zeigt alle User in einer Domäne an");
            Console.WriteLine("env -- Zeigt das Aktuelle Working Directory des Programms an!");
            Console.WriteLine("ver -- Zeigt die Aktuelle Programmversion an");
            Console.WriteLine("stat -- Zeit den Aktuellen Programm Status an");
            Console.WriteLine("check -- Checkt die Tabelle tracking");
            Console.WriteLine("exit -- Beendet das Programm");

        }
    }
}
