using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lyman.Models.Requests;
using Lyman.Receivers;
using Lyman.Di;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lyman.Web.Api.Controllers
{
    [Route("api/[controller]")]
    public class DealtTilesController : Controller
    {
        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]DealtTilesRequest request)
        {
            request.Attach();
            var response = DiProvider.GetContainer().GetInstance<DealtTilesReceiver>().Receive(request);
            response.Detach(request.RoomKey);
            return Ok(response);
        }
    }
}
