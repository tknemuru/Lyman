using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lyman.Di;
using Lyman.Helpers;
using Lyman.Managers;
using Lyman.Models.Requests;
using Lyman.Receivers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lyman.Web.Api.Controllers
{
    [Route("api/[controller]")]
    public class AiDrawController : Controller
    {
        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]DrawRequest request)
        {
            try
            {
                request.Attach();
                request.Position = RoomManager.Get(request.RoomKey).NextPosition;
                request.Wind = RoomManager.Get(request.RoomKey).Turn;
                var response = DiProvider.GetContainer().GetInstance<DrawReceiver>().Receive(request);
                response.Detach(request.RoomKey);
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
