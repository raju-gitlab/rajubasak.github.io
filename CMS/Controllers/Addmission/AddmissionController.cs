using CMS.BUSINESS.IBusiness.IAddmission;
using CMS.Filter;
using CMS.MODEL.Addmission;
using CMS.MODEL.Master;
using CMS.MODEL.User;
using CMS.UTILITIES.LogManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers.Addmission
{
    public class AddmissionController : Controller
    {
        #region Parameters And Constructor
        private readonly INewStudentAddmissionBusiness _newStudentAddmissionBusiness;
        public AddmissionController(INewStudentAddmissionBusiness newStudentAddmissionBusiness)
        {
            this._newStudentAddmissionBusiness = newStudentAddmissionBusiness;
        }
        #endregion

        #region Get
        #region ListItemsForDropDownLists
        public ActionResult CountryList()
        {
            try
            {
                List<DropDownListModel> result = this._newStudentAddmissionBusiness.CountryList();
                List<DropDownListModel> result2 = this._newStudentAddmissionBusiness.StreamList();
                List<DropDownListModel> result3 = this._newStudentAddmissionBusiness.GenderList();
                List<DropDownListModel> result4 = this._newStudentAddmissionBusiness.StateList();
                List<DropDownListModel> result5 = this._newStudentAddmissionBusiness.CityList();
                List<DropDownListModel> result6 = this._newStudentAddmissionBusiness.SemesterList();
                if (result != null)
                {
                    ViewBag.CountryList = result;
                    ViewBag.StreamList = result2;
                    ViewBag.GenderList = result3;
                    ViewBag.Statelist = result4;
                    ViewBag.CityList = result5;
                    ViewBag.SemesterList = result6;
                    return View();
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
        public ActionResult StreamList()
        {
            try
            {
                List<DropDownListModel> result = this._newStudentAddmissionBusiness.StreamList();
                if (result != null)
                {
                    ViewBag.StreamList = result;
                    return View();
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
        public ActionResult GenderList()
        {

            try
            {
                List<DropDownListModel> result = this._newStudentAddmissionBusiness.GenderList();
                if (result != null)
                {
                    ViewBag.GenderList = result;
                    return View();
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
        public ActionResult StateList()
        {

            try
            {
                List<DropDownListModel> result = this._newStudentAddmissionBusiness.StateList();
                if (result != null)
                {
                    ViewBag.Statelist = result;
                    return View();
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

        public JsonResult StateListById(string CountryGuid)
        {
            try
            {
                List<DropDownListModel> StateListById = this._newStudentAddmissionBusiness.StateListById(CountryGuid);
                return Json(StateListById, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        public JsonResult CityListById(string StateGuid)
        {
            try
            {
                List<DropDownListModel> StateListById = this._newStudentAddmissionBusiness.CityListById(StateGuid);
                return Json(StateListById, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        public ActionResult SemesterList()
        {
            try
            {
                List<DropDownListModel> result = this._newStudentAddmissionBusiness.SemesterList();
                if(result != null)
                {
                    ViewBag.SemesterList = result;
                    return View();
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
        public JsonResult SemesterListById(string StreamGuid)
        {
            List<DropDownListModel> streamList = this._newStudentAddmissionBusiness.SemesterListById(StreamGuid);
            return Json(streamList, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region SearchStudentbyStudentName,Stream,Semester
        [HttpGet]
        [UserAuthorizeAttribute(RoleUser = "Admin,Teacher,Vice-Admin")]
        public ActionResult SearchStudent()
        {
            return View();
        }
        [HttpPost]
        [UserAuthorizeAttribute(RoleUser = "Admin,Teacher,Vice-Admin")]
        public ActionResult SearchStudent(StudentFessEditModel studentFess)
        {
            try
            {
                List<StudentCardModel> result = this._newStudentAddmissionBusiness.SearchStudent(studentFess);
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

        #region GetNewStudentsAddmissionListByCurrentTime(ByCurrentYear)

        [UserAuthorizeAttribute(RoleUser = "Admin,Teacher,Vice-Admin")]
        public ActionResult FresherStudents()
        {
            try
            {
                List<StudentModel> result = this._newStudentAddmissionBusiness.NewStudentsAddmissionList();
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

        #region GetNewStudentsAddmissionListByCurrentTime(ByCurrentYear)

        [UserAuthorizeAttribute(RoleUser = "Admin,Teacher,Vice-Admin")]
        public ActionResult FresherStudentCatagory(StudentModel student)
        {
            try
            {
                List<StudentModel> result = this._newStudentAddmissionBusiness.NewStudentsAddmissionListByFilterParameter(student);
                if(result != null)
                {
                    return View();
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

        #region Post
        #region NewtudentAddmission
        [HttpGet]
        [UserAuthorizeAttribute(RoleUser = "Admin,Teacher,Vice-Admin")]
        public ActionResult NewAddmission()
        {
            List<DropDownListModel> result = this._newStudentAddmissionBusiness.CountryList();
            List<DropDownListModel> result2 = this._newStudentAddmissionBusiness.StreamList();
            List<DropDownListModel> result3 = this._newStudentAddmissionBusiness.GenderList();
            List<DropDownListModel> result4 = this._newStudentAddmissionBusiness.StateList();
            List<DropDownListModel> result5 = this._newStudentAddmissionBusiness.CityList();
            List<DropDownListModel> result6 = this._newStudentAddmissionBusiness.SemesterList();
            ViewBag.CountryList = result;
            ViewBag.StreamList = result2;
            ViewBag.GenderList = result3;
            ViewBag.Statelist = result4;
            ViewBag.CityList = result5;
            ViewBag.SemesterList = result6;
            return View();
        }
        [HttpPost]
        [UserAuthorizeAttribute(RoleUser = "Admin,Teacher,Vice-Admin")]
        public ActionResult NewAddmission(AddmissionEditModel student)
        {
            try
            {
                student.CreatedBy = Session["UserGuid"].ToString();
                bool result = this._newStudentAddmissionBusiness.StudentAddmission(student);
                if(result == true)
                {
                    return View();
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