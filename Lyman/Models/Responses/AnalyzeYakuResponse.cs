using System.Collections.Generic;
// using System.Linq;
using System;

namespace Lyman.Models.Responses
{
    /// <summary>
    /// 役分析応答
    /// </summary>
    public class AnalyzeYakuResponse : Response
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AnalyzeYakuResponse()
        {
            this.HasCompleted = false;
        }

        /// <summary>
        /// 役
        /// </summary>
        public Yaku Yaku { get; set; }

        /// <summary>
        /// 役が成立しているかどうか
        /// </summary>
        public bool HasCompleted { get; set; }
    }
}
