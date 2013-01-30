using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProspectRealEstate.Web.Models
{
    public partial class Page
    {
        public IEnumerable<PageInOtherLanguagesModel> Multilingua { get; set; }
    }

    public class PageInOtherLanguagesModel
    {
        public int ID { get; set; }
        public Language Language { get; set; }
    }
}