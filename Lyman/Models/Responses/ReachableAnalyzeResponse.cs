using System.Collections.Generic;
// using System.Linq;
using System;

namespace Lyman.Models.Responses
{
    /// <summary>
    /// リーチ可能性分析応答
    /// </summary>
    public class ReachableAnalyzeResponse : Response
    {
        /// <summary>
        /// リーチできるかどうか
        /// </summary>
        public bool Reachable { get; set; }

        /// <summary>
        /// 捨牌の候補リスト
        /// </summary>
        public List<uint> DiscardCandidates { get; set; }
    }
}
