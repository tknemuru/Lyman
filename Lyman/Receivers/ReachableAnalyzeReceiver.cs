// using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using Lyman.Di;
using Lyman.Models;
using Lyman.Models.Requests;
using Lyman.Models.Responses;

namespace Lyman.Receivers
{
    /// <summary>
    /// リーチの分析要求の受信機能を提供します。
    /// </summary>
    public class ReachableAnalyzeReceiver : IReceivable<FieldAttachedRequest, ReachableAnalyzeResponse>
    {
        /// <summary>
        /// リーチ分析要求の受信処理を実行します。
        /// </summary>
        /// <returns>リーチ分析応答</returns>
        /// <param name="request">リーチ分析要求</param>
        public ReachableAnalyzeResponse Receive(FieldAttachedRequest request)
        {
            var response = DiProvider.GetContainer().GetInstance<ReachableAnalyzeResponse>();
            response.DiscardCandidates = new List<uint>();
            if (request.Context.Rivers[request.Wind.ToInt()].Count > 0)
            {
                response.DiscardCandidates.Add(request.Context.Rivers[request.Wind.ToInt()].First());
                response.Reachable = false;
            }
            return response;
        }
    }
}
