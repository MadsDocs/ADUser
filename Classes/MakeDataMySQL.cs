using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data;
using MySql.Data.MySqlClient;

using System.Data;
using System.Data.SqlClient;

namespace ADUser.Classes
{
    class MakeDataMySQL
    {
        public static void InsertData(string sAMAccount, DateTime lastLogonTimeStampt, int logonCount)
        {
            try
            {

                bool valid = variablen.isValid;


                if (lastLogonTimeStampt == variablen.nuller)
                {

                    MySqlConnection con = new MySqlConnection(Classes.variablen.connectionstring);
                    MySqlCommand cmd = con.CreateCommand();

                    cmd.CommandText = "INSERT INTO tracking VALUES (ID,@sAMAccount, @lastLogonTimeStampt, @logonCount);";
                    cmd.Parameters.AddWithValue("@sAMAccount", sAMAccount);
                    cmd.Parameters.AddWithValue("@lastLogonTimeStampt", "Never Logged On");
                    cmd.Parameters.AddWithValue("@logonCount", logonCount);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    logger._logger("MySQL Command ausgeführt...");
                    con.Close();

                }
                else
                {

                    MySqlConnection con = new MySqlConnection(Classes.variablen.connectionstring);
                    MySqlCommand cmd = con.CreateCommand();

                    cmd.CommandText = "INSERT INTO tracking VALUES (ID,@sAMAccount, @lastLogonTimeStampt, @logonCount);";
                    cmd.Parameters.AddWithValue("@sAMAccount", sAMAccount);
                    cmd.Parameters.AddWithValue("@lastLogonTimeStampt", lastLogonTimeStampt);
                    cmd.Parameters.AddWithValue("@logonCount", logonCount);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    logger._logger("MySQL Command ausgeführt...");
                    con.Close();
                }




            }
            catch (Exception ex)
            {
                logger._eLogger(ex);
            }


        }

        public static void InserDataMSSQL(string sAMAccount, DateTime lastLogonTimeStamp, int logoncount)
        {
            if (lastLogonTimeStamp == variablen.nuller)
            {

                try
                {
                    string query = "use UserData; INSERT INTO tracking VALUES (@sAMAccount, @lastLogon, @count);";
                    SqlConnection con = new SqlConnection(variablen.mssconnectionstring);
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@sAMAccount", sAMAccount);
                    cmd.Parameters.AddWithValue("@lastLogon", lastLogonTimeStamp);
                    cmd.Parameters.AddWithValue("@count", logoncount);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);

                    Console.ReadLine();
                }
            }
            else
            {

                try
                {
                    string query = "use UserData; INSERT INTO tracking VALUES (@sAMAccount, @lastLogon, @count);";
                    SqlConnection con = new SqlConnection(variablen.mssconnectionstring);
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@sAMAccount", sAMAccount);
                    cmd.Parameters.AddWithValue("@lastLogon", lastLogonTimeStamp);
                    cmd.Parameters.AddWithValue("@count", logoncount);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Bitte den Connectionstring umändern!");
                    Console.ForegroundColor = ConsoleColor.Gray;

                    logger._logger("Connectionstring ist null, beende SUB Programm InsertDataMSSQL!");
                }
            }
        }
    }
}
                
           
