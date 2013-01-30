using ProspectRealEstate.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProspectRealEstate.Web.Models
{
    interface IPropertyRepository : IRepository<Property>
    {
        Property Find(string code);
        IQueryable<Property> FindFeatured();
        IQueryable<Property> FindLatest(int num);
        IQueryable<Property> FindBySearchModel(PropertySearchModel model);
    }
}
