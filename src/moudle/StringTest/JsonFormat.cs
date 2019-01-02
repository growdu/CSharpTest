using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringTest
{
    class JsonFormat
    {
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
