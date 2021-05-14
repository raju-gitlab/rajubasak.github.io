using CMS.BUSINESS.IBusiness.ITeachers;
using CMS.UTILITIES.LogManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers.Teachers
{
    public class TeachersController : Controller
    {
        #region Constructor And Parameters
        private readonly ITeachersBusiness _teachersBusiness;
        public TeachersController(ITeachersBusiness teachersBusiness)
        {
            this._teachersBusiness = teachersBusiness;
        }
        #endregion

        #region Get
        #region GetTeachers
        [HttpGet]
        public ActionResult OurTeachers()
        {
            try
            {
                var result = this._teachersBusiness.ListTeachers();
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

        #region GetTeachersDetails
        public ActionResult Teacher_SingleDetails(string TSLid)
        {
            try
            {
                var result = this._teachersBusiness.Teacher_SingleDetails(TSLid);
                if(result != null)
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