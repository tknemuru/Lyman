using System.Collections.Generic;
using System.Linq;
using System;
using Lyman.Di;
using Lyman.Managers;
using Lyman.Models.Requests;
using Lyman.Models.Responses;

namespace Lyman.Receivers
{
    /// <summary>
    /// プレイヤ取得要求の受信機能を提供します。
    /// </summary>
    public class SelectPlayerReceiver : IReceivable<SelectPlayerRequest, SelectPlayerResponse>
    {
        /// <summary>
        /// プレイヤ取得要求の受信処理を実行します。
        /// </summary>
        /// <returns>プレイヤ取得要求</returns>
        /// <param name="request">プレイヤ取得応答</param>
        public SelectPlayerResponse Receive(SelectPlayerRequest request)
        {
            var room = RoomManager.Get(request.RoomKey);
            var response = DiProvider.GetContainer().GetInstance<SelectPlayerResponse>();
            response.Players = room.Players.Select(p => p.Value.Name);
            return response;
        }
    }
}
