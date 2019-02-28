using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TCPTest
{
    /// <summary>
    /// 发送和接收http请求，主要是post和get
    /// </summary>
    public class HttpTest
    {
        static Object o = new object();


        public struct Message
        {
            public int code;
            public string message;

            public override string ToString()
            {
                JObject jobj = new JObject(
                    new JProperty("code", code.ToString()), new JProperty("message", message));
                return jobj.ToString();
            }
        }
        //static bool isTest = true;

        /// <summary>
        /// 监听网络地址
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="isTest"></param>
        /// <returns></returns>
        public static bool Listen(string uri, bool isTest = true)
        {
            HttpListener listener = new HttpListener();
            while (true)
            {
                try
                {
                    listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
                    listener.Prefixes.Add(uri);
                    listener.Start();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failure.");
                    return false;
                }

                Console.WriteLine("服务启动成功，等待客户连接中......");
                while (true)
                {
                    //等待请求连接
                    //没有请求则GetContext处于阻塞状态
                    HttpListenerContext ctx = listener.GetContext();

                    ThreadPool.QueueUserWorkItem(new WaitCallback(TaskParse), ctx);
                }
            }
        }

        /// <summary>
        /// 解析接收到的数据
        /// </summary>
        /// <param name="o"></param>
        private static void TaskParse(object o)
        {
            string ip = "";
            HttpListenerContext ctx = (HttpListenerContext)o;
            ip = ctx.Request.RemoteEndPoint.Address.ToString();
            Console.WriteLine(DateTime.Now.ToString() + "：收到来自" + ip + "的消息。");
            Message message = new Message();
            try
            {
                //接收url链接
                string filename = Path.GetFileName(ctx.Request.RawUrl);
                //接收post信息
                Stream stream = ctx.Request.InputStream;
                System.IO.StreamReader reader = new System.IO.StreamReader(stream, Encoding.UTF8);
                string body = reader.ReadToEnd();
                //body = HttpUtility.UrlDecode(body);
                Console.WriteLine("正在进行解析......");
                ctx.Response.StatusCode = 200;
                message.code =0;
                message.message = "消息接收成功。";
                Response(ctx, message.ToString());
                Console.WriteLine("成功收到数据:" + filename);
            }
            catch (Exception ex)
            {
                ctx.Response.StatusCode = 500;//系统错误
                message.code = -1;
                message.message = "系统错误，请稍后重试。";
                Response(ctx, message.ToString());
                Console.WriteLine(ex.ToString());
                return;
            }
        }

        /// <summary>
        /// 根据请求返回相应的状态
        /// </summary>
        /// <param name="ctx">http服务器</param>
        /// <param name="message">要返回的json字符串</param>
        static void Response(HttpListenerContext ctx, string message)
        {
            //使用Writer输出http响应代码,UTF8格式
            using (StreamWriter writer = new StreamWriter(ctx.Response.OutputStream, Encoding.UTF8))
            {
                writer.Write(message);
                writer.Close();
                ctx.Response.Close();
            }
        }

        /// <summary>
        /// 采用post方法传输数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static bool PostJson(string url, string json)
        {
            if (url == null || url == "")
            {
                url = "http://210.13.70.67:15000/news/send";
            }
            bool result = false;
            //生成文件流
            byte[] buffer = Encoding.UTF8.GetBytes(json);
            //向流中写字符串
            StreamWriter writer = null;
            //根据url创建请求对象
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //设置发送方式
            request.Method = "POST";
            //提交长度
            request.ContentLength = buffer.Length;
            //发送内容格式
            request.ContentType = "text/json";
            try
            {
                writer = new StreamWriter(request.GetRequestStream());
                writer.Write(json);
            }
            catch (Exception)
            {
                result = false;

            }
            finally
            {
                writer.Close();
            }
            //读取服务器返回信息
            HttpWebResponse objresponse = (HttpWebResponse)request.GetResponse();
            using (StreamReader sr = new StreamReader(objresponse.GetResponseStream()))
            {
                string message = sr.ReadToEnd();
                result = true;
                sr.Close();
            }
            return false;
        }

    }
}
