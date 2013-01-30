using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProspectRealEstate.Web.Models.ViewModels
{
    public class PageContent
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}