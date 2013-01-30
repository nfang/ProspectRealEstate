using ProspectRealEstate.Web.Filters;
using ProspectRealEstate.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProspectRealEstate.Web.Controllers
{
    public class PageController : Controller
    {
        private PageRepository repository = new PageRepository();

        //
        // GET: /Page/

        public ActionResult Index()
        {
            return View();
        }

        [NoMembershipAuthorize]
        public ActionResult Edit(int id)
        {
            var model = repository.All.FirstOrDefault(p => p.ID == id);
            model.Multilingua = repository.FindPageInMultipleLanguages(model.name);
            return View(model);
        }

        [HttpPost, NoMembershipAuthorize, ValidateInput(false)]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var model = repository.All.FirstOrDefault(p => p.ID == id);
            try
            {
                if (model != null)
                {
                    UpdateModel(model, collection);
                    repository.SubmitChanges();
                }
            }
            catch (Exception)
            {
                model.Multilingua = repository.FindPageInMultipleLanguages(model.name);
                return View(model);
            }
            model.Multilingua = repository.FindPageInMultipleLanguages(model.name);
            return View(model);
        }
    }
}
