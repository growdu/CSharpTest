using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Tools.DataHelper
{
    public static partial class TableHelper
    {
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

    }
}
