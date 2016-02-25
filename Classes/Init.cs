using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Net;
using System.Diagnostics;
using System.Windows.Forms;

namespace ADUser.Classes
{
    class Init
    {

        /*
                Dies ist die Init Methode, diese wird gebraucht damit 
                die Logger ordentlich mit loggen können und das die
                Installation des MySQL Connectors ordentlich funktioniert

                Sollte diese Methode einen Fehler zurück geben
                (abgefragt hier durch die Try / Catch Methode
                wird die Variable can_start auf false gesetzt
                und das Programm startet nicht

                Die Init Methode wird Funktionen enthalten die
                unter %APPDATA% einen Ordner erstellt wird welcher
                wie folgt aussieht:
                            -- Root Ordner (UserData)
                                -- Logs
                                -- Temp (Dieser Ordner wird dazu verwendet um zB den MySQL Connector zwischen zu speichern)
                                -- Updates
                                    --Changelogs


        */
        public static void init()
        {
            try
            {
                if (Directory.Exists(variablen.appdata + @"\UserData\"))
                {
                    logger._ilogger("Ordner unter %APPDATA% ist vorhanden");
                    variablen.can_start = true;
                    logger._ilogger("Neuer ADUser Start Status ist: " + variablen.can_start);
                    Deployment.ChecIfMySqlConnector();

                    if (variablen.found == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Keinen MySQL Connector gefunden, bitte Install ausführen damit die MySQL Funktionen des Programms benutzt werden können!");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else
                    {

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("MySQL Connector gefunden");
                        Console.ForegroundColor = ConsoleColor.Gray;

                        logger._ilogger("MySQL Connector wurde installiert");
                    }

                }
                else
                {
                    Directory.CreateDirectory(variablen.appdata + @"\UserData\");
                    Directory.CreateDirectory(variablen.appdata + @"\UserData\Logs");
                    Directory.CreateDirectory(variablen.appdata + @"\UserData\Temp");
                    Directory.CreateDirectory(variablen.appdata + @"\UserData\Updates");
                    Directory.CreateDirectory(variablen.appdata + @"\UserData\Updates\Changelogs");
                    Directory.CreateDirectory(variablen.appdata + @"\UserData\Installation");

                    logger._ilogger("Directory wurde erfolgreich erstellt..");
                }
            }
            catch (Exception ex)
            {
                logger._elogger(ex);
            }

           
        }

        public static void Connector()
        {
            try
            {
                if (variablen.found)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("MySQL Connector wurde gefunden!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    WebClient client = new WebClient();
                    client.DownloadFile("http://dev.mysql.com/get/Downloads/Connector-Net/mysql-connector-net-6.9.8.msi", variablen.appdata + @"\UserData\Installation\mysql-connector.msi");

                        Process proc = new Process();
                        proc.StartInfo.WorkingDirectory = variablen.appdata + @"\UserData\Installation";
                        proc.StartInfo.FileName = "msiexec";
                        proc.StartInfo.Arguments = string.Format(@"/quiet");
                        proc.StartInfo.Arguments = string.Format(@"/package  " + variablen.appdata + @"\UserData\Installation\mysql-connector.msi");
                        proc.StartInfo.UseShellExecute = false;
                        proc.StartInfo.CreateNoWindow = false;
                        proc.Start();
                        proc.WaitForExit();
                }

              


            }
            catch (Exception ex)
            {
                logger._elogger(ex);
            }
        }
    }
}
