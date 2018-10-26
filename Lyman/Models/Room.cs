using System.Collections.Generic;
using System.Linq;
using System;
using Lyman.Helpers;
using System.Diagnostics;

namespace Lyman.Models
{
    /// <summary>
    /// 部屋
    /// </summary>
    public class Room
    {
        /// <summary>
        /// 部屋名
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// フィールド状態
        /// </summary>
        /// <value>The context.</value>
        public FieldContext Context { get; set; }

        /// <summary>
        /// プレイヤ
        /// </summary>
        /// <value>The players.</value>
        private Dictionary<Wind.Index, string> Players { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Room()
        {
            this.Players = new Dictionary<Wind.Index, string>();
        }

        /// <summary>
        /// プレイヤを追加します。
        /// </summary>
        /// <param name="wind">風</param>
        /// <param name="name">プレイヤ名</param>
        public void AddPlayer(Wind.Index wind, string name)
        {
            Debug.Assert(wind != Wind.Index.Undefined, "風が不正です。");
            this.Players.Add(wind, name);
        }

        /// <summary>
        /// 使用可能な風のリストを取得します。
        /// </summary>
        /// <returns>使用可能な風のリスト</returns>
        public IEnumerable<Wind.Index> GetAvailableWinds()
        {
            return IEnumerableHelper.GetEnums<Wind.Index>().
                                    Where(w => w != Wind.Index.Undefined).
                                    Except(this.Players.Select(p => p.Key));
        }
    }
}
