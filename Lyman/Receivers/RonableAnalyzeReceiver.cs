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
    /// ロン可能性の分析要求の受信機能を提供します。
    /// </summary>
    public class RonableAnalyzeReceiver : IReceivable<FieldAttachedRequest, RonableAnalyzeResponse>
    {
        /// <summary>
        /// ロン可能性分析要求の受信処理を実行します。
        /// </summary>
        /// <returns>ロン可能性分析結果</returns>
        /// <param name="request">ロン可能性分析要求</param>
        public RonableAnalyzeResponse Receive(FieldAttachedRequest request)
        {
            var response = DiProvider.GetContainer().GetInstance<RonableAnalyzeResponse>();
            response.YakuCandidates = new List<Yaku>();
            var hand = request.Context.Hands[request.Wind.ToInt()]
                .Select(h => h).ToList();
            // 手牌に最後の捨牌を追加する
            var room = RoomManager.Get(request.RoomKey);
            var lastPosition = room.LastDiscardPosition;
            if (lastPosition == null)
            {
                response.Ronable = false;
                return response;
            }
            hand.Add(request.Context.GetRiverTile(lastPosition));
            //var ableYakus = DiProvider.GetContainer().GetInstance<YakuAnalyzerProvider>()
            //    .GetYakuAnalyzers()
            //    .Select(analy => analy.Receive(hand).HasCompleted)
            //    .Where(result => result.Value)
            //    .Select(result => result.Key)
            //    .ToList();
            //response.YakuCandidates = ableYakus;
            //response.Ronable = ableYakus.Count > 0;
            return response;
        }
    }
}
