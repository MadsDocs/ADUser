using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace ADUser.Classes
{
    class Deployment
    {
        /*
            Diese Methode überprüft ob der MySQL Connector installiert ist. Sollte dieser
            nicht installiert sein, wird er nach installiert...

            Dies ist erforderlich da sonst das Programm abstürzt wenn die Befehle
            Truncate, Add und Select ausgeführt werden
            
            Sollte der Connector nicht vorhanden sein, wird dieser
            von einer Freigabe runter geladen und installiert.

            Danach soll und wird wieder überprüft ob der Connector
            richtig installiert wurde. 

        */
        public static void ChecIfMySqlConnector()
        {
            try
            {
               if (!Directory.Exists(@"C:\Program Files (x86)\MySQL"))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Es wurde kein MySQL Connector gefunden! Es wird nun versucht das Programm nach zu installieren.");
                    logger._wlogger("Kein MySQL Connector gefunden... Aborting...");
                    Environment.Exit(-1);
                }
               else
                {
                    Console.WriteLine("MySQL Connector wurde gefunden");
                    Classes.variablen.is_connector = true;
                    logger._ilogger("MySQL Connector gefunden...");
                    logger._ilogger(variablen.is_connector.ToString());
                }
            }
            catch (Exception ex)
            {
                Classes.logger._elogger(ex);
                Console.WriteLine("Fehler bei Installation des MySQL Connectors... Aborting");
                Console.ReadLine();
                Environment.Exit(-1);
            }
        }
    }
}
