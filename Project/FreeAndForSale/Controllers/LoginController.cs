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

            var session = HttpContext.Current.Session;
            if (session["username"] == null)
            {
                var q = UserLogin.IsValidUser(logindetails.username, logindetails.password);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, q);
                if (q == true)
                    session["username"] = logindetails.username;
                return response;
            }
            HttpResponseMessage res = Request.CreateResponse(HttpStatusCode.BadRequest);
            return res;

        }

        [Route("api/register")]
        public HttpResponseMessage Put([FromBody] user u)
        {

            var q = UserLogin.InsertUser(u);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "Inserted");
            return response;
        }

        [Route("api/getusers")]
        public HttpResponseMessage Get()
        {
            var query = UserLogin.GetAllUsers();
            HttpResponseMessage res = Request.CreateResponse(HttpStatusCode.OK, query);
            return res;
        }

        [Route("api/signout")]
        public HttpResponseMessage Post()
        {
            var session = HttpContext.Current.Session;
            session["username"] = null;
            HttpResponseMessage res = Request.CreateResponse(HttpStatusCode.OK, "logged out");
            return res;
        }

        [Route("api/getusername")]
        public HttpResponseMessage Put()
        {
            var session = HttpContext.Current.Session;
            var usr = session["username"];
            HttpResponseMessage res = Request.CreateResponse(HttpStatusCode.OK, usr);
            return res;
        }
    }
}
