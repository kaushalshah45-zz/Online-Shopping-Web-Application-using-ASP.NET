using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreeAndForSale.Models
{
    public class ProductRepository
    {
        private static UTDEntities dataContext = new UTDEntities();

        public static string InsertProduct(product p)
        {
            dataContext.products.Add(p);
            dataContext.SaveChanges();

           

            return "success";
        }

        public static int getProductID(product p)
        {
          
            var userResults = from u in dataContext.products
                              where u.username == p.username
                              && u.productName == p.productName && u.productInfo == p.productInfo && u.price == p.price
                              select u;


            var j1 = dataContext.products.First(a => a.productID == p.productID).productID;
          
            return j1;
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
                pro.photo2 = p.photo1;
            }
            dataContext.SaveChanges();
            return "success";
        }

        public static List<product> GetAllProducts()
        {
            var query = from p in dataContext.products
                        select p;
            return query.ToList();
        }

        public static List<product> SearchProductsByCategory(string category)
        {
            var query = from prod in dataContext.products
                        where prod.category == category
                        select prod;
            return query.ToList();
        }

       

        public static List<product> SearchProducts(string keyword)
        {
            var query = from prod in dataContext.products
                        where prod.productName.Contains(keyword) || prod.productInfo.Contains(keyword) 
                        select prod;
            return query.ToList();
        }
    }
}