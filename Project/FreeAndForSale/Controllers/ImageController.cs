using FreeAndForSale.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Cors;

namespace FreeAndForSale.Controllers
{
    public class ImageController : ApiController
    {
        [EnableCors(origins: "http://localhost:55058", headers: "*", methods: "*")]
        [Route("api/addimage/{productid?}")]
        public Task<IEnumerable<string>> Post(int productid)
        {
            var session = HttpContext.Current.Session;

            try
            {
                if (session["username"] != null)
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

                                img.productID = productid;
                                img.photo1 = a;
                                ProductRepository.updateProduct(img);
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
                else
                {
                    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, "Bad Request"));
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return null;
            }

        }

        [Route("api/getimage/{id?}/{no?}")]
        public HttpResponseMessage Get(int id, int no)
        {

            UTDEntities db = new UTDEntities();
            var data = from i in db.products
                       where i.productID == id
                       select i;
            product img = (product)data.SingleOrDefault();
            byte[] imgData = null;
            if (no == 1)
                imgData = img.photo1;
            if (no == 2)
                imgData = img.photo2;
            //AddProduct.byteArrayToImage(imgData);
            HttpResponseMessage response = new HttpResponseMessage();

            //2
            TypeConverter typeConverter = TypeDescriptor.GetConverter(typeof(Bitmap));
            Bitmap bmp = (Bitmap)typeConverter.ConvertFrom(imgData);
            //3'
            var name = id + "" + no;
            var Fs = new FileStream(HostingEnvironment.MapPath("~/uploads") + @"\I" + name.ToString() + ".png", FileMode.Create);
            bmp.Save(Fs, ImageFormat.Png);
            bmp.Dispose();
            //4
            Image img1 = Image.FromStream(Fs);
            Fs.Close();
            Fs.Dispose();
            //5
            MemoryStream ms = new MemoryStream();
            img1.Save(ms, ImageFormat.Png);
            //6
            response.Content = new ByteArrayContent(ms.ToArray());
            ms.Close();
            ms.Dispose();
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            response.StatusCode = HttpStatusCode.OK;
            db.Dispose();
            return response;

        }

    }


}

