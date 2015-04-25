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
    public class ImageController : ApiController
    {
        [EnableCors(origins: "http://localhost:55058", headers: "*", methods: "*")]
        [Route("api/addproduct/addimage")]
        public Task<IEnumerable<string>> Put()
        {
            try
            {
                if (Request.Content.IsMimeMultipartContent())
                {
                    string fullPath = HttpContext.Current.Server.MapPath("~/uploads");
                    MultipartFormDataStreamProvider streamProvider = new MultipartFormDataStreamProvider(fullPath);
                    var task = Request.Content.ReadAsMultipartAsync(streamProvider).ContinueWith(t =>
                    {
                        if (t.IsFaulted || t.IsCanceled)
                            throw new HttpResponseException(HttpStatusCode.InternalServerError);
                        var fileInfo = streamProvider.FileData.Select(i =>
                        {
                            var info = new FileInfo(i.LocalFileName);
                            product img = new product();
                            byte[] a = File.ReadAllBytes(info.FullName);
                            img.productID = 1;
                            img.photo1 = a;

                            AddProduct.updateProduct(img);



                            return "File uploaded successfully!";
                        });
                        return fileInfo;
                    });
                    return task;
                }
                else
                {
                    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "Invalid Request!"));
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return null;
            }
        }

        
    }
}
