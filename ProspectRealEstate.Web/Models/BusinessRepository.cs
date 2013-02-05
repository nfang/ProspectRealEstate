using ProspectRealEstate.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ProspectRealEstate.Web.Models
{
    public class BusinessRepository : IBusinessRepository
    {
        private ProspectRealEstateDbContext db = new ProspectRealEstateDbContext();

        private const string BUSINESS_STATUS_ARCHIVED = @"archived";

        public BusinessRepository() { }

        public Business Find(int id)
        {
            var biz = db.Businesses.SingleOrDefault(b => b.ID == id);

            if (biz == null) return null;

            return biz;
        }

        public Business Find(string code, string lang)
        {
            var biz = db.Businesses
                        .SingleOrDefault(b => b.business_code == code &&
                                         b.Language.LanguageName == lang);

            if (biz == null) return null;

            return biz;
        }

        public IQueryable<Business> All
        {
            get
            {
                return from b in db.Businesses
                       select b;
            }
        }

        public IQueryable<Business> FindFeatured()
        {
            return from b in db.Businesses
                   where b.featured &&
                         (string.IsNullOrEmpty(b.status) || b.status != BUSINESS_STATUS_ARCHIVED)
                   select b;
        }

        public IQueryable<Business> FindLatest()
        {
            return from b in db.Businesses
                   where (string.IsNullOrEmpty(b.status) || b.status != BUSINESS_STATUS_ARCHIVED)
                   orderby b.added_on descending
                   select b;
        }

        public IQueryable<Business> FindBySearchModel(BusinessSearchModel sm)
        {
            if (sm.BusinessLocation == null || sm.BusinessLocation.Count == 0)
                throw new ArgumentException("Suburb must be provided.");

            IQueryable<Business> bs = from b in db.Businesses
                                      where sm.BusinessLocation.Contains(b.suburb_id) &&
                                            (string.IsNullOrEmpty(b.status) || b.status != BUSINESS_STATUS_ARCHIVED)
                                      select b;

            if (sm.Category.HasValue)
            {
                bs = bs.Where(b => b.category_id == sm.Category);
            }

            if (sm.MinAskingPrice.HasValue)
            {
                bs = bs.Where(b => b.asking >= sm.MinAskingPrice);
            }

            if (sm.MaxAskingPrice.HasValue)
            {
                bs = bs.Where(b => b.asking <= sm.MaxAskingPrice);
            }

            return bs;
        }

        public void InsertOrUpdate(Business b)
        {
            if (b.ID == default(int))
            {
                db.Businesses.Add(b);
            }
            else
            {
                db.Entry(b).State = System.Data.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
        }

        public void SubmitChanges()
        {
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}