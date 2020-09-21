using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lyman.Di;
using Lyman.Helpers;
using Lyman.Managers;
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
    public class DrawController : BaseController
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="contextHub">コンテキストハブ</param>
        public DrawController(IHubContext<ContextHub> contextHub) : base(contextHub) { }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]DrawRequest request)
        {
            try
            {
                // ツモ
                request.Attach();
                request.Position = RoomManager.Get(request.RoomKey).NextPosition;
                var response = DiProvider.GetContainer().GetInstance<DrawReceiver>().Receive(request);

                // ツモで上がれるかの判定
                var analyzeRequest = DiProvider.GetContainer().GetInstance<DrawWinnableAnalyzeRequest>();
                analyzeRequest.ShallowImport(request);
                analyzeRequest.DrawTile = response.Tile;
                var drawWinnableInfo = DiProvider.GetContainer().GetInstance<DrawWinnableAnalyzeReceiver>().Receive(analyzeRequest);
                response.DrawWinnableInfo = drawWinnableInfo;

                RoomManager.Get(request.RoomKey).NextPosition = response.NextPosition;
                response.Detach(request.RoomKey);

                // 通知
                this.NotifyRoomContext(request.RoomKey);

                return Ok(response);
            }
            catch (Exception ex)
            {
                FileHelper.WriteLine(ex.ToString());
                throw ex;
            }
        }
    }
}
