using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.IO;
using System.Web.UI.WebControls;

namespace FreeAndForSale.Models
{
    public class ImageConvertor
    {
        public static byte [] convert(System.Drawing.Image image) 
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }
    }
}