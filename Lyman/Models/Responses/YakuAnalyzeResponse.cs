using System.Collections.Generic;
// using System.Linq;
using System;

namespace Lyman.Models.Responses
{
    /// <summary>
    /// 役分析応答
    /// </summary>
    public class YakuAnalyzeResponse : Response
    {
        /// <summary>
        /// 役が成立しているかどうか
        /// </summary>
        public KeyValuePair<Yaku, bool> HasCompleted { get; set; }
    }
}
