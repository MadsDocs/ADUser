using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;

namespace ADUser.Classes
{
    class Interakt
    {
        public static void TRUNCATE ()
        {
            bool valid = variablen.isValid;
            try
            {
                    Console.Clear();
                    MySqlConnection con = new MySqlConnection(Classes.variablen.connectionstring);
                    MySqlCommand cmd = con.CreateCommand();

                    con.Open();
                    cmd.CommandText = "use userdata";
                    cmd.CommandText = "TRUNCATE tracking";
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Console.WriteLine("TRUNCATE Befehl wurde erfolgreich ausgeführt! Benutzen Sie den add Befehl um die Tabelle wieder zu füllen!");
                    logger._ilogger("TRUNCATE Befehl wurde erfolgreich ausgeführt.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("TRUNCATE Befehl ist fehlgeschlagen...");
                Console.WriteLine(ex.Message);
                logger._elogger(ex);

                Console.ReadLine();
            }


        }

        public static void SELECT()
        {
            try
            {
                Console.Clear();
                MySqlConnection con = new MySqlConnection(Classes.variablen.connectionstring);
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "use userdata";
                cmd.CommandText = "SELECT * FROM tracking ORDER BY ID ASC";

                con.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    Console.WriteLine("Keine Daten zum anzeigen gefunden! Bitte einmal den add Befehl ausführen!");
                }
                else
                {
                    Console.WriteLine("ID\t\t|sAMAccount\t\t|LastLogon\t\t|Count\t\t|");
                    Console.WriteLine("________________________________________________");
                    while (reader.Read())
                    {
                        try
                        {
                            int id = reader.GetInt32(0);
                            string sAmaccount = reader.GetString(1);
                            DateTime lastLogon = reader.GetDateTime(2);
                            int count = reader.GetInt32(3);

                            Console.WriteLine(id + "\t\t|" + sAmaccount + "\t\t|" + lastLogon + "\t|\t" + count + "\t\t|");


                        }
                        catch (Exception ex)
                        {
                            logger._elogger(ex);
                        }

                    }
                    con.Close();
                }

            }
            catch (Exception ex)
            {
                Classes.logger._elogger(ex);
                Console.WriteLine("Fehler beim Anzeigen der Daten!");
                Console.WriteLine(ex.Message);

                Console.ReadLine();
            }
               
        }

        public static void CHECK()
        {
            try
            {

                //Diese Methode überprüft die Tabelle tracking
                MySqlConnection con = new MySqlConnection(Classes.variablen.connectionstring);
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "CHECK TABLE tracking";

                Console.WriteLine("Tabelle Tracking wird nun untersucht...");
                con.Open();

                cmd.ExecuteReader();

                con.Close();

                Console.WriteLine("Tablle wurde erfolgreich überprüft, keine Fehler!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Tabellen Überprüfung fehlgeschlagen!");
                logger._elogger(ex);
            }

        }
    }
}
