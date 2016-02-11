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

                Desweiteren prüft die Methode ob der MySQL Connector installiert ist und
                wenn dieser nicht installiert ist, wird dieser installiert. 

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
                }
                else
                {
                    Directory.CreateDirectory(variablen.appdata + @"\UserData\");
                    Directory.CreateDirectory(variablen.appdata + @"\UserData\Logs");
                    Directory.CreateDirectory(variablen.appdata + @"\UserData\Temp");
                    Directory.CreateDirectory(variablen.appdata + @"\UserData\Updates");
                    Directory.CreateDirectory(variablen.appdata + @"\UserData\Updates\Changelogs");

                    logger._ilogger("Directory wurde erfolgreich erstellt..");
                    logger._ilogger("Versuche nun den MySQL Connector zu installieren");

                    WebClient mysql = new WebClient();
                    mysql.DownloadFile(@"\\srv00\Deployment\MySQL Connector\mysql-connector-net-6.9.8.msi", variablen.appdata + @"\UserData\Temp\mysql-connector-net-6.9.8.msi");
                    Process.Start(variablen.appdata + @"\UserData\Temp\mysql-connector-net-6.9.8.msi", "/quiet");

                    if(Directory.Exists(@"C:\Program Files (x86)\MySQL"))
                    {
                        Console.WriteLine("MySQL Connector wurde installiert... Das Programm kann nun verwenden werden!");
                        logger._ilogger("MySQL Connector wurde installiert... Überprüfung des Ordner war positiv (Untersuchter Ordner: C:\\Program Files (x86)\\MySQL");
                        variablen.can_start = true;
                        logger._ilogger("Neuer ADUser Start Status ist: " + variablen.can_start);

                    }
                    else
                    {
                        

                        var result = MessageBox.Show(variablen.message, variablen.caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            Console.WriteLine("Wir versuchen noch mal eine Automatische Installation.");
                            WebClient connector = new WebClient();
                            connector.DownloadFile(@"\\srv00\Deployment\MySQL Connector\mysql-connector-net-6.9.8.msi", variablen.appdata + @"\UserData\Temp\mysql-connector-net-6.9.8.msi");
                            Process.Start(variablen.appdata + @"\UserData\Temp\mysql-connector-net-6.9.8.msi", "/quiet");

                            if(!Directory.Exists(@"C:\Program Files (x86)\MySQL"))
                            {
                                MessageBox.Show(variablen.error_message, variablen.error_caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                variablen.can_start = false;
                                logger._sLogger(variablen.can_start);
                                Environment.Exit(-1);

                            }
                            else
                            {
                                Console.WriteLine("MySQL Connector wurde installiert, das Programm kann nun verwendet werden!");
                                variablen.can_start = true;
                                logger._sLogger(variablen.can_start);
                            }



                        }
                        else
                        {
                            MessageBox.Show(variablen.error_message, variablen.error_caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            logger._wlogger("MySQL Connector konnte nicht installiert werden! Bitte den MySQL Connector manuell installieren!");
                            variablen.can_start = false;
                            logger._sLogger(variablen.can_start);
                        }

                        
                    }

                }
            }
            catch (Exception ex)
            {
                logger._elogger(ex);
            }

           
        }
    }
}
