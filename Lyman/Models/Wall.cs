// using System.Collections.Generic;
// using System.Linq;
using System;
namespace Lyman.Models
{
    /// <summary>
    /// 山/壁牌(ピーパイ)
    /// </summary>
    public static class Wall
    {
        /// <summary>
        /// 長さ
        /// </summary>
        public const int Length = 17;

        /// <summary>
        /// 段
        /// </summary>
        public enum Rank
        {
            /// <summary>
            /// 下段
            /// </summary>
            Lower,

            /// <summary>
            /// 上段
            /// </summary>
            Upper,
        }

        /// <summary>
        /// 各風の壁に対して戻り値を持たないメソッドを繰り返し実行します。
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
