using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Lyman.Di;
using Lyman.Models.Requests;
using Lyman.Receivers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lyman.Web.Api.Controllers
{
    [Route("api/[controller]")]
    public class QuickStartController : Controller
    {
        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]QuickStartRequest request)
        {
            var response = DiProvider.GetContainer().GetInstance<QuickStartReceiver>().Receive(request);
            //this.ControllerContext.HttpContext.Response.Headers.Add
            //this.HttpContext.Response.Headers.Add("Content-Length")
            //return Ok(new StringContent(JsonConvert.SerializeObject(response), null, "application/json"));
            return Ok(response);
        }
    }
}
