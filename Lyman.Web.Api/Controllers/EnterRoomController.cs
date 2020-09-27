using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lyman.Di;
using Lyman.Models.Requests;
using Lyman.Receivers;
using Lyman.Web.Api.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lyman.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnterRoomController : BaseController
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="contextHub">コンテキストハブ</param>
        public EnterRoomController(IHubContext<ContextHub> contextHub) : base(contextHub) { }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]EnterRoomRequest request)
        {
            // 入室
            var response = DiProvider.GetContainer().GetInstance<EnterRoomReceiver>().Receive(request);

            // 部屋の状態を更新
            var stateReq = DiProvider.GetContainer().GetInstance<UpdateRoomStatusRequest>();
            stateReq.RoomKey = request.RoomKey;
            stateReq.Attach();
            var stateRes = DiProvider.GetContainer().GetInstance<UpdateRoomStatusReceiver>().Receive(stateReq);
            stateRes.Detach();

            // 通知
            this.NotifyEnterRoom(request.RoomKey, request.PlayerName);
            this.NotifyRoomContext(request.RoomKey);

            return Ok(response);
        }
    }
}
