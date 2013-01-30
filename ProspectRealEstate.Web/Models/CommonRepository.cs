using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProspectRealEstate.Web.Models
{
    public class CommonRepository
    {
        private ProspectRealEstateDbContext db = new ProspectRealEstateDbContext();

        public IQueryable<Country> AllCountries()
        {
            return db.Countries.Where(c => c.Language.LanguageName == "en-AU");
        }

        public IQueryable<Suburb> AllSuburbs(State state = null)
        {
            if (state != null)
                return db.Suburbs.Where(sub => sub.state_id == state.ID);

            return db.Suburbs.Where(sub => sub.Language.LanguageName == "en-AU");
        }

        public IQueryable<State> AllStates(Country country = null)
        {
            return db.States.Where(stt => stt.Language.LanguageName == "en-AU");
        }

        public IQueryable<Category> AllCategories()
        {
            return db.Categories.Where(c => c.Language.LanguageName == "en-AU");
        }

        public IQueryable<PropertyType> AllPropertyTypes()
        {
            return from pt in db.PropertyTypes
                   where pt.Language.LanguageName == "en-AU"
                   select pt;
        }
    }
}