// using System.Collections.Generic;
// using System.Linq;
using System;
namespace Lyman.Models.Requests
{
    /// <summary>
    /// 入室要求
    /// </summary>
    public class EnterRoomRequest : Request
    {
        /// <summary>
        /// 部屋のキー
        /// </summary>
        /// <value>The room key.</value>
        public Guid RoomKey { get; set; }

        /// <summary>
        /// プレイヤ種別
        /// </summary>
        public PlayerType PlayerType { get; set; }

        /// <summary>
        /// プレイヤ名
        /// </summary>
        public string PlayerName { get; set; }

        /// <summary>
        /// コネクションID
        /// </summary>
        public string ConnectionId { get; set; }
    }
}
