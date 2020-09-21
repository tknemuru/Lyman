using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lyman.Di;
using Lyman.Models.Requests;
using Lyman.Receivers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lyman.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchRoomController : ControllerBase
    {
        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]SearchRoomRequest request)
        {
            var responses = DiProvider.GetContainer().GetInstance<SearchRoomReceiver>().Receive(request);
            return Ok(responses);
        }
    }
}
