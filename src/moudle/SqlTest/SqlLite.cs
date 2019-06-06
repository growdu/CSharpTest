using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlTest
{
    class SqlLite
    {
        private string _dbPath;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbPath">数据库文件路径</param>
        public SqlLite(string dbPath)
        {
            _dbPath = dbPath;
            if (!File.Exists(_dbPath))
            {
                SQLiteConnection.CreateFile(_dbPath);
            }
        }


        static void Main()
        {
            var fileName = @"test.db";
            SqlLite lite = new SqlLite(fileName);
            string q = @"SELECT * FROM user";
            Parallel.Invoke(()=> {
                var dt = lite.Query(q);
                Console.WriteLine(dt.Rows.Count);
            }, () => {
                var dt = lite.Query(q);
                Console.WriteLine(dt.Rows.Count);
            });
            //string q = @"INSERT INTO user(Name,Age,Email) VALUES('root','99','136')";
            //lite.ExecuteQuery(q);
            //List<string> qs = new List<string>();
            //for (int i=0;i<10;i++)
            //{
            //    qs.Add(q);
            //}
            //lite.ExecuteQuerys(qs);
            if (!File.Exists(fileName))
            {
                SQLiteConnection.CreateFile(fileName);
            }
            string connectionString = "data source = " + fileName;
            using (SQLiteConnection dbConnection = new SQLiteConnection(connectionString))
            {
                dbConnection.Open();
                string cmd = "SELECT * FROM user";
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd, dbConnection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    //dt.PrimaryKey = new DataColumn[] { dt.Columns["ID"] };
                    SQLiteCommandBuilder thisBuilder = new SQLiteCommandBuilder(adapter);
                    dt.Rows[0]["Email"] = "@@";
                    adapter.Update(dt);
                }
                string[] colNames = new string[] { "ID", "Name", "Age", "Email" };
                string[] colTypes = new string[] { "INTEGER", "TEXT", "INTEGER", "TEXT" };

                string tableName = "user";

                string queryString = "CREATE TABLE IF NOT EXISTS " + tableName + "( " + colNames[0] + " " + colTypes[0];

                for (int i = 1; i < colNames.Length; i++)
                {
                    queryString += ", " + colNames[i] + " " + colTypes[i];
                }
                queryString += "  ) ";
                SQLiteCommand dbCommand = dbConnection.CreateCommand();
                dbCommand.CommandText = queryString;
                SQLiteDataReader dataReader = dbCommand.ExecuteReader();
            }

        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="q">sql语句</param>
        /// <returns></returns>
        public DataTable Query(string q)
        {
            try
            {
                string connectionString = "data source = " + _dbPath;
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(q, connection))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }  
            }
            catch
            {
                return null;
            }
           
        }

        public void Update(DataTable dt,string tableName,string primaryColName)
        {
            try
            {
                string q = "update from "+tableName+" set ";
                List<string> querys = new List<string>();
                foreach (DataRow dr in dt.Rows)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(q);
                    foreach (DataColumn dc in dt.Columns)
                    {
                        string name = dc.ColumnName;
                        if (name == primaryColName)
                            continue;

                        sb.Append(name+"='"+dr[name]+"'");
                        sb.Append(",");
                    }
                    sb.Remove(sb.Length-1,1);
                    sb.Append(" where "+primaryColName+"='"+dr[primaryColName]+"'");
                    querys.Add(sb.ToString());
                }
                ExecuteQuerys(querys);
            }
            catch
            {

            }
        }

        public void Insert(DataTable dt,string tableName)
        {
            try
            {
                string q = @"INSERT INTO "+tableName+"(";
                foreach (DataColumn dc in dt.Columns)
                {
                    string name = dc.ColumnName;
                    q += name + ",";
                 }
                q=q.Remove(q.Length-1,1);
                q += ") VALUES(";
                List<string> querys = new List<string>();
                foreach (DataRow dr in dt.Rows)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(q);
                    foreach (DataColumn dc in dt.Columns)
                    {
                        string name = dc.ColumnName;
                        sb.Append("'"+dr[name]+"'"+",");
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append(")");
                    querys.Add(sb.ToString());
                }
                ExecuteQuerys(querys);
            }
            catch
            {
                
            }
        }

        public bool Delete(DataTable dt,string tableName,string primaryColName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"delete from " + tableName + " where " + primaryColName + " in (");
            foreach (DataRow dr in dt.Rows)
            {
                sb.Append("'"+dr[primaryColName]+"',");
            }
            sb.Remove(sb.Length-1,1);
            ExecuteQuery(sb.ToString());
            return true;
        }

        /// <summary>
        /// 执行单条sql语句
        /// </summary>
        /// <param name="q"></param>
        public void ExecuteQuery(string q)
        {
            try
            {
                string connectionString = "data source = " + _dbPath;
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(q, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
               
            }
            catch 
            {

            }

        }


        /// <summary>
        /// 执行多条sql语句(事务)
        /// </summary>
        /// <param name="querys"></param>
        public void ExecuteQuerys(List<string> querys)
        {
            for (int i = 0; i < querys.Count; i++)
            {
                ExecuteQuery(querys[i]);
            }
        }


    }
}

      
