using System.Collections.Generic;
using System.Linq;
using System;
using Lyman.Di;
using Lyman.Models.Requests;
using Lyman.Models.Responses;

namespace Lyman.Receivers
{
    /// <summary>
    /// 即時ゲーム開始要求の受信機能を提供します。
    /// </summary>
    public class QuickStartReceiver : IReceivable<QuickStartRequest, QuickStartResponse>
    {
        /// <summary>
        /// 即時ゲーム開始要求の受信処理を実行します。
        /// </summary>
        /// <returns>即時ゲーム開始応答</returns>
        /// <param name="request">即時ゲーム開始要求</param>
        public QuickStartResponse Receive(QuickStartRequest request)
        {
            //var response = DiProvider.GetContainer().GetInstance<QuickStartResponse>();
            //response.CreateRoomResponse = DiProvider.GetContainer().GetInstance<CreateRoomReceiver>().
            //    Receive(request.CreateRoomRequest);
            //var roomKey = response.CreateRoomResponse.RoomKey;
            //response.EnterRoomResponses = request.EnterRoomRequests.Select(req =>
            //{
            //    req.RoomKey = roomKey;
            //    return DiProvider.GetContainer().GetInstance<EnterRoomReceiver>().Receive(req);
            //});
            //var playerKey = response.EnterRoomResponses.First().PlayerKey;
            //request.DealtTilesRequest.RoomKey = roomKey;
            //request.DealtTilesRequest.AttachContext();
            //response.DealtTilesResponse = DiProvider.GetContainer().GetInstance<DealtTilesReceiver>().
            //    Receive(request.DealtTilesRequest);
            //response.DealtTilesResponse.DetachContext(roomKey);
            //request.SelectRoomRequest.RoomKey = roomKey;
            //request.SelectRoomRequest.PlayerKey = playerKey;
            //response.SelectRoomResponse = DiProvider.GetContainer().GetInstance<SelectRoomReceiver>().
            //    Receive(request.SelectRoomRequest);
            //return response;

            var response = DiProvider.GetContainer().GetInstance<QuickStartResponse>();
            var createRoomResponse = DiProvider.GetContainer().GetInstance<CreateRoomReceiver>().
                Receive(request.CreateRoomRequest);
            var roomKey = createRoomResponse.RoomKey;
            var enterRoomResponses = request.EnterRoomRequests.Select(req =>
            {
                req.RoomKey = roomKey;
                return DiProvider.GetContainer().GetInstance<EnterRoomReceiver>().Receive(req);
            });
            var playerKey = enterRoomResponses.First().PlayerKey;
            request.DealtTilesRequest.RoomKey = roomKey;
            request.DealtTilesRequest.AttachContext();
            var dealtTilesResponse = DiProvider.GetContainer().GetInstance<DealtTilesReceiver>().
                Receive(request.DealtTilesRequest);
            dealtTilesResponse.DetachContext(roomKey);
            request.SelectRoomRequest.RoomKey = roomKey;
            request.SelectRoomRequest.PlayerKey = playerKey;
            var selectRoomResponse = DiProvider.GetContainer().GetInstance<SelectRoomReceiver>().
                Receive(request.SelectRoomRequest);
            response.Hand = selectRoomResponse.Hand;
            return response;
        }
    }
}
