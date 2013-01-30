using ProspectRealEstate.Web.Helpers;
using ProspectRealEstate.Web.Models;
using ProspectRealEstate.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProspectRealEstate.Web.Controllers
{
    public class AccountController : Controller
    {
        private UserRepository repository = new UserRepository();

        //
        // GET: /Admin/LogOn

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model)
        {
            if (string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password))
                throw new ArgumentNullException("Username or password");

            var ret = repository.Authenticate(model.UserName, model.Password);

            if (ret != null)
            {
                var user = ret as User;

                // Set session cusr;
                Session["_CUSR"] = new SessionUser()
                {
                    LoggedOnTime = DateTime.Now,
                    Username = user.login,
                    ID = user.ID
                };
                return RedirectToAction("Admin", "Business", new { lang = "en-AU" });
            }
            else
            {
                return View("LogOn");
            }
        }
    }
}
