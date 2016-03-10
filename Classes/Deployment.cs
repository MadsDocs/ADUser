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
                Classes.logger._eLogger(ex);
                Console.WriteLine("Fehler bei Installation des MySQL Connectors... Aborting");
                Console.ReadLine();
                Environment.Exit(-1);
            }
        }
    }
}
