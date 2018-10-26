using System.Collections.Generic;
using System.Linq;
using System;
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
        /// プレイヤを追加します。
        /// </summary>
        /// <param name="wind">風</param>
        /// <param name="name">プレイヤ名</param>
        public void AddPlayer(Wind.Index wind, string name)
        {
            this.Players.Add(wind, name);
        }
    }
}
