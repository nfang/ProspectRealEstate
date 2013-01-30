using ProspectRealEstate.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ProspectRealEstate.Web.Models
{
    public class PageRepository
    {
        private ProspectRealEstateDbContext db = new ProspectRealEstateDbContext();

        public IQueryable<Page> All
        {
            get { return db.Pages; }
        }

        public IEnumerable<PageContent> FindAllPages(string lang = "")
        {
            if (string.IsNullOrEmpty(lang))
                return db.Pages.Select(page => AssembleViewModel(page));

            return db.Pages.Where(page => page.Language.LanguageName.Equals(lang, StringComparison.InvariantCultureIgnoreCase))
                                .Select(page => AssembleViewModel(page));
        }

        public PageContent FindPageByName(string name)
        {
            var lang = CultureInfo.CurrentUICulture.Name;

            var pages = db.Pages.Where(page => page.name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

            if (string.IsNullOrEmpty(lang))
                return AssembleViewModel(pages.FirstOrDefault());

            return AssembleViewModel(pages.FirstOrDefault(
                page => page.Language.LanguageName.Equals(lang, StringComparison.InvariantCultureIgnoreCase)));
        }

        public IEnumerable<PageInOtherLanguagesModel> FindPageInMultipleLanguages(string name)
        {
            var pages = db.Pages.Where(p => p.name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            foreach (var item in pages)
            {
                yield return new PageInOtherLanguagesModel()
                {
                    ID = item.ID,
                    Language = item.Language
                };
            }
        }

        private PageContent AssembleViewModel(Page page)
        {
            if (page == null) return null;

            return new PageContent()
            {
                ID = page.ID,
                Title = page.Title,
                Content = page.Content,
                AddedOn = page.added_on,
                ModifiedOn = page.modified_on.HasValue ? page.modified_on.Value : page.added_on
            };
        }

        public void SubmitChanges()
        {
            db.SaveChanges();
        }
    }
}