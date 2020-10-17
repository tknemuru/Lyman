using System.Collections.Generic;
// using System.Linq;
using System;

namespace Lyman.Models.Responses
{
    /// <summary>
    /// 面子分析応答
    /// </summary>
    public class TileSetAnalyzeResponse : Response
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
