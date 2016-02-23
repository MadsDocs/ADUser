using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;

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

            REWRITE: 23.02.2016..

        */
        public static void ChecIfMySqlConnector()
        {
            try
            {
                List<String> softwareList = new List<String>();
                RegistryKey products = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Classes\Installer\Products");
                foreach (String keyName in products.GetSubKeyNames())
                    foreach (String valueName in products.OpenSubKey(keyName).GetValueNames())
                        if (valueName == "ProductName")
                        {
                            String entry = products.OpenSubKey(keyName).GetValue("ProductName").ToString();
                            if (entry != null)
                                softwareList.Add(entry);
                        }


                
                foreach (string sName in softwareList)
                {
                    if (sName.Contains("MySQL Connector Net"))
                    {
                        variablen.found = true;
                        Console.WriteLine("MySQL Connector wurde gefunden!");
                    }
                  
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
