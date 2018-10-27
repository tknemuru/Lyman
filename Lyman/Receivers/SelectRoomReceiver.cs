using System.Collections.Generic;
using System.Linq;
using System;
using Lyman.Di;
using Lyman.Models;
using Lyman.Managers;
using Lyman.Models.Requests;
using Lyman.Models.Responses;

namespace Lyman.Receivers
{
    /// <summary>
    /// 部屋取得要求の受信機能を提供します。
    /// </summary>
    public class SelectRoomReceiver : IReceivable<SelectRoomRequest, SelectRoomResponse>
    {
        /// <summary>
        /// 部屋取得要求の受信処理を実行します。
        /// </summary>
        /// <returns>部屋取得応答</returns>
        /// <param name="request">部屋取得要求</param>
        public SelectRoomResponse Receive(SelectRoomRequest request)
        {
            var response = DiProvider.GetContainer().GetInstance<SelectRoomResponse>();
            var room = RoomManager.Get(request.RoomKey);
            response.Name = room.Name;
            response.State = room.State.ToString().ToLower();
            response.Players = room.Players.ToDictionary(p => p.Key, p => p.Value.Name);
            response.Rivers = room.Context.Rivers;
            var player = room.GetPlayer(request.PlayerKey);
            response.Hand = room.Context.Hands[player.Key.ToInt()];
            return response;
        }
    }
}
