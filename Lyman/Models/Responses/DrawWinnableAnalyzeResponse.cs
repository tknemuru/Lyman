using System.Collections.Generic;
// using System.Linq;
using System;

namespace Lyman.Models.Responses
{
    /// <summary>
    /// ツモ可能性分析応答
    /// </summary>
    public class DrawWinnableAnalyzeResponse : Response
    {
        /// <summary>
        /// ツモできるかどうか
        /// </summary>
        public bool DrawWinnable { get; set; }

        /// <summary>
        /// 役の候補リスト
        /// </summary>
        public List<Yaku> YakuCandidates { get; set; }
    }
}
