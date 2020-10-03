// using System.Collections.Generic;
// using System.Linq;
using System;
using Lyman.Di;
using Lyman.Managers;
using Lyman.Models;
using Lyman.Models.Requests;
using Lyman.Models.Responses;

namespace Lyman.Receivers
{
    /// <summary>
    /// 点数計算要求の受信機能を提供します。
    /// </summary>
    public class CalcScoreReceiver : IReceivable<CalcScoreRequest, CalcScoreResponse>
    {
        /// <summary>
        /// 点数計算要求の受信処理を実行します。
        /// </summary>
        /// <returns>点数計算応答</returns>
        /// <param name="request">点数計算要求</param>
        public CalcScoreResponse Receive(CalcScoreRequest request)
        {
            var response = DiProvider.GetContainer().GetInstance<CalcScoreResponse>();
            var room = request.Room;
            response.Room = room;
            return response;
        }
    }
}
