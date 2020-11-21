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
    /// 七対子（チートイツ）の分析機能を提供します。
    /// </summary>
    public class AnalyzeSevenPairsReceiver : IYakuAnalyzable
    {
        /// <summary>
        /// 七対子（チートイツ）分析要求の受信処理を実行します。
        /// </summary>
        /// <returns>七対子（チートイツ）分析応答</returns>
        /// <param name="request">七対子（チートイツ）分析要求</param>
        public AnalyzeYakuResponse Receive(AnalyzeYakuRequest request)
        {
            var response = DiProvider.GetContainer().GetInstance<AnalyzeYakuResponse>();
            var room = request.Room;
            response.Yaku = Yaku.SevenPairs;
            var context = request.WinHandsContext;
            // 対子が7つある
            response.HasCompleted = context.Pairs.Count() == 7;
            return response;
        }
    }
}