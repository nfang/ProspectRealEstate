using ProspectRealEstate.Web.Filters;
using ProspectRealEstate.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace ProspectRealEstate.Web.Controllers
{
    [HandleCulture]
    public class ContactUsController : Controller
    {
        private SmtpClient client = new SmtpClient() { EnableSsl = true };

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PostEmail(ContactUsEmail model)
        {
            var to = ConfigurationManager.AppSettings["contactUsMailbox"];

            var emailValidator = new Regex(@"\b[A-Za-z0-9._%-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}\b");
            if (emailValidator.IsMatch(model.From))
            {
                var email = new MailMessage(new MailAddress(model.From), new MailAddress(to));
                email.Subject = "General query";
                email.Body = HttpUtility.HtmlEncode(model.Message + "\r\n" + model.SenderName+ "\r\n" + model.From);
                email.IsBodyHtml = false;
                
                client.Send(email);
                return Content("success");
            }
            return Content("Something terrible has happened.");
        }
    }
}
