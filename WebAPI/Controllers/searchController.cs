using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class searchController : ApiController
    {
        [HttpGet]
        public IHttpActionResult getResults([FromBody]Request que)
        {

            return Ok("test ok");
        }
    }

    public class Request
    {
        public string[] engines { get; set; }
        public string query { get; set; }
    }



}
