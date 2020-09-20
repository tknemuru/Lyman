using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lyman.Analyzers;
using Lyman.Di;
using Lyman.Models.Requests;
using Lyman.Models.Responses;
using Lyman.Receivers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lyman.Web.Api.Controllers
{
    [Route("api/[controller]")]
    public class SelectRoomController : Controller
    {
        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]SelectRoomRequest request)
        {
            var response = DiProvider.GetContainer().GetInstance<SelectRoomReceiver>().Receive(request);
            this.Analyze(request, response);
            return Ok(response);
        }

        /// <summary>
        /// 分析を行います。
        /// </summary>
        /// <returns>分析結果</returns>
        private void Analyze(SelectRoomRequest request, SelectRoomResponse response)
        {
            var fieldAttachedRequest = DiProvider.GetContainer().GetInstance<FieldAttachedRequest>();
            fieldAttachedRequest.RoomKey = request.RoomKey;
            fieldAttachedRequest.PlayerKey = request.PlayerKey;
            fieldAttachedRequest.Attach();
            response.ReachableInfo = DiProvider.GetContainer().GetInstance<ReachableAnalyzeReceiver>().Receive(fieldAttachedRequest);
            response.RonableInfo = DiProvider.GetContainer().GetInstance<RonableAnalyzeReceiver>().Receive(fieldAttachedRequest);
        }
    }
}
