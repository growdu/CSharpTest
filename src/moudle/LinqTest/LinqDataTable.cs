using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace LinqTest
{
    class LinqDataTable
    {
        public static void Query()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("name", typeof(string)),
                                        new DataColumn("sex", typeof(string)),
                                        new DataColumn("score", typeof(int)) });
            dt.Rows.Add(new object[] { "张三", "男", 1 });
            dt.Rows.Add(new object[] { "张三", "男", 4 });
            dt.Rows.Add(new object[] { "李四", "男", 100 });
            dt.Rows.Add(new object[] { "李四", "女", 90 });
            dt.Rows.Add(new object[] { "王五", "女", 77 });
            var query = from t in dt.AsEnumerable()
                        group t by new { t1 = t.Field<string>("name"), t2 = t.Field<string>("sex") } into m
                        select new
                        {
                            name = m.Key.t1,
                            sex = m.Key.t2,
                            score = m.Sum(n => n.Field<decimal>("score"))
                        };
            if (query.ToList().Count > 0)
            {
                query.ToList().ForEach(q =>
                {
                    Console.WriteLine(q.name + "," + q.sex + "," + q.score);
                });
            }
        }
    }
}
