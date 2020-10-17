using System.Collections.Generic;
using System.Linq;
using System;
namespace Lyman.Helpers
{
    /// <summary>
    /// 計算に関する補助機能を提供します。
    /// </summary>
    public static class MathHelper
    {
        /// <summary>
        /// 組み合わせを作成します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">要素リスト</param>
        /// <param name="r">項数</param>
        /// <returns>組み合わせリスト</returns>
        public static IEnumerable<IEnumerable<T>> Combination<T>(IEnumerable<T> items, int r)
        {
            if (r == 0)
            {
                yield return Enumerable.Empty<T>();
            }
            else
            {
                var i = 1;
                foreach (var x in items)
                {
                    var xs = items.Skip(i);
                    foreach (var c in Combination(xs, r - 1))
                        yield return Before(c, x);
                    i++;
                }
            }
        }

        /// <summary>
        /// 指定した要素を先頭に追加します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">要素リスト</param>
        /// <param name="first">先頭に追加する要素</param>
        /// <returns>指定した要素を先頭に追加したリスト</returns>
        private static IEnumerable<T> Before<T>(IEnumerable<T> items, T first)
        {
            yield return first;

            foreach (var i in items)
                yield return i;
        }
    }
}
