using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers.Master.Error
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult UnAuthorizedUser()
        {
            return View();
        }
    }
}