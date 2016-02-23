using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;

namespace ADUser.Classes
{
    class variablen
    {
        /*
            Block I:
                        appdata: Dies ist die Variable für die Init Variable diese bildet den %APPDATA% Ordner ab
                        version: Dies ist die aktuelle Programmversion
                        connectionstring: Der Connectionstring um der MySQLConnection zu sagen wo der MySQL Server ist
                        branch: Aktuelle Branch des Programms
                        is_connector = Variable die anzeigt ob der MySQL Connector installiert ist
                        can_start = Variable die den aktuellen Status des Programms anzeigt

            Block II: Dies sind die Messagebox Variablen
                        


        */
        public static string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public static string connectionstring = Classes.misc.getAppSetting("connectionstring");
        public static string mssconnectionstring = Classes.misc.getAppSetting("msconnectionstring");
        public static string branch = "Development";
        public static bool is_connector = false;
        public static bool can_start = true;
        public static DateTime nuller = new DateTime(1970, 1, 1);
        public static bool found = false;

        public const string message = "Soll versucht werden den MySQL Connector neu zu installieren?";
        public const string caption = "MySQL Connector neu installieren?";
        public const string error_message = "Automatische Installation abgebrochen, bitte den MySQL Connector manuell installieren";
        public const string error_caption = "Automatische Installation abgebrochen";

        public static string domain = Classes.misc.getDomainSetting("domain");

        public static bool isValid = false;
        

       


    }
}
