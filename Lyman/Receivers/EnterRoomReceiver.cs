using System.Collections.Generic;
using System.Linq;
using System;
using Lyman.Managers;
using Lyman.Models.Requests;
using Lyman.Models.Responses;
using Lyman.Di;

namespace Lyman.Receivers
{
    /// <summary>
    /// 入室要求の受信機能を提供します。
    /// </summary>
    public class EnterRoomReceiver : IReceivable<EnterRoomRequest, EnterRoomResponse>
    {
        /// <summary>
        /// 入室要求の受信処理を実行します。
        /// </summary>
        /// <returns>入室応答</returns>
        /// <param name="request">入室要求</param>
        public EnterRoomResponse Receive(EnterRoomRequest request)
        {
            var room = RoomManager.Get(request.RoomKey);
            var wind = room.GetAvailableWinds().OrderBy(w => Guid.NewGuid()).First();
            room.AddPlayer(wind, request.PlayerName);
            var response = DiProvider.GetContainer().GetInstance<EnterRoomResponse>();
            response.Wind = wind;
            return response;
        }
    }
}
