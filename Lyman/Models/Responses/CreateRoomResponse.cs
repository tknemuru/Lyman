// using System.Collections.Generic;
// using System.Linq;
using System;
namespace Lyman.Models.Responses
{
    /// <summary>
    /// 部屋作成応答
    /// </summary>
    public class CreateRoomResponse : Response
    {
        /// <summary>
        /// 部屋のキー
        /// </summary>
        public Guid RoomKey { get; set; }
    }
}
