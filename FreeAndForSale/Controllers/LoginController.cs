using FreeAndForSale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;


namespace FreeAndForSale.Controllers
{
    public class LoginController : ApiController
    {
        [EnableCors(origins: "http://localhost:55058", headers: "*", methods: "*")]
        [Route("api/login")]

        public HttpResponseMessage Post([FromBody] user logindetails)
        {
            
                var q = UserLogin.IsValidUser(logindetails.username, logindetails.password);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, q);
                return response;
        }

        [Route("api/register")]

        public HttpResponseMessage Put([FromBody] user u)
        {

            var q = UserLogin.InsertUser(u);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "Inserted");
            return response;
        }






    }
}
