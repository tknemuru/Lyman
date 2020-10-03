using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lyman.Di;
using Lyman.Helpers;
using Lyman.Managers;
using Lyman.Models;
using Lyman.Models.Requests;
using Lyman.Receivers;
using Lyman.Web.Api.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lyman.Web.Api.Controllers
{
    /// <summary>
    /// ロンController
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class RonController : BaseController
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="contextHub">コンテキストハブ</param>
        public RonController(IHubContext<ContextHub> contextHub) : base(contextHub) { }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]CalcScoreRequest request)
        {
            request.Attach();

            // 最後の捨牌を追加
            var room = request.Room;
            var player = room.GetPlayer(request.PlayerKey);
            room.Context.Hands[player.Wind.ToInt()].Add(room.GetLastDiscardTile());

            // 点数計算
            var response = DiProvider.GetContainer().GetInstance<CalcScoreReceiver>().Receive(request);

            // 進行状況の更新
            ProgressHelper.Update(request.RoomKey, player.Wind);

            // 通知
            this.NotifyRoomContext(request.RoomKey);

            return Ok(response);
        }
    }
}
