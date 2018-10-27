// using System.Collections.Generic;
// using System.Linq;
using System;
namespace Lyman.Models.Requests
{
    /// <summary>
    /// 部屋取得要求
    /// </summary>
    public class SelectRoomRequest : Request
    {
        /// <summary>
        /// 部屋のキー
        /// </summary>
        public Guid RoomKey { get; set; }

        /// <summary>
        /// プレイヤの識別キー
        /// </summary>
        public Guid PlayerKey { get; set; }
    }
}
