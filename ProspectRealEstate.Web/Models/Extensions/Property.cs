using ProspectRealEstate.Web.Helpers;
using ProspectRealEstate.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ProspectRealEstate.Web.Models
{
    public partial class Property
    {
        public PropertyListModel ToListModel()
        {
            var slug = this.description;
            if (this.description.Length > 250)
                slug = this.description.Substring(0, 247) + "...";

            var listModel = new PropertyListModel()
            {
                ID = this.ID,
                Title = String.Join(",", this.street, this.Suburb.name),
                Description = slug,
                IsFeatured = this.featured,
                NumOfBathroom = this.num_of_bathroom.ToString("##"),
                NumOfBedroom = this.num_of_bedroom.ToString("##"),
                NumOfCarspace = this.num_of_carspace.ToString("##"),
                Price = StringHelper.ParsePriceRange(this.min_price, this.max_price),
                AddedOn = this.added_on,
                Status = this.status
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

        public string PriceRange
        {
            get { return StringHelper.ParsePriceRange(this.min_price, this.max_price); }
        }
    }
}