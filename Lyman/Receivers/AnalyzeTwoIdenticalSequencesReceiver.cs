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
    /// 一盃口（イーペイコー）の分析機能を提供します。
    /// </summary>
    public class AnalyzeTwoIdenticalSequencesReceiver : IYakuAnalyzable
    {
        /// <summary>
        /// 一盃口（イーペイコー）分析要求の受信処理を実行します。
        /// </summary>
        /// <returns>一盃口（イーペイコー）分析応答</returns>
        /// <param name="request">一盃口（イーペイコー）分析要求</param>
        public AnalyzeYakuResponse Receive(AnalyzeYakuRequest request)
        {
            var response = DiProvider.GetContainer().GetInstance<AnalyzeYakuResponse>();
            var room = request.Room;
            response.Yaku = Yaku.TwoIdenticalSequences;
            var context = request.WinHandsContext;
            // 同じ順子（連番で揃えた面子）が２つある
            var exists = context.Chows
                .GroupBy(c => new { kind = c.GetKind(), num = c.GetNumber() })
                .Select(c => new { Count = c.Count() })
                .Any(c => c.Count == 2);
            response.HasCompleted = exists;
            return response;
        }
    }
}