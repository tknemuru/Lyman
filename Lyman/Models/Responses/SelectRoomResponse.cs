using System.Collections.Generic;
using System.Linq;
using System;
namespace Lyman.Models.Responses
{
    /// <summary>
    /// 部屋取得応答
    /// </summary>
    public class SelectRoomResponse : Response
    {
        /// <summary>
        /// 部屋名
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// 部屋の状態
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// プレイヤ
        /// </summary>
        public Dictionary<Wind.Index, string> Players { get; set; }

        /// <summary>
        /// 手牌
        /// </summary>
        /// <value>The hand.</value>
        public IEnumerable<uint> Hand { get; set; }

        /// <summary>
        /// 河牌
        /// </summary>
        public IEnumerable<IEnumerable<uint>> Rivers { get; set; }
    }
}
