// using System.Collections.Generic;
// using System.Linq;
using System;
using System.Threading.Tasks;
using Lyman.Di;
using Lyman.Managers;
using Lyman.Models.Requests;
using Lyman.Receivers;
using Lyman.Web.Api.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Lyman.Web.Api.Controllers
{
    /// <summary>
    /// コントローラの基底機能を提供します。
    /// </summary>
    public abstract class BaseController : ControllerBase
    {
        /// <summary>
        /// ハブコンテキスト
        /// </summary>
        protected IHubContext<ContextHub> ContextHub { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="contextHub">ハブコンテキスト</param>
        public BaseController(IHubContext<ContextHub> contextHub)
        {
            this.ContextHub = contextHub;
        }

        /// <summary>
        /// 部屋の状態を通知します。
        /// </summary>
        /// <param name="roomKey">通知対象のルームキー</param>
        protected void NotifyRoomContext(Guid roomKey)
        {
            var request = DiProvider.GetContainer().GetInstance<SelectRoomRequest>();
            request.RoomKey = roomKey;
            var room = RoomManager.Get(roomKey);
            var players = room.GetConnectedPlayers();
            foreach (var player in players)
            {
                request.PlayerKey = player.Value.Key;
                var response = DiProvider.GetContainer().GetInstance<SelectRoomReceiver>().Receive(request);
                var json = JsonConvert.SerializeObject(
                    response,
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    }
                );
                this.ContextHub.Clients.Client(player.Value.ConnectionId).SendAsync("notifyRoomContext", json);
            }
        }
    }
}
