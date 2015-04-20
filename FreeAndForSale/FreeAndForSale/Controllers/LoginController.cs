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
    public class LoginController : ApiController
    {
        [EnableCors(origins: "http://localhost:55058", headers: "*", methods: "*")]
        [Route("api/login/{uname?}/{pass?}")]
        public HttpResponseMessage Get(string uname, string pass)
        {
            var q = UserLogin.IsValidUser(uname, pass);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, q);
            return response;
        }

        [Route("api/register")]
        public HttpResponseMessage Post(user u)
        {
            var employees = UserLogin.InsertUser(u);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, employees);
            return response;

        }


    }
}
