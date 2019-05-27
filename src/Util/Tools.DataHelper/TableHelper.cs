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
    }
}
