// using System.Collections.Generic;
// using System.Linq;
using System;
using Lyman.Managers;

namespace Lyman.Models.Requests
{
    /// <summary>
    /// プレイヤが付随した要求
    /// </summary>
    public class PlayerAttachedRequest : Request
    {
        /// <summary>
        /// 部屋のキー
        /// </summary>
        /// <value>The room key.</value>
        public Guid RoomKey { get; set; }

        /// <summary>
        /// プレイヤの識別キー
        /// </summary>
        /// <value>The player key.</value>
        public Guid PlayerKey { get; set; }

        /// <summary>
        /// プレイヤ
        /// </summary>
        public Player Player { get; set; }

        /// <summary>
        /// フィールド情報を紐付けます。
        /// </summary>
        public void Attach()
        {
            var room = RoomManager.Get(this.RoomKey);
            var player = room.GetPlayer(this.PlayerKey);
            this.Player = player.Value;
        }
    }
}
