using FreeAndForSale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace FreeAndForSale.Controllers
{
    public class ChangePasswordController : ApiController
    {

        [EnableCors(origins: "http://localhost:55058", headers: "*", methods: "*")]
        [Route("api/changepass")]
        //[RequireHttps]


        public HttpResponseMessage Post([FromBody] user details)
        {

            var q = UserLogin.ChangePassword(details.username, details.password);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, q);
            return response;
        }
    }
}
