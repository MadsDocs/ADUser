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
        public static void InsertData (string sAMAccount, DateTime lastLogonTimeStampt, int logonCount)
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
                        logger._ilogger("MySQL Command ausgeführt...");
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
                    logger._ilogger("MySQL Command ausgeführt...");
                    con.Close();
                }


                
                
            }
            catch (Exception ex)
            {
                logger._elogger(ex);
            }
            
        }
    }
}
