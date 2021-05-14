using CMS.BUSINESS.IBusiness.IMaster.ICourse;
using CMS.MODEL.Master;
using CMS.UTILITIES.LogManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers.Master.Course
{
    public class CourseController : Controller
    {
        #region Parameters And Constructor
        private readonly ICourseBusiness _courseBusiness;
        public CourseController(ICourseBusiness courseBusiness)
        {
            this._courseBusiness = courseBusiness;
        }
        #endregion

        #region Get
        #region GetCourseList
        public ActionResult Courses()
        {
            try
            {
                List<CourseEditModel> result = this._courseBusiness.courses();
                if(result != null)
                {
                    string UserGuid = Session["UserGuid"]?.ToString();
                    if(UserGuid != null)
                    {
                        ViewBag.IsLogedIn = true;
                        return View(result);
                    }
                    else
                    {
                        ViewBag.IsLogedIn = true;
                        return View(result);
                    }
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion

        #region CourseDetailById
        public ActionResult CourseDetails(string ciD)
        {
            try
            {
                var result = this._courseBusiness.CourseDetails(ciD);
                if (result != null)
                {
                    return View(result);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion
        #endregion
    }
}