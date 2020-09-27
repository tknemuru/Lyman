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
    /// 部屋状態更新要求の受信機能を提供します。
    /// </summary>
    public class UpdateRoomStatusReceiver : IReceivable<UpdateRoomStatusRequest, RoomAttachedResponse>
    {
        /// <summary>
        /// 部屋状態更新要求の受信処理を実行します。
        /// </summary>
        /// <returns>部屋状態更新応答</returns>
        /// <param name="request">部屋状態更新要求</param>
        public RoomAttachedResponse Receive(UpdateRoomStatusRequest request)
        {
            var response = DiProvider.GetContainer().GetInstance<RoomAttachedResponse>();
            var room = request.Room;
            response.Room = room;

            // 指定がある場合は指定を優先する
            if (request.RoomState != RoomState.Undefined)
            {
                room.State = request.RoomState;
                return response;
            }

            if (room.Players.Count < Wind.Length)
            {
                room.State = RoomState.Entering;
            }
            else
            {
                room.State = RoomState.Entered;
            }
            return response;
        }
    }
}
