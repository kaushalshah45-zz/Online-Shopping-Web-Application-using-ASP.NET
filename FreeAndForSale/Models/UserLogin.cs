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
                       
                        select user;
            return query.ToList();
        }

        public static List<user> InsertUser(user e)
        {
            dataContext.users.Add(e);
            dataContext.SaveChanges();
            return GetAllUsers();
        }
    }
}