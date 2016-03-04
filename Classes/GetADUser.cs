using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.IO;

namespace ADUser.Classes
{
    class GetADUser
    {
        public static void DisplayADAUser()
        {
            try
            {
                int eintraege = 0;

                if(variablen.domain == "Place your Domain here!")
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Bitte Suchdomain ändern!");
                    Console.ResetColor();
                }
                else
                {
                    PrincipalContext pc = new PrincipalContext(ContextType.Domain, variablen.domain);
                    PrincipalSearcher us = new PrincipalSearcher(new UserPrincipal(pc));
                    PrincipalSearchResult<Principal> psr = us.FindAll();



                    Console.WriteLine("Tabelle wird nun gefüllt!");
                    foreach (UserPrincipal up in psr)
                    {
                        eintraege++;
                        DateTime lastLog = up.LastLogon.GetValueOrDefault(variablen.nuller);
                        Console.WriteLine("Derzeitiger Eingefügter Eintrag: " + eintraege + "( " + up.SamAccountName + " )");
                        MakeDataMySQL.InsertData(up.SamAccountName, lastLog, 600);
                    }

                }



            }
            catch (Exception ex)
            {
                logger._elogger(ex);
            }
        }

        public static void DisplayADUserwDB()
        {
            try
            {
                Console.Clear();
                int eintraege = 0;

                if (variablen.domain == "Place your Domain here!")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Bitte Suchdomain umändern!");
                    Console.ResetColor();
                }
                else
                {

                    string udomain = Environment.UserDomainName;

                    if (variablen.domain == string.Empty)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Keine Domain angegeben!");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else
                    {
                        PrincipalContext pc = new PrincipalContext(ContextType.Domain, variablen.domain);
                        PrincipalSearcher us = new PrincipalSearcher(new UserPrincipal(pc));
                        PrincipalSearchResult<Principal> psr = us.FindAll();

                        Console.WriteLine("User werden nun angezeigt!");
                        foreach (UserPrincipal up2 in psr)
                        {
                            eintraege++;
                            DateTime lastlog = up2.LastLogon.GetValueOrDefault(variablen.nuller);
                            string ausgabe = String.Format("\r\nsAMAccount: {0}, \r\n LastLog: {1}", up2.SamAccountName, lastlog);
                            Console.WriteLine(ausgabe);

                        }
                        Console.WriteLine("Auslesen fertig! Angezeigte User: " + eintraege);
                    }                                  
                }

            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Fehler bei Domänenausgabe... ");
                Console.ForegroundColor = ConsoleColor.Gray;
                logger._elogger(ex);
            }
        }

    }
}
