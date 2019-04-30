using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SqlTest.DapperTest
{
    class Connect
    {
        public static MySqlConnection MySqlConnection()
        {
            string mysqlConnectionStr = ConfigurationManager.AppSettings["MysqlConnectionStr"];
            MySqlConnection connection = new MySqlConnection(mysqlConnectionStr);
            connection.Open();
            return connection;
        }

        public static List<T> Select<T>(T t)
        {
            using (IDbConnection conn = MySqlConnection())
            {
                string sqlCommandStr = @"SELECT * FROM USER";
                List<T> userList = conn.Query<T>(sqlCommandStr).ToList();
                return userList;
            }
        }

        public static void InsertInfo<T>(T t)
        {
            string insertSqlStr = @"INSERT INTO  user(id,name,memo,date,password)VALUES(@id,@name,@memo,@date,@password)";

            using (IDbConnection conn = MySqlConnection())
            {
                // conn.Execute(insertSqlStr, new { Name="ssss",Sex=true,Age="22",Tel="2222"});　　///这种是手动赋值
                conn.Execute(insertSqlStr, t);    //这种是按实体插入
            }
        }

        public class User
        {
            public int id { get; set; }

            public string name { get; set; }

            public string memo { get; set; }

            public string date { get; set; }

            public string password { get; set; }

        }

    }
}
