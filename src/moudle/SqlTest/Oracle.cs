using System;
using System.Collections.Generic;
using System.Data.OleDb;
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
* 描    述：访问oracle
***********************************************************************/
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
