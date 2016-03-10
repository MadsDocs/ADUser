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
                    logger._logger("Ordner unter %APPDATA% ist vorhanden");
                    variablen.can_start = true;
                    logger._logger("Neuer ADUser Start Status ist: " + variablen.can_start);
                    Deployment.ChecIfMySqlConnector();
                    Console.WriteLine(Environment.UserDomainName);

                    SearchforDLL();
                    SearchForConfig();

                    bool isDomain = isDomainPC(variablen.domain);

                    if(isDomain)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("PC ist DomänenPC");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("PC ist kein DomänenPC");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }


                    if (variablen.found == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Keinen MySQL Connector gefunden, bitte Install ausführen damit die MySQL Funktionen des Programms benutzt werden können!");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else
                    {

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("MySQL Connector / DLL Check = Gefunden" );
                        Console.ForegroundColor = ConsoleColor.Gray;

                        logger._logger("MySQL Connector wurde installiert");
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

                    logger._logger("Directory wurde erfolgreich erstellt..");
                }
            }
            catch (Exception ex)
            {
                logger._eLogger(ex);
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
                logger._eLogger(ex);
            }
        }

        public static void SearchforDLL()
        {
            string env = variablen.enviroment;
            string Filename = @"\MySql.Data.dll";

            string dll = env + Filename;
            variablen.dll = dll;

            Console.WriteLine(dll);

            if ( File.Exists (dll))
            {
                logger._logger("MySqlData.dll gefunden!");
                variablen.dll_found = true;
            }
            else
            {
                if (!File.Exists(env + @"\MySqlData.dll"))
                {
                    if (Directory.Exists(@"C:\Program Files (x86)\MySQL"))
                    {
                        if (Directory.Exists("MySQL Connector Net 6.9.8"))
                        {
                            File.Copy(@"C:\Program Files (x86)\MySQL\MySQL Connector Net 6.9.8\Assemblies\v4.5\MySql.Data.dll", env + @"\MySql.Data.dll");
                        }
                        logger._logger("MySqlData.dll wurde nicht gefunden!");
                    }
                    else
                    {
                        Connector();
                    }
                }

            }

        }

        public static void SearchForConfig()
        {
            string env = variablen.enviroment;
            string config = @"\App.config";

            string pfad = env + config;

            Console.WriteLine(pfad);

            if( File.Exists(pfad))
            {
                ///TODO: Bessere Abfrage reinprogrammiern
                // Loggen das die app.config existiert

            }
            else
            {
                // Loggen das die app.config nicht existiert...
            }
        }

        public static bool isDomainPC(string domain)
        {
            //Diese Funktion überprüft ob der PC zu einer Domäne gehört oder nicht
            //Siehe auch Tasklist Item 11

            string computer = Environment.UserDomainName.ToLower();

            

            if (computer.Contains(variablen.domain))
            {
                //Dann befinden wir uns in der gegebenen Domäne...
                return true;
            }
            else
            {
                //Tja, dann halt nicht
                return false;
            }
        }
    }
}
