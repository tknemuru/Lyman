// using System.Collections.Generic;
// using System.Linq;
using System;
using Lyman.Di;

namespace Lyman.Models
{
    /// <summary>
    /// プレイヤ
    /// </summary>
    public sealed class Player
    {
        /// <summary>
        /// 識別キー
        /// </summary>
        /// <value>The key.</value>
        public Guid Key { get; set; }

        /// <summary>
        /// プレイヤ種別
        /// </summary>
        public PlayerType PlayerType { get; set; }

        /// <summary>
        /// プレイヤ名
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// コネクションID
        /// </summary>
        public string ConnectionId { get; set; }

        /// <summary>
        /// ディープコピーを行います。
        /// </summary>
        /// <returns>ディープコピーしたインスタンス</returns>
        public Player DeepCopy()
        {
            var player = DiProvider.GetContainer().GetInstance<Player>();
            player.Key = this.Key;
            player.PlayerType = this.PlayerType;
            player.Name = this.Name;
            player.ConnectionId = this.ConnectionId;
            return player;
        }
    }
}
