// using System.Collections.Generic;
// using System.Linq;
using System;
using Lyman.Ai;

namespace Lyman.Models.Requests
{
    /// <summary>
    /// AI捨牌要求
    /// </summary>
    public class AiDiscardRequest : FieldAttachedRequest
    {
        /// <summary>
        /// 捨牌機能
        /// </summary>
        public IDiscardable Discardable { get; set; }
    }
}
