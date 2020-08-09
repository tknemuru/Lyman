// using System.Collections.Generic;
//using System.Linq;
using System;
using System.Collections.Generic;
using Lyman.Di;
using Lyman.Models;
using Lyman.Models.Requests;
using Lyman.Models.Responses;

namespace Lyman.Analyzers
{
    /// <summary>
    /// 分析機能を提供します。
    /// </summary>
    public abstract class Analyzer
    {
        /// <summary>
        /// 分析種別
        /// </summary>
        protected abstract AnalyzeType AnalyzeType { get; }

        /// <summary>
        /// 分析を行います。
        /// </summary>
        /// <param name="request">分析リクエスト</param>
        /// <returns>分析レスポンス</returns>
        public AnalyzeResponse Analyze(AnalyzeRequest request)
        {
            var internalResult = this.InternalAnalyze(request);
            var result = new Dictionary<AnalyzeType, Dictionary<Wind.Index, bool>>();
            result.Add(this.AnalyzeType, internalResult);
            var response = DiProvider.GetContainer().GetInstance<AnalyzeResponse>();
            response.Result = result;
            return response;
        }

        /// <summary>
        /// 分析の内部処理を行います。
        /// </summary>
        /// <param name="request">リクエスト</param>
        /// <returns>レスポンス</returns>
        protected abstract Dictionary<Wind.Index, bool> InternalAnalyze(AnalyzeRequest request);
    }
}
