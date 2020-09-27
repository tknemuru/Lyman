// using System.Collections.Generic;
// using System.Linq;
using System;
using Lyman.Managers;

namespace Lyman.Models.Responses
{
    /// <summary>
    /// 部屋が付随した応答
    /// </summary> 
    public class RoomAttachedResponse : Response
    {
        /// <summary>
        /// 部屋
        /// </summary>
        public Room Room { get; set; }

        /// <summary>
        /// 部屋を引き離します。
        /// </summary>
        public void Detach()
        {
            RoomManager.Set(this.Room.Key, this.Room);
            this.Room = null;
        }
    }
}
