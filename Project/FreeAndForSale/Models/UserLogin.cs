using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreeAndForSale.Models
{
    public class UserLogin
    {
        private static UTDEntities dataContext = new UTDEntities();
        public static bool IsValidUser(string uname, string pass)
        {

            var userResults = from u in dataContext.users
                              where u.username == uname
                              && u.password == pass
                              select u;
            return Enumerable.Count(userResults) > 0;
        }

        public static List<user> GetAllUsers()
        {
            var query = from user in dataContext.users
                         select new
                         {
                             uname = user.username,
                             fname = user.firstName,
                             lname = user.lastName,
                             phone = user.phoneNumber,
                             type = user.userType,
                             gender = user.sex,
                         };
            List<user> list = query.AsEnumerable()
                          .Select(o => new user
                          {
                              firstName = o.fname,
                              lastName = o.lname,
                              username = o.uname,
                              phoneNumber = o.phone,
                              sex = o.gender,
                              userType = o.type
                          }).ToList();
            return list;
        }

        public static string InsertUser(user e)
        {
            dataContext.users.Add(e);
            dataContext.SaveChanges();
            return "success";
        }

        public static String ChangePassword(String username, String password)
        {
            var result = dataContext.users.SingleOrDefault(b => b.username == username);
            if (result != null)
            {
                result.password = password;
                dataContext.SaveChanges();
            }
            return "Success";

        }
    }
}