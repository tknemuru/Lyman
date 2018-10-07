using System.Collections.Generic;
using System.Linq;
using System;

namespace Lyman.Models
{
    /// <summary>
    /// 風
    /// </summary>
    public static class Wind
    {
        /// <summary>
        /// 風の長さ
        /// </summary>
        public const int Length = 4;

        /// <summary>
        /// インデックス
        /// </summary>
        public enum Index
        {
            /// <summary>
            /// 未確定
            /// </summary>
            Undefined = -1,

            /// <summary>
            /// 東
            /// </summary>
            East,

            /// <summary>
            /// 南
            /// </summary>
            South,

            /// <summary>
            /// 西
            /// </summary>
            West,

            /// <summary>
            /// 北
            /// </summary>
            North,
        }

        /// <summary>
        /// 日本語名称辞書
        /// </summary>
        public static readonly TwoWayDictionary<Index, string> JapaneseName =
            new TwoWayDictionary<Index, string>(
                new[]
                {
                    Index.East,
                    Index.North,
                    Index.South,
                    Index.West,
                },
                new[]
                {
                    "東",
                    "北",
                    "南",
                    "西",
                }
            );

        /// <summary>
        /// インデックスを数値に変換します。
        /// </summary>
        /// <returns>数値</returns>
        /// <param name="index">インデックス</param>
        public static int ToInt(this Index index)
        {
            return (int)index;
        }

        /// <summary>
        /// 各風に対して戻り値を持たないメソッドを繰り返し実行します。
        /// </summary>
        /// <param name="action">戻り値を持たないメソッド</param>
        public static void ForEach(Action<Index> action)
        {
            for (var wind = Index.East; (int)wind < Wind.Length; wind++)
            {
                action(wind);
            }
        }

        /// <summary>
        /// 次の風に移動します。
        /// </summary>
        /// <returns>次の風</returns>
        /// <param name="index">風</param>
        public static Index Next(this Index index)
        {
            return index == Index.North ? Index.East : (Index)(index.ToInt() + 1);
        }

        /// <summary>
        /// 前の風に移動します。
        /// </summary>
        /// <returns>前の風</returns>
        /// <param name="index">風</param>
        public static Index Prev(this Index index)
        {
            return index == Index.East ? Index.North : (Index)(index.ToInt() - 1);
        }
    }
}
