using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Filter
{
    public class UserAuthorizeAttribute : AuthorizeAttribute
    {
        public string RoleUser { get; set; }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if(Authorization(filterContext))
            {
                if(CheckUserRole(filterContext))
                {
                    return ;
                }
                else
                {
                    filterContext.HttpContext.Response.Redirect("/User/UnAuthrizedUser");
                }
            }
            else
            {
                filterContext.HttpContext.Response.Redirect("/Accounts/Login");
            }
            base.OnAuthorization(filterContext);
        }
        public bool Authorization(AuthorizationContext filterContext)
        {
            try
            {
                string UserRole = filterContext.HttpContext.Session["UserRole"]?.ToString();
                string UserGuid = filterContext.HttpContext.Session["UserGuid"]?.ToString();
                if (UserRole != null && UserGuid != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                filterContext.HttpContext.Response.Redirect("/Accounts/Login");
                throw;
            }
        }
        public bool CheckUserRole(AuthorizationContext FilterContext)
        {
            string[] Roles = RoleUser.Split(new char[] { ',' });
            string UserRole = FilterContext.HttpContext.Session["UserRole"]?.ToString();
            if(UserRole != null && UserRole != string.Empty)
            {
                foreach(var item in Roles)
                {
                    if(item == UserRole)
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
        }
    }
}