using ProspectRealEstate.Web.Models;
using ProspectRealEstate.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Globalization;
using ProspectRealEstate.Web.Filters;
using System.IO;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace ProspectRealEstate.Web.Controllers
{
    [Serializable]
    public class SetArchiveViewModel
    {
        public int ID { get; set; }
    }

    [HandleCulture]
    public class BusinessController : Controller
    {
        IBusinessRepository repository;
        CommonRepository commons;
        AgentRepository agentRepository;

        private SmtpClient client = new SmtpClient();

        private const int PAGE_SIZE = 10;

        public BusinessController()
        {
            repository = new BusinessRepository();
            commons = new CommonRepository();
            agentRepository = new AgentRepository();
        }

        public ActionResult Index(int? page, string sortOrder)
        {
            return RedirectToAction("List", new { page = page, sortOrder = sortOrder });
        }

        public ActionResult List(int? page, string sortOrder)
        {
            ViewBag.SortParam = sortOrder;
            SetSearchViewBag();
            var models = from m in repository.All
                         where m.Language.LanguageName == CultureInfo.CurrentUICulture.Name
                         select m;

            switch (sortOrder)
            {
                case "hp":
                    models = models.OrderByDescending(b => b.asking);
                    break;
                case "lp":
                    models = models.OrderBy(b => b.asking);
                    break;
                default:
                    models = models.OrderByDescending(b => b.added_on);
                    break;
            }

            var listModels = models.AsEnumerable().Select(b => b.ToListModel());
            var pageNumber = page ?? 1;
            return View("Index", listModels.ToPagedList(pageNumber, PAGE_SIZE));
        }

        public ActionResult Search(BusinessSearchModel sm, int? page, string sortOrder)
        {
            ViewBag.SortParam = sortOrder;
            SetSearchViewBag();
            var models = from m in repository.FindBySearchModel(sm)
                         where m.Language.LanguageName == CultureInfo.CurrentUICulture.Name
                         select m;

            switch (sortOrder)
            {
                case "hp":
                    models = models.OrderByDescending(b => b.asking);
                    break;
                case "lp":
                    models = models.OrderBy(b => b.asking);
                    break;
                default:
                    models = models.OrderByDescending(b => b.added_on);
                    break;
            }

            var listModels = models.AsEnumerable().Select(b => b.ToListModel());
            var pageNumber = page ?? 1;
            return View("Index", listModels.ToPagedList(pageNumber, PAGE_SIZE));
        }

        public ActionResult Detail(int id)
        {
            var model = repository.Find(id);
            return View(model);
        }

        [NoMembershipAuthorize]
        public ActionResult Admin(int? page, string sortOrder, string language)
        {
            var all = repository.All;

            // Apply language filter
            if (language != "all" && !String.IsNullOrEmpty(language))
            {
                if (language == "en")
                    all = all.Where(b => b.Language.LanguageName == "en-AU");
                else
                    all = all.Where(b => b.Language.LanguageName == "zh-CN");
            }

            // Apply sorter
            ViewBag.TitleSortParam = String.IsNullOrEmpty(sortOrder) ? "td" : "";
            ViewBag.DateSortParam = sortOrder == "da" ? "dd" : "da";
            switch (sortOrder)
            {
                case "td":
                    all = all.OrderByDescending(m => m.title);
                    break;
                case "da":
                    all = all.OrderBy(m => m.added_on);
                    break;
                case "dd":
                    all = all.OrderByDescending(m => m.added_on);
                    break;
                default:
                    all = all.OrderBy(m => m.title);
                    break;
            }

            var models = all.AsEnumerable().Select(b => b.ToListModel());
            var pageNumber = page ?? 1;
            return View("AdminIndex", models.ToPagedList(pageNumber, PAGE_SIZE));
        }

        [NoMembershipAuthorize]
        public ActionResult Create()
        {
            var model = new Business();
            SetViewBag(model);
            return View(model);
        }

        [HttpPost, NoMembershipAuthorize]
        public ActionResult Create(Business model, IEnumerable<HttpPostedFileBase> attachments)
        {
            var cusr = Session["_CUSR"] as SessionUser;
            if (cusr == null) throw new ArgumentException("SessionUser");

            model.added_by = (byte)cusr.ID;
            model.added_on = DateTime.Now;

            try
            {
                if (attachments != null)
                {
                    foreach (var item in attachments)
                    {
                        if (item != null && item.ContentLength > 0)
                        {
                            var filename = Path.GetFileName(item.FileName).ToLower();
                            var dirpath = "~/Uploads/Business/" + GetFolderName(model);
                            var directory = Server.MapPath(dirpath);

                            if (!Directory.Exists(directory))
                                Directory.CreateDirectory(directory);

                            string path = Path.Combine(directory, filename);
                            string relPath = Path.Combine(dirpath, filename);
                            item.SaveAs(path);

                            model.Attachments.Add(new ProspectRealEstate.Web.Models.Attachment()
                            {
                                added_by = (byte)cusr.ID,
                                added_on = DateTime.Now,
                                Business = model,
                                file_location = relPath,
                                type = "image"
                            });
                        }
                    }
                }

                ModelState.Remove("Attachments");

                if (ModelState.IsValid)
                {
                    repository.InsertOrUpdate(model);
                    repository.SubmitChanges();
                    return RedirectToAction("Edit", new { id = model.ID });
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [NoMembershipAuthorize]
        public ActionResult Edit(int id)
        {
            var model = repository.Find(id);
            SetViewBag(model);
            return View(model);
        }

        [HttpPost, NoMembershipAuthorize]
        public ActionResult Edit(int id, IEnumerable<HttpPostedFileBase> attachments, FormCollection collection)
        {
            var cusr = Session["_CUSR"] as SessionUser;

            var model = repository.Find(id);
            try
            {
                if (attachments != null)
                {
                    foreach (var item in attachments)
                    {
                        if (item != null && item.ContentLength > 0)
                        {
                            var filename = Path.GetFileName(item.FileName).ToLower();
                            var dirpath = "~/Uploads/Business/" + GetFolderName(model);
                            var directory = Server.MapPath(dirpath);

                            if (!Directory.Exists(directory))
                                Directory.CreateDirectory(directory);

                            string path = Path.Combine(directory, filename);
                            string relPath = Path.Combine(dirpath, filename);
                            item.SaveAs(path);

                            var numOfAttachments = model.Attachments.Count(a => a.file_location == relPath);
                            if (numOfAttachments > 0)
                            {
                                var ex = model.Attachments.FirstOrDefault(a => a.file_location == relPath);
                                ex.modified_by = (byte)cusr.ID;
                                ex.modified_on = DateTime.Today;
                            }
                            else
                            {
                                model.Attachments.Add(new ProspectRealEstate.Web.Models.Attachment()
                                {
                                    added_by = (byte)cusr.ID,
                                    added_on = DateTime.Now,
                                    Business = model,
                                    file_location = relPath,
                                    type = "image"
                                });
                            }
                        }
                    }
                }
                model.modified_by = (byte)cusr.ID;
                model.modified_on = DateTime.Now;

                UpdateModel(model, collection);
                repository.SubmitChanges();
            }
            catch (Exception)
            {
                SetViewBag(model);
                return View(model);
            }
            SetViewBag(model);
            return View(model);
        }

        [HttpPost, NoMembershipAuthorize]
        public ActionResult SetArchive(SetArchiveViewModel model)
        {
            var business = repository.Find(model.ID);
            if (business != null)
            {
                try
                {
                    business.status = "archived";
                    repository.SubmitChanges();
                    return Content("success");
                }
                catch (Exception)
                {
                    return Content("Something terrible happened");
                }
            }
            else
            {
                return Content("Model not found");
            }
        }

        [HttpPost, NoMembershipAuthorize]
        public ActionResult SetActive(SetArchiveViewModel model)
        {
            var business = repository.Find(model.ID);
            if (business != null)
            {
                try
                {
                    business.status = "Active";
                    repository.SubmitChanges();
                    return Content("success");
                }
                catch (Exception)
                {
                    return Content("Something terrible happened");
                }
            }
            else
            {
                return Content("Model not found");
            }
        }

        [HttpPost, NoMembershipAuthorize]
        public ActionResult RemoveAttachment(int attachmentId, int businessId)
        {
            try
            {
                var business = repository.Find(businessId);
                if (business != null)
                {
                    var toRemove = business.Attachments.FirstOrDefault(a => a.ID == attachmentId);
                    if (toRemove != null)
                        business.Attachments.Remove(toRemove);
                    repository.SubmitChanges();
                }
                return Content("success");
            }
            catch (Exception)
            {
                return Content("failed");
            }
        }

        [HttpPost]
        public ActionResult SendQuery(SendQueryModel model)
        {
            var emailValidator = new Regex(@"\b[A-Za-z0-9._%-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}\b");
            if (emailValidator.IsMatch(model.UserEmail))
            {
                try
                {
                    var email = new MailMessage(new MailAddress(model.UserEmail), new MailAddress(model.AgentEmail));
                    email.Subject = model.Subject;
                    email.Body = HttpUtility.HtmlEncode(model.Message + "\r\n" + model.UserName);
                    email.IsBodyHtml = false;

                    client.Send(email);
                    return Content("success");
                }
                catch (Exception ex)
                {
                    return Content("error: " + ex.Message);
                }
            }
            return Content("Invalid email address.");
        }

        private void SetViewBag(Business model = null)
        {
            if (model == null)
            {
                this.SetLocationsViewBag(null, null, null);
                this.SetCategoriesViewBag(null);
                this.SetAgentsViewBag(null);
            }
            else
            {
                this.SetLocationsViewBag(model.suburb_id, model.state_id, model.country_id);
                this.SetCategoriesViewBag(model.category_id);
                this.SetAgentsViewBag(model.agent_id);
            }
        }

        private void SetSearchViewBag()
        {
            // Set for location drop down list
            ViewBag.Suburbs = new MultiSelectList(commons.AllSuburbs(), "ID", "name");
            ViewBag.States = new SelectList(commons.AllStates(), "ID", "state_name");
            ViewBag.Countries = new SelectList(commons.AllCountries(), "ID", "common_name");

            // Set for category drop down list
            ViewBag.Categories = new SelectList(commons.AllCategories(), "ID", "name");

            // Set for property type drop down list
            ViewBag.PropertyTypes = new MultiSelectList(commons.AllPropertyTypes(), "ID", "name");
        }

        private void SetLocationsViewBag(short? suburbId, short? stateId, short? countryId)
        {
            if (suburbId == null)
                ViewBag.Suburbs = new SelectList(commons.AllSuburbs(), "ID", "name");
            else
                ViewBag.Suburbs = new SelectList(commons.AllSuburbs(), "ID", "name", suburbId);

            if (stateId == null)
                ViewBag.States = new SelectList(commons.AllStates(), "ID", "state_name");
            else
                ViewBag.States = new SelectList(commons.AllStates(), "ID", "state_name", stateId);

            if (countryId == null)
                ViewBag.Countries = new SelectList(commons.AllCountries(), "ID", "common_name");
            else
                ViewBag.Countries = new SelectList(commons.AllCountries(), "ID", "common_name", countryId);
        }

        private void SetAgentsViewBag(short? agentId)
        {
            if (agentId == null)
                ViewBag.Agents = new SelectList(agentRepository.All, "ID", "name");
            else
                ViewBag.Agents = new SelectList(agentRepository.All, "ID", "name", agentId);
        }

        private void SetCategoriesViewBag(short? categoryId)
        {
            if (categoryId == null)
                ViewBag.Categories = new SelectList(commons.AllCategories(), "ID", "name");
            else
                ViewBag.Categories = new SelectList(commons.AllCategories(), "ID", "name", categoryId);
        }

        private string GetFolderName(Business model)
        {
            return model.business_code;
        }
    }
}
