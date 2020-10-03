// using System.Collections.Generic;
// using System.Linq;
using System;
using Lyman.Di;
using Lyman.Managers;
using Lyman.Models;
using Lyman.Models.Requests;
using Lyman.Receivers;

namespace Lyman.Helpers
{
    /// <summary>
    /// 進行状況に関する補助機能を提供します。
    /// </summary>
    public static class ProgressHelper
    {
        /// <summary>
        /// 進行状況をアップデートします。
        /// </summary>
        /// <param name="roomKey">部屋キー</param>
        public static void Update(Guid roomKey)
        {
            Update(roomKey, Wind.Index.Undefined);
        }

        /// <summary>
        /// 進行状況をアップデートします。
        /// </summary>
        /// <param name="roomKey">部屋キー</param>
        /// <param name="winIndex">あがったプレイヤの風</param>
        public static void Update(Guid roomKey, Wind.Index winIndex)
        {
            var room = RoomManager.Get(roomKey);
            var fieldAttachedRequest = DiProvider.GetContainer().GetInstance<FieldAttachedRequest>();
            fieldAttachedRequest.RoomKey = roomKey;
            fieldAttachedRequest.PlayerKey = room.GetParentPlayer().Key;
            fieldAttachedRequest.Attach();
            var reachableInfo = DiProvider.GetContainer().GetInstance<ReachableAnalyzeReceiver>().Receive(fieldAttachedRequest);
            var updateReq = DiProvider.GetContainer().GetInstance<UpdateGameProgressRequest>();
            updateReq.RoomKey = roomKey;
            updateReq.ParentWaitingHand = reachableInfo.Reachable;
            updateReq.WinWind = winIndex;
            updateReq.Attach();
            var updateRes = DiProvider.GetContainer().GetInstance<UpdateGameProgressReceiver>().Receive(updateReq);
            updateRes.Detach();
        }
    }
}
