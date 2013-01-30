using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProspectRealEstate.Web.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<PropertyListModel> LatestProperties { get; set; }
        public IEnumerable<BusinessListModel> LatestBusinesses { get; set; }
        public IEnumerable<FeaturedModel> FeaturedItems { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Suburb> Suburbs { get; set; }
        public IEnumerable<PropertyType> PropertyTypes { get; set; }
    }

    public abstract class FeaturedModel
    {
        public abstract string Subject { get; }
        public abstract string Tagline { get; }
        public abstract FeaturedModelType Type { get; }
        public abstract string FeaturedPhoto { get; set; }
    }

    public enum FeaturedModelType
    {
        Business, Property
    }
}