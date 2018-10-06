// using System.Collections.Generic;
// using System.Linq;
using System;
namespace Lyman.Converters
{
    /// <summary>
    /// 変換機能を提供します。
    /// </summary>
    public interface IConvertible<in TIn, out TOut>
    {
        /// <summary>
        /// ソースを変換します。
        /// </summary>
        /// <param name="source">ソース</param>
        /// <returns>送信結果</returns>
        TOut Convert(TIn source);
    }
}
