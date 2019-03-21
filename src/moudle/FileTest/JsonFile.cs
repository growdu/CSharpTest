using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileTest
{
    
   public class JsonFile
    {

        static void Main(string[] args)
        {
            string path = @"D:\舆情数据";
            path = DirTest.CreateDir(path);

            string filaName = @"D:\downloads\2019-03-187bca3f2b-cbbe-47b1-be18-0a8d4a7c032f-yhtx.T_INDEX_WEIGHT_TX.json";
            //string json = File.ReadAllText(filaName);
            JObject root = null;
            using (System.IO.StreamReader file = System.IO.File.OpenText(filaName))
            {
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    root = (JObject)JToken.ReadFrom(reader);
                }
            }
            for(int i = 0; i < root.Count; i++)
            {
                JObject node =(JObject) root[i.ToString()];
                string json = node.ToString();
                Trade trade = JsonConvert.DeserializeObject<Trade>(json);
            }
        }


        public class Trade
        {
            /// <summary>
            /// 
            /// </summary>
            public double F_INDEX_ID {
                get
                {
                    return (int)id;
                }
                set
                {
                    id = value;
                }
            }
            double id;

            /// <summary>
            /// 
            /// </summary>
            public string F_INDEX_CODE { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public double F_TRADE_DATE
            {
                get
                {
                    return (int)date;
                }
                set
                {
                    date = value;
                }
            }
            double date;

            /// <summary>
            /// 
            /// </summary>
            public float F_NEW { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double F_PRECLOSE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double F_OPEN { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double F_HIGH { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double F_LOW { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double F_CLOSE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string F_VOLUME { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string F_AMOUNT { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double S_SEQ { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double S_ROW_VERSION { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string S_UPDATE_TIME { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double S_DATA_STATE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string number { get; set; }
        }

    }
}
