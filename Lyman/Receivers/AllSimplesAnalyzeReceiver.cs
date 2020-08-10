// using System.Linq;
using System;
using System.Collections.Generic;
using Lyman.Di;
using Lyman.Models;
using Lyman.Models.Requests;
using Lyman.Models.Responses;

namespace Lyman.Receivers
{
    /// <summary>
    /// タンヤオの分析機能を提供します。
    /// </summary>
    public class AllSimplesAnalyzeReceiver : IYakuAnalyzable
    {
        /// <summary>
        /// タンヤオ分析要求の受信処理を実行します。
        /// </summary>
        /// <returns>タンヤオ分析応答</returns>
        /// <param name="request">タンヤオ分析要求</param>
        public YakuAnalyzeResponse Receive(List<uint> hand)
        {
            var response = DiProvider.GetContainer().GetInstance<YakuAnalyzeResponse>();
            response.HasCompleted = new KeyValuePair<Yaku, bool>(Yaku.AllSimples, true);
            return response;
        }
    }
}