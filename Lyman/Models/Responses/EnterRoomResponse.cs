// using System.Collections.Generic;
// using System.Linq;
using System;
namespace Lyman.Models.Responses
{
    /// <summary>
    /// 入室応答
    /// </summary>
    public class EnterRoomResponse : Response
    {
        /// <summary>
        /// 風
        /// </summary>
        /// <value>The wind.</value>
        public Wind.Index WindIndex { get; set; }

        /// <summary>
        /// 風（日本語名）
        /// </summary>
        /// <value>The wind.</value>
        public string Wind { get; set; }

        /// <summary>
        /// プレイヤの識別キー
        /// </summary>
        /// <value>The player key.</value>
        public Guid PlayerKey { get; set; }
    }
}
