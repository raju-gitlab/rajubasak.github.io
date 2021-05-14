using CMS.UTILITIES.LogManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CMS.Filter
{
    public class SessionAuthorizationAttribute : ActionFilterAttribute
    {
        string UserType;
        public SessionAuthorizationAttribute(string UserType)
        {
            this.UserType = UserType;
        }
        public void AOnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["Email"] == null || filterContext.HttpContext.Session["StudentGuid"] == null)
            {

                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.HttpContext.Response.Write("Session Timeout");
                    filterContext.HttpContext.Response.End();
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Account" }, { "Action", "LogOut" } });

                }
                else
                {
                    filterContext.HttpContext.Response.Redirect("~/Accounts/Login");
                }

            }
            else
            {
                var RoleUser = Convert.ToString(HttpContext.Current.Session["UserRole"]);
                if (RoleUser != "Admin")
                {
                    if (this.UserType == "Admin")
                    {
                        filterContext.HttpContext.Response.Redirect("~/Library/Errorshow");
                    }
                    else
                    {
                        filterContext.HttpContext.Response.Redirect("~/Library/Errorshow");
                    }
                }
                else
                {

                }

            }

            //base.OnActionExecuting(filterContext);
        }
    }
}