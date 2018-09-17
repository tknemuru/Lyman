// using System.Collections.Generic;
// using System.Linq;
using System;
namespace Lyman.Models
{
    /// <summary>
    /// 手牌
    /// </summary>
    public static class Hand
    {
        /// <summary>
        /// 長さ
        /// </summary>
        public const int Length = 13;

        /// <summary>
        /// 各風に対して戻り値を持たないメソッドを繰り返し実行します。
        /// </summary>
        /// <param name="action">戻り値を持たないメソッド</param>
        public static void ForEach(Action<int> action)
        {
            for (var i = 0; i < Length; i++)
            {
                action(i);
            }
        }
    }
}
