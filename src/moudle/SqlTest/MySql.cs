using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SqlTest
{
    /***********************************************************************
* 文 件 名：
* CopyRight(C) 2019-2029 https://www.github.com/growdu
* 创 建 人：growdu
* 创建日期：2019-04-24
* 修 改 人：
* 修改日期：
* 描    述：访问mysql
***********************************************************************/
    class MySql
    {
        static string _conString = "server=127.0.0.1;user id=python;password=python;database=web";
        public static void  GetDataReader()
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
                        while (reader.Read())
                        {

                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public static DataTable GetDataTable()
        {
            using (MySqlConnection myCon = new MySqlConnection(_conString))
            {
                try
                {
                    myCon.Open();
                    string cmd = "SELECT * FROM user";
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd, myCon))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                return null;
            }
        }

        public void GetData()
        {
            
        }
    }
}
