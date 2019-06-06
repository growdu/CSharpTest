using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Tools.DataHelper
{
    public static partial class TableHelper
    {

        #region 非扩展方法
        /// <summary>
        /// 利用反射将对象的值映射到Datatable，前提是Datatable的各字段与对象的特性相对应
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        public static DataRow SetRowValue(DataRow dr,object o)
        {
            var infos = o.GetType().GetProperties();
            foreach (var info in infos)
            {
                var field = (FieldAttribute)info.GetCustomAttributes(typeof(FieldAttribute), false)[0];
                dr[field.Fields] = info.GetValue(o,null);
            }
            return dr;
        }

        /// <summary>
        /// 两个DataTable行互相赋值的函数
        /// </summary>
        /// <param name="to">目标表</param>
        /// <param name="from">来源表</param>
        /// <returns></returns>
        public static string RowToRowFunc(DataTable to,DataTable from)
        {
            StringBuilder sb = new StringBuilder();
            int max = Math.Max(to.Columns.Count,from.Columns.Count);
            sb.Append("/// <summary>\n/// 将from行的数据赋值到目标行source</summary>\n/// <param name=\"to\">目标行</param>\n/// <param name=\"from\">来源行</param>");
            sb.Append("public void GetData(ref to,DataRow from){\n");
            for(int i = 0; i < max; i++)
            {
                sb.Append("to[\"" +to.Columns[i].ColumnName+ "\"]" );
                sb.Append("=");
                sb.Append("from[\"" + from.Columns[i].ColumnName + "\"]");
            }
            return sb.ToString();
        }

        #endregion

        #region DataTable 扩展方法系列

        /// <summary>
        /// 利用反射将对象的值映射到Datatable，前提是Datatable的各字段与对象的特性相对应
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="objects">对象集合</param>
        /// <returns></returns>
        public static DataTable SetTableValue(this DataTable dt,IEnumerable<object> objects)
        {
            foreach (var o in objects)
            {
                DataRow dr = dt.NewRow();
                dr = dr.SetValue(o);
                dt.Rows.Add(dr);
            }
            return dt;
        }

        /// <summary>
        /// 利用反射给Datatable的行赋值
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="o">包含Filed（数据库字段名）特性的对象</param>
        /// <returns></returns>
        public static DataRow SetValue(this DataRow dr, object o)
        {
            var infos = o.GetType().GetProperties();
            foreach (var info in infos)
            {
                var attrs = info.GetCustomAttributes(typeof(FieldAttribute), false);
                if (attrs == null || attrs.Length == 0)
                    continue;

                var field = (FieldAttribute)attrs[0];
                dr[field.Fields] = info.GetValue(o, null);
            }
            return dr;
        }

        /// <summary>
        /// 将datatable转换为一个class，将列作为类的属性，并将列的名字作为特性
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToClass(this DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"using System;\nusing System.Collections.Generic;\nusing System.Linq;\nusing System.Text;\nusing Tools.DataHelper;\nnamespace Data{\n");
            sb.Append("public class "+dt.TableName+"{\n");
            foreach (DataColumn col in dt.Columns)
            {
                sb.Append("[Field("+ col.ColumnName+")]");
                sb.Append("public string " + col.ColumnName + " {get;set;}\n");
            }
            sb.Append("}");
            sb.Append("\n}");
            return sb.ToString();
        }

        /// <summary>
        /// 将datatable转换为一个class，将列作为类的属性，并将所给字符串作为特性；若所给字符串分隔后的数目与列不相等，则将列名作为特性
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="field">对应另一张表的字段字符串</param>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        public static string ToClass(this DataTable dt,string field, char separator = ',')
        {
            var fields = field.Split(separator);
            return dt.ToClass(fields);
        }

        /// <summary>
        /// 将datatable转换为一个class，将列作为类的属性，并将所给字符串作为特性；若所给字符串分隔后的数目与列不相等，则将列名作为特性
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fields">需要映射的字段名</param>
        /// <returns></returns>
        public static string ToClass(this DataTable dt, IEnumerable<string> fields)
        {
            var fs= fields.ToArray();
            if (fields == null || fields.Count() != dt.Columns.Count)
                return dt.ToClass();

            StringBuilder sb = new StringBuilder();
            sb.Append(@"using System;\nusing System.Collections.Generic;\nusing System.Linq;\nusing System.Text;\nusing Tools.DataHelper;\nnamespace Data{\n");
            sb.Append("public class " + dt.TableName + "{\n");
            int i = 0;
            foreach (DataColumn col in dt.Columns)
            {
                sb.Append("[Field(" + fs[i] + ")]");
                i++;
                sb.Append("public string " + col.ColumnName + " {get;set;}\n");
            }
            sb.Append("}");
            sb.Append("\n}");
            return sb.ToString();
        }

        #endregion

        public static void CreateSqlTable(this DataTable dt,SqlConnection connection,string tableName)
        {
            int count = dt.Columns.Count;
            string[] colNames = new string[count] ;
            string[] colTypes = new string[count] ;
            for (int i=0;i<count;i++)
            {
                DataColumn dc = dt.Columns[i];
                colNames[i] = dc.ColumnName;
                colTypes[i] = dc.DataType.ToString();
            }
            string queryString = "CREATE TABLE IF NOT EXISTS " + tableName + "( " + colNames[0] + " " + colTypes[0];
            for (int i = 1; i < colNames.Length; i++)
            {
                queryString += ", " + colNames[i] + " " + colTypes[i];
            }
            queryString += "  ) ";
            using (SqlCommand cmd=new SqlCommand(queryString,connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

    }
}
