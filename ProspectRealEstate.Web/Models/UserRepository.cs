using ProspectRealEstate.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace ProspectRealEstate.Web.Models
{
    public class UserRepository
    {
        private ProspectRealEstateDbContext db = new ProspectRealEstateDbContext();

        public object Authenticate(string username, string password)
        {
            using (MD5 md5 = MD5.Create())
            {
                var user = db.Users.SingleOrDefault(u => u.login.Equals(username, StringComparison.InvariantCultureIgnoreCase)
                    && u.pass == password); // StringHelper.VerifyMd5Hash(md5, password, u.pass)
                return user;
            }
        }
    }
}