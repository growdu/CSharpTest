using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace SqlTest
{
    class Oracle
    {
        static string _conString = @"Provider=OraOLEDB.Oracle.1;Data Source=127.0.0.1/orcl;User ID=scott;PassWord=tiger";
        public void GetData()
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(_conString))
                {
                    conn.Open();//打开连接
                    using (OleDbCommand cmdd = conn.CreateCommand())
                    {
                        cmdd.CommandText = "select * from scott.Test";
                        using (OleDbDataReader oleRead = cmdd.ExecuteReader())
                        {
                            if (oleRead == null)
                            {
 
                            }
                            if (oleRead.FieldCount == 0 || !oleRead.HasRows)
                            {

                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
