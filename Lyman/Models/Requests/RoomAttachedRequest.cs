// using System.Collections.Generic;
// using System.Linq;
using System;
using Lyman.Managers;

namespace Lyman.Models.Requests
{
    /// <summary>
    /// 部屋が付随した要求
    /// </summary>
    public class RoomAttachedRequest : Request
    {
        /// <summary>
        /// 部屋のキー
        /// </summary>
        /// <value>The room key.</value>
        public Guid RoomKey { get; set; }

        /// <summary>
        /// 部屋
        /// </summary>
        public Room Room { get; set; }

        /// <summary>
        /// 部屋を紐付けます。
        /// </summary>
        public void Attach()
        {
            var room = RoomManager.Get(this.RoomKey);
            this.Room = room;
        }
    }
}
