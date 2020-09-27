// using System.Collections.Generic;
// using System.Linq;
using System;
using Lyman.Di;
using Lyman.Managers;
using Lyman.Models.Requests;
using Lyman.Models.Responses;

namespace Lyman.Receivers
{
    public class UpdateConnectionIdReceiver : IReceivable<UpdateConnectionIdRequest, PlayerAttachedResponse>
    {
        /// <summary>
        /// コネクションID更新要求の受信処理を実行します。
        /// </summary>
        /// <returns>コネクションID更新応答</returns>
        /// <param name="request">コネクションID更新要求</param>
        public PlayerAttachedResponse Receive(UpdateConnectionIdRequest request)
        {
            var response = DiProvider.GetContainer().GetInstance<PlayerAttachedResponse>();
            var player = request.Player;
            player.ConnectionId = request.ConnectionId;
            response.Player = player;
            return response;
        }
    }
}
