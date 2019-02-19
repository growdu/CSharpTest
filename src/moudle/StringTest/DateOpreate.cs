using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringTest
{
    /// <summary>
    /// 字符串日期操作
    /// </summary>
    public class DateOpreate
    {
        /// <summary>
        /// 将日期转换为星期
        /// </summary>
        /// <param name="date">日期，格式为yyyy-MM-dd</param>
        /// <returns></returns>
        public string GetWeek(string date="")
        {
            if (date == "")
            {
                date = DateTime.Now.ToString("yyyy-MM-dd");
            }
            DateTime dateTime = Convert.ToDateTime(date);
            string week = dateTime.ToString("dddd");
            return week;
        }

    }
}
