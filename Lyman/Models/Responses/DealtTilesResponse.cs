// using System.Collections.Generic;
// using System.Linq;
using System;
using Lyman.Models.Requests;
namespace Lyman.Models.Responses
{
    /// <summary>
    /// 配牌応答
    /// </summary>
    public class DealtTilesResponse : FieldAttachedResponse
    {
        /// <summary>
        /// 次のツモ位置
        /// </summary>
        public WallPosition NextDrawPosition { get; set; }
    }
}
