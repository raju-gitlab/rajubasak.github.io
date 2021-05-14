using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Filter
{
    public class LoginCheckAttribute : ActionFilterAttribute
    {
        public void OnAuthorization(ActionExecutingContext filterContext)
        {
            string UserGuid = filterContext.HttpContext.Session["UserGuid"]?.ToString();
            if (UserGuid != null && UserGuid != string.Empty)
            {
                filterContext.Controller.ViewBag.IsLogedIn = true;
            }
            else
            {
                filterContext.Controller.ViewBag.IsLogedIn = false;
            }
        }
    }
}