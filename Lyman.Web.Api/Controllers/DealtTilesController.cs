using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lyman.Models.Requests;
using Lyman.Receivers;
using Lyman.Di;
using Microsoft.AspNetCore.Mvc;
using Lyman.Managers;
using Microsoft.AspNetCore.SignalR;
using Lyman.Web.Api.Hubs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lyman.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DealtTilesController : BaseController
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="contextHub">コンテキストハブ</param>
        public DealtTilesController(IHubContext<ContextHub> contextHub) : base(contextHub) { }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]DealtTilesRequest request)
        {
            request.Attach();
            var response = DiProvider.GetContainer().GetInstance<DealtTilesReceiver>().Receive(request);
            RoomManager.Get(request.RoomKey).NextPosition = response.NextDrawPosition;
            response.Detach(request.RoomKey);

            // 通知
            this.NotifyRoomContext(request.RoomKey);

            return Ok(response);
        }
    }
}
