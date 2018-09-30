using System.Collections.Generic;
using System.Linq;
using System;
using Lyman.Helpers;

namespace Lyman.Models
{
    /// <summary>
    /// 河
    /// </summary>
    public static class River
    {
        /// <summary>
        /// 長さ
        /// </summary>
        public const int Length = 21;

        /// <summary>
        /// キーの長さ
        /// </summary>
        public static readonly int KeyLength = IEnumerableHelper.GetEnums<Key>().Count();

        /// <summary>
        /// キー
        /// </summary>
        public enum Key
        {
            /// <summary>
            /// キー名
            /// </summary>
            Name,

            /// <summary>
            /// 風
            /// </summary>
            Wind,
        }

        /// <summary>
        /// 河の各牌に対して戻り値を持たないメソッドを繰り返し実行します。
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
