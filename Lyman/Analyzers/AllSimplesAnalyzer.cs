// using System.Collections.Generic;
// using System.Linq;
using System;
using System.Collections.Generic;
using Lyman.Models;
using Lyman.Models.Requests;
using Lyman.Models.Responses;

namespace Lyman.Analyzers
{
    /// <summary>
    /// タンヤオの分析機能を提供します。
    /// </summary>
    public class AllSimplesAnalyzer : Analyzer
    {
        /// <summary>
        /// 分析種別
        /// </summary>
        protected override AnalyzeType AnalyzeType { get => AnalyzeType.AllSimples; }

        /// <summary>
        /// リーチの分析を行います。
        /// </summary>
        /// <param name="request">リクエスト</param>
        /// <returns>レスポンス</returns>
        protected override Dictionary<Wind.Index, bool> InternalAnalyze(AnalyzeRequest request)
        {
            var result = new Dictionary<Wind.Index, bool>();
            result.Add(request.Wind, true);
            return result;
        }
    }
}
