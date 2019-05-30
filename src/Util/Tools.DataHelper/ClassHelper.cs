using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools.DataHelper
{
    public class ClassHelper
    {
        /// <summary>
        /// 根据输入字符串和分隔符构造数据实体类
        /// </summary>
        /// <param name="input">包含各字段的字符串</param>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        public string ConstructByCsv(string input, char separator=',')
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"using System;\nusing System.Collections.Generic;\nusing System.Linq;\nusing System.Text;\nnamespace Data{\n");
            var ss = input.Split(separator);
            foreach(var s in ss)
            {
                sb.Append("public string "+s+" {get;set;}\n");
            }
            sb.Append("}");
            return sb.ToString();
        }

    }
}
