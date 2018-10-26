// using System.Collections.Generic;
// using System.Linq;
using System;
using Lyman.Models.Requests;
namespace Lyman.Models.Responses
{
    /// <summary>
    /// 捨牌応答
    /// </summary>
    public class DiscardResponse : FieldAttachedResponse
    {
        /// <summary>
        /// 捨牌対象の風
        /// </summary>
        /// <value>The wind.</value>
        public Wind.Index Wind { get; set; }

        /// <summary>
        /// 捨てる牌
        /// </summary>
        /// <value>The tile.</value>
        public uint Tile { get; set; }
    }
}
