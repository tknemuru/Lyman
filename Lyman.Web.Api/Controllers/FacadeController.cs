using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lyman.Di;
using Lyman.Models.Requests;
using Lyman.Models.Responses;
using Lyman.Receivers;
using Microsoft.AspNetCore.Mvc;

namespace Lyman.Web.Api.Controllers
{
    [Route("api")]
    public class FacadeController : Controller
    {
        // GET: api/values
        [HttpGet]
        public DealtTilesResponse Get()
        {
            var receiver = DiProvider.GetContainer().GetInstance<DealtTilesReceiver>();
            var request = DiProvider.GetContainer().GetInstance<DealtTilesRequest>();
            var response = receiver.Receive(request);
            return response;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
