using ProspectRealEstate.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ProspectRealEstate.Web.Models
{
    public class PropertyRepository : IPropertyRepository
    {
        private ProspectRealEstateDbContext db = new ProspectRealEstateDbContext();

        private const string PROPERTY_STATUS_ARCHIVED = "archived";

        public PropertyRepository() { }

        public Property Find(int id)
        {
            var property = db.Properties.SingleOrDefault(p => p.ID == id);

            if (property == null) return null;

            return property;
        }

        public Property Find(string code)
        {
            var property = db.Properties.SingleOrDefault(p => p.property_code == code &&
                                                              p.Language.LanguageName == CultureInfo.CurrentUICulture.Name);

            if (property == null) return null;

            return property;
        }

        public IQueryable<Property> All
        {
            get 
            {
                return from p in db.Properties
                       select p;
            }
        }

        public IQueryable<Property> FindFeatured()
        {
            return from p in db.Properties
                   where p.featured && 
                        (string.IsNullOrEmpty(p.status) || p.status != PROPERTY_STATUS_ARCHIVED)
                   select p;
        }

        public IQueryable<Property> FindLatest(int num = 5)
        {
            return (from p in db.Properties
                    where (string.IsNullOrEmpty(p.status) || p.status != PROPERTY_STATUS_ARCHIVED)
                    orderby p.added_on
                    select p).Take(num);
        }

        public IQueryable<Property> FindBySearchModel(PropertySearchModel sm)
        {
            if (sm.PropertyLocation == null || sm.PropertyLocation.Count == 0)
                throw new ArgumentException("Suburb must be provided.");

            if (String.IsNullOrEmpty(sm.RentOrSale))
                throw new ArgumentException("Must specify rent or sale.");

            IQueryable<Property> ps = from p in db.Properties
                                      where sm.PropertyLocation.Contains(p.suburb_id) &&
                                            p.rent_or_sale == sm.RentOrSale.ToLower() &&
                                           (string.IsNullOrEmpty(p.status) || p.status != PROPERTY_STATUS_ARCHIVED)
                                      select p;

            if (sm.PropertyTypes != null && sm.PropertyTypes.Count(pt => pt != 0) > 0)
            {
                ps = ps.Where(p => sm.PropertyTypes.Contains(p.property_type));
            }

            if (sm.NumOfBedroom.HasValue)
            {
                ps = ps.Where(p => p.num_of_bedroom >= sm.NumOfBedroom);
            }

            if (sm.NumOfBathroom.HasValue)
            {
                ps = ps.Where(p => p.num_of_bathroom >= sm.NumOfBathroom);
            }

            if (sm.NumOfCarspace.HasValue)
            {
                ps = ps.Where(p => p.num_of_carspace >= sm.NumOfCarspace);
            }

            if (sm.MinPrice.HasValue)
            {
                ps = ps.Where(p => !p.min_price.HasValue || p.min_price >= sm.MinPrice);
            }

            if (sm.MaxPrice.HasValue)
            {
                ps = ps.Where(p => !p.max_price.HasValue || p.max_price <= sm.MaxPrice);
            }

            return ps;
        }

        public void InsertOrUpdate(Property p)
        {
            if (p.ID == default(int))
            {
                db.Properties.Add(p);
            }
            else
            {
                db.Entry(p).State = System.Data.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
        }

        public void SubmitChanges()
        {
            db.SaveChanges();
        }
    }
}