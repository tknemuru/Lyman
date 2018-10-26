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
    [Route("api/[controller]")]
    public class CreateRoomController : Controller
    {
        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]CreateRoomRequest request)
        {
            var response = DiProvider.GetContainer().GetInstance<CreateRoomReceiver>().Receive(request);
            return Ok(response);
        }
    }
}
