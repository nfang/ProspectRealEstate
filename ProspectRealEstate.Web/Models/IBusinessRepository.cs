using ProspectRealEstate.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProspectRealEstate.Web.Models
{
    interface IBusinessRepository : IRepository<Business>
    {
        Business Find(string code, string lang);
        IQueryable<Business> FindFeatured();
        IQueryable<Business> FindLatest();
        IQueryable<Business> FindBySearchModel(BusinessSearchModel model);
    }
}
