using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreeAndForSale.Models
{
    public class AddProduct
    {
        private static UTDEntities dataContext = new UTDEntities();

        public static string InsertProduct(product p)
        {
            dataContext.products.Add(p);
            dataContext.SaveChanges();
            return "success";
        }

        public static string updateProduct(product p)
        {
            product pro = dataContext.products.First(i => i.productID == p.productID);
            var p1 = dataContext.products.First(a => a.productID == p.productID).photo1;
            if (p1 == null)
            {
                pro.photo1 = p.photo1;
            }
            else
            {
                pro.photo2 = p.photo2;
            }
            dataContext.SaveChanges();
            return "success";
        }
    }
}