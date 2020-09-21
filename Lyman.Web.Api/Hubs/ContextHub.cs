// using System.Collections.Generic;
// using System.Linq;
using System;
using System.Threading.Tasks;
using Lyman.Models.Responses;
using Microsoft.AspNetCore.SignalR;

namespace Lyman.Web.Api.Hubs
{
    /// <summary>
    /// プッシュ通知のハブ機能を提供します。
    /// </summary>
    public class ContextHub : Hub
    {
        /// <summary>
        /// 部屋の状態を通知します。
        /// </summary>
        /// <param name="roomContext">部屋の状態</param>
        /// <returns>タスク</returns>
        public Task SendMessage(SelectRoomResponse roomContext)
        {
            return Clients.All.SendAsync("ReceiveMessage", roomContext);
        }
    }
}
