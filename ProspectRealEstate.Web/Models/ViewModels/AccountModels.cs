using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProspectRealEstate.Web.Models.ViewModels
{
    public class LogOnModel
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }

    public class SessionUser
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public DateTime LoggedOnTime { get; set; }
    }
}