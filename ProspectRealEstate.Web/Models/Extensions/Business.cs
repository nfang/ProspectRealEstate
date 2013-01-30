using ProspectRealEstate.Web.Helpers;
using ProspectRealEstate.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace ProspectRealEstate.Web.Models
{
    public partial class Business
    {
        public string AskingPrice
        {
            get { return StringHelper.ParsePriceRange(this.asking, null); }
        }

        public string TakingPrice
        {
            get { return StringHelper.ParsePriceRange(this.taking, null); }
        }

        public string RentPrice
        {
            get { return StringHelper.ParsePriceRange(this.rent, null); }
        }

        public BusinessListModel ToListModel()
        {
            var slug = this.description;
            if (this.description.Length > 250)
                slug = this.description.Substring(0, 247) + "...";

            var listModel = new BusinessListModel()
            {
                ID = this.ID,
                Title = this.title,
                Description = slug,
                IsFeatured = this.featured,
                Location = this.Suburb.name,
                AskingPrice = this.asking.ToString("C", CultureInfo.CurrentUICulture),
                Category = this.Category.name,
                AddedOn = this.added_on,
                Status = this.status,
                ThumbnailFilePath = ""
            };

            if (this.Attachments.Count > 0)
            {
                listModel.ThumbnailFilePath = this.Attachments.First().file_location;

                var featuredFilePath = this.Attachments.FirstOrDefault(
                    a => Path.GetFileNameWithoutExtension(a.file_location).ToLower().StartsWith("featured"));

                if (featuredFilePath != null)
                    listModel.FeaturedPhoto = featuredFilePath.file_location;
                else
                    listModel.FeaturedPhoto = listModel.ThumbnailFilePath;
            }

            return listModel;
        }
    }
}