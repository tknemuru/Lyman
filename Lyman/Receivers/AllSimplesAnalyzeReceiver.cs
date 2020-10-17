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
    /// タンヤオの分析機能を提供します。
    /// </summary>
    public class AnalyzeAllSimplesReceiver : IYakuAnalyzable
    {
        /// <summary>
        /// タンヤオ分析要求の受信処理を実行します。
        /// </summary>
        /// <returns>タンヤオ分析応答</returns>
        /// <param name="request">タンヤオ分析要求</param>
        public AnalyzeYakuResponse Receive(AnalyzeYakuRequest request)
        {
            var response = DiProvider.GetContainer().GetInstance<AnalyzeYakuResponse>();
            response.Yaku = Yaku.AllSimples;
            var context = request.WinHandsContext;
            // 雀頭
            if (!IsAllSimpleRange(context.Head.GetNumber()))
            {
                return response;
            }
            // 順子(シュンツ)
            if (context.Chows.Count() > 0)
            {
                var isValidChows = context.Chows
                    .All(t => IsAllSimpleRangeForChows(t.GetNumber()));
                if (!isValidChows)
                {
                    return response;
                }
            }
            // 刻子(コーツ)と槓子(カンツ)
            if (context.PungsAndKongs.Count() > 0)
            {
                var isValidPungs = context.PungsAndKongs
                    .All(t => IsAllSimpleRange(t.GetNumber()));
                if (!isValidPungs)
                {
                    return response;
                }
            }
            response.HasCompleted = true;
            return response;
        }

        /// <summary>
        /// タンヤオの範囲内の数字かどうか
        /// </summary>
        /// <param name="num">数字</param>
        /// <returns>タンヤオの範囲内の数字かどうか</returns>
        private bool IsAllSimpleRange(int num)
        {
            return num >= 2 && num <= 8;
        }

        /// <summary>
        /// 順子(シュンツ)がタンヤオの範囲内の数字かどうか
        /// </summary>
        /// <param name="num">数字</param>
        /// <returns>タンヤオの範囲内の数字かどうか</returns>
        private bool IsAllSimpleRangeForChows(int num)
        {
            return num >= 2 && num <= 6;
        }
    }
}