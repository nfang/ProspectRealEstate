using ProspectRealEstate.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProspectRealEstate.Web.Filters
{
    public class NoMembershipAuthorize : AuthorizeAttribute
    {
        // Override the default application logic to avoid using membership service
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var currentUser = httpContext.Session["_CUSR"] as SessionUser;
            if (currentUser != null)
            {
                var mins = (DateTime.Now - currentUser.LoggedOnTime).Minutes;
                if (mins < httpContext.Session.Timeout)
                    return true;
            }
            return false;
        }
    }
}