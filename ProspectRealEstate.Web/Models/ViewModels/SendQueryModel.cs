using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProspectRealEstate.Web.Models.ViewModels
{
    public class SendQueryModel
    {
        public int AgentId { get; set; }
        public string AgentName { get; set; }
        public string AgentEmail { get; set; }

        public string UserName { get; set; }
        public string UserEmail { get; set; }

        public string Subject { get; set; }
        public string Message { get; set; }
    }
}