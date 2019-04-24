using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlTest
{
    class MySql
    {
        static string _conString = "server=127.0.0.1;user id=root;password=123;database=test";
        public static MySqlDataReader GetDataReader()
        {
            using (MySqlConnection myCon = new MySqlConnection(_conString))
            {
                try
                {
                    myCon.Open();
                    string cmd = "SELECT * FROM web";
                    using (MySqlCommand myCmd = new MySqlCommand(cmd, myCon))
                    {
                        MySqlDataReader reader = myCmd.ExecuteReader();
                        return reader;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return null;
        }

        public void GetData()
        {
            MySqlDataReader reader = GetDataReader();
            while (reader.Read())
            {

            }
        }
    }
}
