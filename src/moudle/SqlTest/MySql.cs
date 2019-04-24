using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
