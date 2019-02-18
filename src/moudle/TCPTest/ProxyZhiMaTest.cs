using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace TCPTest
{
    class ProxyZhiMaTest
    {
        static void Main(string[] args)
        {
            HttpClient hc = new HttpClient();
            //获取ip
            string uri = @"http://http.tiqu.alicdns.com/getip3?num=1&type=1&pro=&city=0&yys=0&port=1&pack=38828&ts=0&ys=0&cs=0&lb=1&sb=0&pb=4&mr=1&regions=";
            HttpResponseMessage response = hc.GetAsync(new Uri(uri)).Result;
            String result = response.Content.ReadAsStringAsync().Result;
            result=result.Replace("\r\n", "");
            String proxyServer = result; // http://host:port, 例(http://1.2.3.4:7777), host可以是域名或者ip,port是代理端口号
            var proxy = new WebProxy(proxyServer);
            HttpClientHandler httpClientHandler = new HttpClientHandler()
            {
                Proxy = proxy
            };
            var httpClient = new HttpClient(httpClientHandler);
            // 增加头部
            httpClient.DefaultRequestHeaders.Add("Header-Key", "header-vaule");

            ProxyZhiMaTest testProxy = new ProxyZhiMaTest();
            testProxy.testGet(httpClient);
            // testProxy.testPost(httpCient);
        }


        public string GetProxy()
        {
            HttpClient hc = new HttpClient();
            //获取ip
            string uri = @"http://http.tiqu.alicdns.com/getip3?num=1&type=1&pro=&city=0&yys=0&port=1&pack=38828&ts=0&ys=0&cs=0&lb=1&sb=0&pb=4&mr=1&regions=";
            HttpResponseMessage response = hc.GetAsync(new Uri(uri)).Result;
            String result = response.Content.ReadAsStringAsync().Result;
            result = result.Replace("\r\n", "");
            return result;
        }

        //添加白名单
        public bool AddWhiteMenu(string ip)
        {
            HttpClient hc = new HttpClient();
            hc.DefaultRequestHeaders.Add("Header-Key", "header-vaule");
            //将ip添加到白名单
            string uri = @"web.http.cnapi.cc/index/index/save_white?neek=60593&appkey=abae9aeedea00707b98d1c9d921b27c5&white="+ip;
            HttpResponseMessage response = hc.GetAsync(new Uri(uri)).Result;
            String result = response.Content.ReadAsStringAsync().Result;
            JObject json= (JObject)JsonConvert.DeserializeObject(result);
            if (json["success"].ToString()!="ok"&&json["msg"].ToString()!= "该ip已经在您的白名单中")
                return false;

            return true;
        }

        public bool DeleteWhiteMenu(string ip)
        {
            HttpClient hc = new HttpClient();
            hc.DefaultRequestHeaders.Add("Header-Key", "header-vaule");
            //将ip添加到白名单
            string uri = @"web.http.cnapi.cc/index/index/del_white?neek=60593&appkey=abae9aeedea00707b98d1c9d921b27c5&white="+ip;
            HttpResponseMessage response = hc.GetAsync(new Uri(uri)).Result;
            String result = response.Content.ReadAsStringAsync().Result;
            JObject json = (JObject)JsonConvert.DeserializeObject(result);
            if (json["success"].ToString() != "ok")
                return false;

            return true;
        }

        // 测试get请求
        public void testGet(HttpClient httpClient)
        {
            String targetUrl = "http://http://h.zhimaruanjian.com/";
            var httpResult = httpClient.GetStringAsync(targetUrl).Result;
        }

        // 测试post请求
        public void testPost(HttpClient httpClient)
        {
            String targetUrl = "http://httpbin.org/post";
            List<KeyValuePair<string, string>> formData = new List<KeyValuePair<string, string>>();
            formData.Add(new KeyValuePair<string, string>("key1", "vaule1"));
            formData.Add(new KeyValuePair<string, string>("key2", "vaule2"));
            var formContent = new FormUrlEncodedContent(formData.ToArray());
            var responseMsg = httpClient.PostAsync(targetUrl, formContent).Result;
            var httpResult = responseMsg.Content.ReadAsStringAsync().Result;
        }

    }
}
