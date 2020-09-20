using System.Collections.Generic;
// using System.Linq;
using System;

namespace Lyman.Models.Responses
{
    /// <summary>
    /// ロン可能性分析応答
    /// </summary>
    public class RonableAnalyzeResponse : Response
    {
        /// <summary>
        /// ロンできるかどうか
        /// </summary>
        public bool Ronable { get; set; }

        /// <summary>
        /// 役の候補リスト
        /// </summary>
        public List<Yaku> YakuCandidates { get; set; }
    }
}
