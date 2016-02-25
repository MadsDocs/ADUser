using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADUser
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                Console.Title = Classes.variablen.version;
                Console.WriteLine("Hilfe? Einfach mal die Enter Taste drücken :)");
                Classes.Init.init();


                do
                {
                    string befehl;
                    
                    befehl = Console.ReadLine();

                    switch (befehl)
                    {
                        case "add":
                            Classes.GetADUser.DisplayADAUser();
                            break;
                        case "display":
                            Classes.GetADUser.DisplayADUserwDB();
                            break;
                        case "truncate":
                            Classes.Interakt.TRUNCATE();
                            break;
                        case "select":
                            Classes.Interakt.SELECT();
                            break;
                        case "exit":
                            Environment.Exit(-1);
                            break;
                        case "cls":
                            Console.Clear();
                            break;
                        case "ver":
                            Console.WriteLine("ADUser Version: " + Classes.variablen.version);
                            Console.WriteLine("Branch: " + Classes.variablen.branch);
                            break;
                        case "env":
                            Console.WriteLine(Classes.variablen.appdata);
                            break;
                        case "stat":
                            Classes.misc.ShowStatus(Classes.variablen.can_start);
                            break;
                        case "check":
                            Classes.Interakt.CHECK();
                            break;
                        case "add --continue":
                            Console.WriteLine("Veraltet");
                            break;
                        case "debug":
                            Console.WriteLine(Classes.variablen.connectionstring);
                            string location = System.Reflection.Assembly.GetExecutingAssembly().Location;
                            Console.WriteLine(location);
                            Classes.misc.ShowDomain();
                            break;
                        case "dc":
                            Classes.misc.isValidDomain();
                            break;
                        case "Install":
                            Classes.Init.Connector();
                            break;
                        default:
                            Classes.misc.ShowHelp();
                            break;
                    }





                } while (true);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.InnerException);

                Console.ReadLine();
            }
            
        }
    }
}
