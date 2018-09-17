using System.Collections.Generic;
using System.Linq;
using System;
using System.Diagnostics;

namespace Lyman.Models
{
    /// <summary>
    /// 双方向辞書
    /// </summary>
    public class TwoWayDictionary<Tkey, TValue>
    {
        /// <summary>
        /// 辞書
        /// </summary>
        private Dictionary<Tkey, TValue> Dic { get; set; }

        /// <summary>
        /// 逆引き辞書
        /// </summary>
        /// <value>The reverse dic.</value>
        private Dictionary<TValue, Tkey> ReverseDic { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="keys">キー要素</param>
        /// <param name="values">値要素</param>
        public TwoWayDictionary(Tkey[] keys, TValue[] values)
        {
            Debug.Assert(keys.Length == values.Length, $"キーと値の要素数は一致させる必要があります。{keys.Length}/{values.Length}");
            this.Dic = Enumerable.Range(0, keys.Length).ToDictionary(i => keys[i], i => values[i]);
            this.ReverseDic = Enumerable.Range(0, values.Length).ToDictionary(i => values[i], i => keys[i]);
        }

        /// <summary>
        /// 値を取得します。
        /// </summary>
        /// <returns>値</returns>
        /// <param name="key">キー</param>
        public TValue Get(Tkey key)
        {
            return this.Dic[key];
        }

        /// <summary>
        /// 逆引きした値を取得します。
        /// </summary>
        /// <returns>値</returns>
        /// <param name="key">キー</param>
        public Tkey Get(TValue key)
        {
            return this.ReverseDic[key];
        }
    }
}
