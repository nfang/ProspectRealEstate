using ProspectRealEstate.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ProspectRealEstate.Web.Models.ViewModels
{
    public class PropertySearchModel
    {
        // Required - Suburb IDs
        public List<short> PropertyLocation { get; set; }
        public string RentOrSale { get; set; }

        // Optional
        public List<short> PropertyTypes { get; set; }
        public short? NumOfBedroom { get; set; }
        public short? NumOfBathroom { get; set; }
        public short? NumOfCarspace { get; set; }
        public long? MinPrice { get; set; }
        public long? MaxPrice { get; set; }
    }

    public class PropertyListModel : FeaturedModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsFeatured { get; set; }
        public string NumOfBedroom { get; set; }
        public string NumOfBathroom { get; set; }
        public string NumOfCarspace { get; set; }
        public string Price { get; set; }
        public string ThumbnailFilePath { get; set; }
        public DateTime AddedOn { get; set; }
        public string Status { get; set; }

        public override string Subject
        {
            get { return this.Title; }
        }

        public override string Tagline
        {
            get { return this.Price; }
        }

        public override FeaturedModelType Type
        {
            get { return FeaturedModelType.Property; }
        }

        public override string FeaturedPhoto { get; set; }
    }

    public class RentOrSaleModel
    {
        public string Literal { get; set; }
        public string Value { get; set; }
    }
}