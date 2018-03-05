using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessInfoSpider.Utility
{
    public static class StringHandler
    {
        /// <summary>
        /// Replace字符串中的空格，换行符，中括号为空字符
        /// </summary>
        /// <param name="str">数据</param>
        /// <returns>处理后数据</returns>
        public static string ReplaceStringExtend(string str)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            return str.Replace(" ", "").Replace("\n", " ").Replace("[", "").Replace("]", "");
        }
    }
}
