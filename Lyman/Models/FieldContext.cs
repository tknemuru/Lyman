using System;
using System.Collections.Generic;
using System.Linq;

namespace Lyman.Models
{
    /// <summary>
    /// フィールドの状態
    /// </summary>
    public sealed class FieldContext
    {
        /// <summary>
        /// 手牌
        /// </summary>
        public uint[][] Hands { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FieldContext()
        {
            this.Hands = new uint[Wind.Length][];

            Wind.ForEach((wind) =>
            {
                this.Hands[wind.ToInt()] = new uint[Hand.Length];
            });
        }

        /// <summary>
        /// 指定のオブジェクトが現在のオブジェクトと等しいかどうかを判断します。
        /// </summary>
        /// <param name="obj">比較対象のオブジェクト</param>
        /// <returns>指定のオブジェクトが現在のオブジェクトと等しいかどうか</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }

            var context = (FieldContext)obj;
            var equals = new Dictionary<string, bool>();

            Wind.ForEach((wind) =>
            {
                Hand.ForEach((i) =>
                {
                    equals.Add($"Hands{wind}{i}", this.Hands[wind.ToInt()][i] == context.Hands[wind.ToInt()][i]);
                });
            });

            return equals.All(e => e.Value);
        }

        /// <summary>
        /// 既定のハッシュ関数として機能します。
        /// </summary>
        /// <returns>現在のオブジェクトのハッシュ コード。</returns>
        public override int GetHashCode()
        {
            return this.Hands.GetHashCode();
        }
    }
}
