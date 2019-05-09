using System;
using System.Collections.Generic;
using System.Text;

namespace Util
{
    public static class Utils
    {
        public static bool IsEmpty(this string s)
        {
            return (s == null || s.Length == 0 || s.Trim() == "" || string.IsNullOrEmpty(s) || s == "　" || s == " ");
        }

    }
}
