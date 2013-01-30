using ProspectRealEstate.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Threading;
using System.Globalization;
using ProspectRealEstate.Web.Filters;
using ProspectRealEstate.Web.Helpers;
using ProspectRealEstate.Web.Models;

namespace ProspectRealEstate.Web.Controllers
{
    [HandleCulture]
    public class HomeController : Controller
    {
        private BusinessRepository bizRepo = new BusinessRepository();
        private PropertyRepository propRepo = new PropertyRepository();
        private PageRepository pageRepo = new PageRepository();
        private CommonRepository commons = new CommonRepository();

        public ActionResult Index()
        {
            var model = new HomeViewModel();

            var latestBusinessModels = from b in bizRepo.FindLatest(2)
                                       where b.Language.LanguageName == CultureInfo.CurrentUICulture.Name
                                       select b;

            model.LatestBusinesses = latestBusinessModels.AsEnumerable().Select(b => b.ToListModel());

            var latestPropertyModels = from p in propRepo.FindLatest(2)
                                       where p.Language.LanguageName == CultureInfo.CurrentUICulture.Name
                                       select p;

            model.LatestProperties = latestPropertyModels.AsEnumerable().Select(p => p.ToListModel());

            var featured = new List<FeaturedModel>();

            var featuredBusinessModels = from b in bizRepo.FindFeatured()
                                         where b.Language.LanguageName == CultureInfo.CurrentUICulture.Name
                                         select b;

            featured.AddRange(featuredBusinessModels.AsEnumerable().Select(b => b.ToListModel()));

            var featuredPropertyModels = from p in propRepo.FindFeatured()
                                         where p.Language.LanguageName == CultureInfo.CurrentUICulture.Name
                                         select p;

            featured.AddRange(featuredPropertyModels.AsEnumerable().Select(p => p.ToListModel()));
            model.FeaturedItems = featured;

            SetViewBag();

            return View(model);
        }

        public ActionResult About()
        {
            return View("Content", pageRepo.FindPageByName("about-us"));
        }

        public ActionResult Finance()
        {
            return View("Content", pageRepo.FindPageByName("finance"));
        }

        public ActionResult Privacy()
        {
            return View("Content", pageRepo.FindPageByName("privacy"));
        }

        public ActionResult ChangeLanguage(string lang, string returnUrl)
        {
            var specifiedCulture = new CultureInfo(lang);
            Thread.CurrentThread.CurrentUICulture = specifiedCulture;

            if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
            {
                var cultureInvarientUrl = StringHelper.GetCultureInvarientRelativeUrl(returnUrl);
                return Redirect(String.Format("/{0}/{1}", lang, cultureInvarientUrl));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private void SetViewBag()
        {
            // Set for location drop down list
            ViewBag.Suburbs = new MultiSelectList(commons.AllSuburbs(), "ID", "name");
            ViewBag.States = new SelectList(commons.AllStates(), "ID", "state_name");
            ViewBag.Countries = new SelectList(commons.AllCountries(), "ID", "common_name");

            // Set for category drop down list
            ViewBag.Categories = new SelectList(commons.AllCategories(), "ID", "name");

            // Set for property type drop down list
            ViewBag.PropertyTypes = new MultiSelectList(commons.AllPropertyTypes(), "ID", "name");

            var rentOrSale = new List<RentOrSaleModel>()
            {
                new RentOrSaleModel() { Literal = "Rent", Value = "rent" },
                new RentOrSaleModel() { Literal = "Sale", Value = "sale" }
            };
            ViewBag.RentOrSale = new SelectList(rentOrSale, "Value", "Literal");
        }
    }
}
