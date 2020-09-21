using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Lyman.Di;
using Lyman.Helpers;
using Lyman.Models.Requests;
using Lyman.Receivers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lyman.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuickStartController : ControllerBase
    {
        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]QuickStartRequest request)
        {
            try
            {
                var response = DiProvider.GetContainer().GetInstance<QuickStartReceiver>().Receive(request);
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
