using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringTest
{
    /***********************************************************************
     * 文 件 名：JsonFormat.cs
     * CopyRight(C) 2019-2029 https://www.github.com/growdu
     * 创 建 人：growdu
     * 创建日期：2019-04-08
     * 修 改 人：
     * 修改日期：
     * 描    述：json字符串使用案例（更多用法在FileTest下的JsonFile.cs）
     ***********************************************************************/
    class JsonFormat
    {
        /// <summary>
        /// 将字符串转换为json
        /// </summary>
        public static void StringToJson()
        {
            string te = "media\"N\"ame";
            JObject jobj = new JObject(
                    new JProperty("mediaName", te), new JProperty("mediaCode", "mediaCode")
                    );
            string test = jobj.ToString();
            Console.WriteLine(test);
        }

    }
}
