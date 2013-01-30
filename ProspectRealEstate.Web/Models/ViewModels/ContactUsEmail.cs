using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProspectRealEstate.Web.Models.ViewModels
{
    public class ContactUsEmail
    {
        public string SenderName { get; set; }
        public string From { get; set; }
        public string Message { get; set; }
    }
}