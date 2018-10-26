// using System.Collections.Generic;
// using System.Linq;
using System;
namespace Lyman.Models.Requests
{
    /// <summary>
    /// 部屋作成要求
    /// </summary>
    public class CreateRoomRequest : Request
    {
        /// <summary>
        /// 部屋名
        /// </summary>
        public string RoomName { get; set; }
    }
}
