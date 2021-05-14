using CMS.BUSINESS.IBusiness.IEvent;
using CMS.BUSINESS.IBusiness.IMaster.IContactUs;
using CMS.BUSINESS.IBusiness.IMaster.ICourse;
using CMS.BUSINESS.IBusiness.ITeachers;
using CMS.Filter;
using CMS.MODEL.Event;
using CMS.MODEL.Master;
using CMS.UTILITIES.LogManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers
{
    public class HomeController : Controller
    {
        #region Parameters And Constructor
        public readonly IEventBusiness _eventBusiness;
        public readonly ICourseBusiness _courseBusiness;
        public readonly ITeachersBusiness _teachersBusiness;
        private readonly IContactUsBusiness _iContactUsBusiness;
        public HomeController(IEventBusiness eventBusiness, ICourseBusiness courseBusiness, ITeachersBusiness teachersBusiness, IContactUsBusiness ContactUsBusiness)
        {
            this._eventBusiness = eventBusiness;
            this._courseBusiness = courseBusiness;
            this._teachersBusiness = teachersBusiness;
            this._iContactUsBusiness = ContactUsBusiness;
        }
        #endregion

        public ActionResult Index()

        {
            try
            {
                var result = this._courseBusiness.HomePageCourseContents();
                var result1 = this._eventBusiness.HomePageEventContents();
                var result2 = this._teachersBusiness.HomePageTeacherContents();
                var content = new Tuple<List<CourseEditModel>, List<EventCardModel>, List<CourseTeacherModel>>(result, result1, result2);
                if(content != null)
                {
                    return View(content);
                }
                else
                {
                    return View(content);
                }
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw;
            }
        }

        #region Pre-deifned Attributes
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.IsShowAlert = false;
            return View();
        }
        [HttpPost]
        public ActionResult Contact(ContactUsModel contactUs)
        {
            try
            {
                contactUs.CreatedBy = Session["UserGuid"]?.ToString();
                if(contactUs.CreatedBy != null && contactUs.CreatedBy != string.Empty)
                {
                    bool result = this._iContactUsBusiness.CreateReport(contactUs);
                    if (result == true)
                    {
                        ViewBag.IsShowAlert = true;
                        ViewBag.BannerLog = "Your Report is Submitted Successfully.We will Take action as soon as possible";
                        return View();
                    }
                    else
                    {

                        ViewBag.IsShowAlert = true;
                        ViewBag.BannerLog = "OOps Something went wrong";
                        return View();
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Accounts");
                }
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw;
            }
        }

        public ActionResult Infrastructure()
        {
            return View();
        }
        #endregion

        #region PageNotFound
        public ActionResult PageNotFound()
        {
            return View();
        }
        #endregion

        #region Library
        public ActionResult Library()
        {
            return View();
        }
        #endregion

        #region MyRegion
        public ActionResult Gallery()
        {
            return View();
        }
        #endregion
    }
}