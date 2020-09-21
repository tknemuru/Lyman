// using System.Collections.Generic;
// using System.Linq;
using System;
namespace Lyman.Models.Requests
{
    /// <summary>
    /// コネクションID更新要求
    /// </summary>
    public class UpdateConnectionIdRequest : Request
    {
        /// <summary>
        /// 部屋のキー
        /// </summary>
        /// <value>The room key.</value>
        public Guid RoomKey { get; set; }

        /// <summary>
        /// プレイヤの識別キー
        /// </summary>
        public Guid PlayerKey { get; set; }

        /// <summary>
        /// コネクションID
        /// </summary>
        public string ConnectionId { get; set; }
    }
}
