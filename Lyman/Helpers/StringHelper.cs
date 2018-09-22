using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;

namespace Lyman.Helpers
{
    /// <summary>
    /// 文字列操作に関する補助機能を提供します。
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// IEnumerable を文字列に変換します。
        /// </summary>
        /// <returns>The numerable to string.</returns>
        /// <param name="strs">Strs.</param>
        public static string IEnumerableToString(IEnumerable<string> strs)
        {
            var sb = new StringBuilder();
            foreach(var str in strs)
            {
                sb.AppendLine(str);
            }
            return sb.ToString();
        }
    }
}
