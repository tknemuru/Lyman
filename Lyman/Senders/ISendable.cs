// using System.Collections.Generic;
// using System.Linq;
using System;
namespace Lyman.Senders
{
    /// <summary>
    /// 送信機能を提供します。
    /// </summary>
    public interface ISendable<in TIn, out TOut>
    {
        /// <summary>
        /// ソースを送信します。
        /// </summary>
        /// <param name="source">ソース</param>
        /// <returns>送信結果</returns>
        TOut Send(TIn source);
    }
}
