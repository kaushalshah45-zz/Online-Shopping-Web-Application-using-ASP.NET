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
             
             var result = AddProduct.InsertProduct(p);
             HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, result);
             return response;
         }

         
    }
}

