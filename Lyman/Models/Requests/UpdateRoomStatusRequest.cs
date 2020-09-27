// using System.Collections.Generic;
// using System.Linq;
using System;
using Lyman.Managers;

namespace Lyman.Models.Requests
{
    /// <summary>
    /// 部屋状態更新要求
    /// </summary>
    public class UpdateRoomStatusRequest : RoomAttachedRequest
    {
        /// <summary>
        /// 部屋の状態
        /// </summary>
        /// <value>The room key.</value>
        public RoomState RoomState { get; set; }
    }
}
