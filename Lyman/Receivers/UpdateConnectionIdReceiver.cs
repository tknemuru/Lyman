// using System.Collections.Generic;
// using System.Linq;
using System;
using Lyman.Di;
using Lyman.Managers;
using Lyman.Models.Requests;
using Lyman.Models.Responses;

namespace Lyman.Receivers
{
    public class UpdateConnectionIdReceiver : IReceivable<UpdateConnectionIdRequest, UpdateConnectionIdResponse>
    {
        /// <summary>
        /// コネクションID更新要求の受信処理を実行します。
        /// </summary>
        /// <returns>コネクションID更新応答</returns>
        /// <param name="request">コネクションID更新要求</param>
        public UpdateConnectionIdResponse Receive(UpdateConnectionIdRequest request)
        {
            var room = RoomManager.Get(request.RoomKey);
            var player = room.GetPlayer(request.PlayerKey);
            player.Value.ConnectionId = request.ConnectionId;
            var response = DiProvider.GetContainer().GetInstance<UpdateConnectionIdResponse>();
            return response;
        }
    }
}
