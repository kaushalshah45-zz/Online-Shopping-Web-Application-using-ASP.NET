using FreeAndForSale.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace FreeAndForSale.Controllers
{
    public class ProductController : ApiController
    {
        [EnableCors(origins: "http://localhost:55058", headers: "*", methods: "*")]
        [Route("api/addproduct")]
        public HttpResponseMessage Put([FromBody] product p)
        {
            var session = HttpContext.Current.Session;

            HttpResponseMessage response;
            if (session["username"] != null)
            {
                var result = ProductRepository.InsertProduct(p);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
                var pid = ProductRepository.getProductID(p);
                session["productID"] = pid;
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest);

            }
            return response;

        }

        [Route("api/getproducts")]
        public HttpResponseMessage Post()
        {
            var result = ProductRepository.GetAllProducts();
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }

        [Route("api/getproducts/search/{key?}")]
        public HttpResponseMessage Get(string key)
        {

            var result = ProductRepository.SearchProducts(key);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }

        [Route("api/getproducts/{category?}")]
        public HttpResponseMessage Get(int category)
        {
            var c = "";
            if (category == 1)
                c = "furniture";
            if (category == 2)
                c = "electronics";
            if (category == 3)
                c = "clothing";
            if (category == 4)
                c = "accessories";
            if (category == 5)
                c = "other";

            var result = ProductRepository.SearchProductsByCategory(c);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }

        [Route("api/getproductid")]
        public HttpResponseMessage Get()
        {
            var session = HttpContext.Current.Session;
            var id = session["productID"];
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, id);
            return response;
        }






    }
}

