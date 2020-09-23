using System.Collections.Generic;
using System.Linq;
using System;
using Lyman.Di;
using Lyman.Models;
using Lyman.Models.Requests;
using Lyman.Models.Responses;
using Lyman.Managers;
using Lyman.Providers;

namespace Lyman.Receivers
{
    /// <summary>
    /// ツモ可能性の分析要求の受信機能を提供します。
    /// </summary>
    public class DrawWinnableAnalyzeReceiver : IReceivable<DrawWinnableAnalyzeRequest, DrawWinnableAnalyzeResponse>
    {
        /// <summary>
        /// ツモ可能性分析要求の受信処理を実行します。
        /// </summary>
        /// <returns>ロンできるかどうか</returns>
        /// <param name="request">ロン可能性分析要求</param>
        public DrawWinnableAnalyzeResponse Receive(DrawWinnableAnalyzeRequest request)
        {
            var response = DiProvider.GetContainer().GetInstance<DrawWinnableAnalyzeResponse>();
            response.YakuCandidates = new List<Yaku>();
            var hand = request.Context.Hands[request.Wind.ToInt()]
                .Select(h => h).ToList();
            // 手牌にツモ牌を追加する
            var drawTile = request.DrawTile;
            if (drawTile == 0)
            {
                response.DrawWinnable = false;
                return response;
            }
            hand.Add(drawTile);
            var ableYakus = DiProvider.GetContainer().GetInstance<YakuAnalyzerProvider>()
                .GetYakuAnalyzers()
                .Select(analy => analy.Receive(hand).HasCompleted)
                .Where(result => result.Value)
                .Select(result => result.Key)
                .ToList();
            response.YakuCandidates = ableYakus;
            response.DrawWinnable = ableYakus.Count > 0;
            return response;
        }
    }
}
