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
    /// 部屋作成要求の受信機能を提供します。
    /// </summary>
    public sealed class CreateRoomReceiver : IReceivable<CreateRoomRequest, CreateRoomResponse>
    {
        /// <summary>
        /// 部屋作成要求の受信処理を実行します。
        /// </summary>
        /// <returns>部屋作成応答</returns>
        /// <param name="request">部屋作成要求</param>
        public CreateRoomResponse Receive(CreateRoomRequest request)
        {
            var key = Guid.NewGuid();
            var room = DiProvider.GetContainer().GetInstance<Room>();
            room.Name = request.RoomName;
            RoomManager.Add(key, room);
            var response = DiProvider.GetContainer().GetInstance<CreateRoomResponse>();
            response.RoomKey = key;
            return response;
        }
    }
}
