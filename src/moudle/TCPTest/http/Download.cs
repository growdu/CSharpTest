using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tools.DataHelper;

namespace TCPTest.http
{
    class Download
    {

        /// <summary>
        /// 获取网页的HTML码
        /// </summary>
        /// <param name="url">链接地址</param>
        /// <param name="encoding">编码类型</param>
        /// <returns></returns>
        public static string GetHtmlStr(string url, string encoding)
        {
            string htmlStr = "";
            if (!String.IsNullOrEmpty(url))
            {
                WebRequest request = WebRequest.Create(url);            //实例化WebRequest对象
                WebResponse response = request.GetResponse();           //创建WebResponse对象
                Stream datastream = response.GetResponseStream();       //创建流对象
                Encoding ec = Encoding.Default;
                if (encoding == "UTF8")
                {
                    ec = Encoding.UTF8;
                }
                else if (encoding == "Default"||encoding.IsEmpty())
                {
                    ec = Encoding.Default;
                }
                StreamReader reader = new StreamReader(datastream, ec);
                htmlStr = reader.ReadToEnd();                           //读取数据
                reader.Close();
                datastream.Close();
                response.Close();
            }
            return htmlStr;
        }

        static void Main(string[] args)
        {
            var html=GetHtmlStr("http://eid.csrc.gov.cn/fund/web/html_view.instance?instanceid=5839599", "UTF8");
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var nodes = doc.DocumentNode.SelectNodes("/html/body/table");
            nodes = doc.DocumentNode.SelectNodes("//table");
            Console.WriteLine(html);
        }

    }
}
