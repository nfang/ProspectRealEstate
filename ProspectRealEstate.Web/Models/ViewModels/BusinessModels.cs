using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ProspectRealEstate.Web.Models.ViewModels
{
    public class BusinessSearchModel
    {
        // Required - Suburb IDs
        public List<short> BusinessLocation { get; set; }

        // Optional
        public byte? Category { get; set; }
        public long? MinAskingPrice { get; set; }
        public long? MaxAskingPrice { get; set; }
    }

    public class BusinessListModel : FeaturedModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsFeatured { get; set; }
        public string Location { get; set; }
        public string AskingPrice { get; set; }
        public string Category { get; set; }
        public DateTime AddedOn { get; set; }
        public string Status { get; set; }
        public string ThumbnailFilePath { get; set; }

        public override string Subject
        {
            get { return this.Title; }
        }

        public override string Tagline
        {
            get { return this.AskingPrice; }
        }

        public override FeaturedModelType Type
        {
            get { return FeaturedModelType.Business; }
        }

        public override string FeaturedPhoto { get; set; }
    }

}