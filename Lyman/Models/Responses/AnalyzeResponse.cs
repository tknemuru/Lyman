using System.Collections.Generic;
// using System.Linq;
using System;
using Lyman.Analyzers;

namespace Lyman.Models.Responses
{
    /// <summary>
    /// 分析応答
    /// </summary>
    public class AnalyzeResponse : Response
    {
        /// <summary>
        /// 分析結果
        /// </summary>
        public Dictionary<AnalyzeType, Dictionary<Wind.Index, bool>> Result { get; set; }
    }
}
