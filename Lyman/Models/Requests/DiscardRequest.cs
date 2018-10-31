// using System.Collections.Generic;
// using System.Linq;
using System;
namespace Lyman.Models.Requests
{
    /// <summary>
    /// 捨牌要求
    /// </summary>
    public class DiscardRequest : FieldAttachedRequest
    {
        /// <summary>
        /// 捨てる牌
        /// </summary>
        /// <value>The tile.</value>
        public uint Tile { get; set; }
    }
}
