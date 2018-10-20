using System.Collections.Generic;
using System.Linq;
using System;
using Lyman.Helpers;

namespace Lyman.Models
{
    /// <summary>
    /// 手牌
    /// </summary>
    public static class Hand
    {
        /// <summary>
        /// ツモ牌を含んだ長さ
        /// </summary>
        public const int DrawLength = 14;

        /// <summary>
        /// 長さ
        /// </summary>
        public const int Length = 13;

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
    }
}
