using System.Collections.Generic;
using System.Linq;
using System;
using Lyman.Di;
using Lyman.Models;
using Lyman.Models.Requests;
using Lyman.Models.Responses;
using Lyman.Managers;

namespace Lyman.Receivers
{
    /// <summary>
    /// ロン可能性の分析要求の受信機能を提供します。
    /// </summary>
    public class RonableAnalyzeReceiver : IReceivable<FieldAttachedRequest, bool>
    {
        /// <summary>
        /// 役分析機能リスト
        /// </summary>
        private static List<IYakuAnalyzable> YakuAnalyzers = BuildYakuAnalyzers();

        /// <summary>
        /// ロン可能性分析要求の受信処理を実行します。
        /// </summary>
        /// <returns>ロンできるかどうか</returns>
        /// <param name="request">ロン可能性分析要求</param>
        public bool Receive(FieldAttachedRequest request)
        {
            request.Attach();
            var hand = request.Context.Hands[request.Wind.ToInt()];
            // 手牌に最後の捨牌を追加する
            var room = RoomManager.Get(request.RoomKey);
            var lastPosition = room.LastDiscardPosition;
            if (lastPosition == null)
            {
                return false;
            }
            hand.Add(request.Context.Rivers[lastPosition.Wind.ToInt()][lastPosition.Index]);
            var able = YakuAnalyzers.Any(analy => analy.Receive(hand).HasCompleted.Value);
            return able;
        }

        /// <summary>
        /// 役分析機能のリストを組み立てます。
        /// </summary>
        /// <returns>役分析機能のリスト</returns>
        private static List<IYakuAnalyzable> BuildYakuAnalyzers()
        {
            var analyzers = new List<IYakuAnalyzable>();
            analyzers.Add(DiProvider.GetContainer().GetInstance<AllSimplesAnalyzeReceiver>());
            return analyzers;
        }
    }
}
