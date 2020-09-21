using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lyman.Models.Requests;
using Lyman.Models.Responses;
using Lyman.Di;
using Lyman.Ai;
using Lyman.Receivers;
using Lyman.Managers;
using Lyman.Models;
using Microsoft.AspNetCore.SignalR;
using Lyman.Web.Api.Hubs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lyman.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AiDiscardController : BaseController
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="contextHub">コンテキストハブ</param>
        public AiDiscardController(IHubContext<ContextHub> contextHub) : base(contextHub) { }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]AiDiscardRequest request)
        {
            request.Attach();
            request.Wind = RoomManager.Get(request.RoomKey).Turn;
            request.Discardable = DiProvider.GetContainer().GetInstance<DrawnTileDiscardExecutor>();
            var response = DiProvider.GetContainer().GetInstance<AiDiscardReceiver>().Receive(request);
            var room = RoomManager.Get(request.RoomKey);
            room.Turn = room.Turn.Next();
            response.Detach(request.RoomKey);

            // 通知
            this.NotifyRoomContext(request.RoomKey);

            return Ok(response);
        }
    }
}
