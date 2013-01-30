using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace ProspectRealEstate.Web.Filters
{
    public class HandleCultureAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var specifiedCulture = new CultureInfo(filterContext.RouteData.Values["lang"].ToString());
            if (!Thread.CurrentThread.CurrentUICulture.Equals(specifiedCulture))
            {
                Thread.CurrentThread.CurrentUICulture = specifiedCulture;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}