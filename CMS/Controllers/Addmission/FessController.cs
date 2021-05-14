using CMS.BUSINESS.IBusiness.IAddmission;
using CMS.Filter;
using CMS.MODEL.Addmission;
using CMS.MODEL.Master;
using CMS.UTILITIES.LogManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers.Addmission
{
    /*[UserAuthorizeAttribute(RoleUser = "Teacher,Admin")]*/
    public class FessController : Controller
    {
        #region Parameter And Constructor
        private readonly IStudentFessBusiness _studentFessBusiness;
        private readonly INewStudentAddmissionBusiness _newStudentAddmissionBusiness;
        public FessController(IStudentFessBusiness studentFessBusiness, INewStudentAddmissionBusiness newStudentAddmissionBusiness)
        {
            this._studentFessBusiness = studentFessBusiness;
            this._newStudentAddmissionBusiness = newStudentAddmissionBusiness;
        }
        #endregion

        #region Get
        #region GetStudentDetailsById
        [HttpGet]
        public ActionResult FindStudent()
        {
            List<DropDownListModel> result2 = this._newStudentAddmissionBusiness.StreamList();
            ViewBag.StreamList = result2;
            return View();
        }
        [HttpPost]
        public ActionResult FindStudent(StudentFessEditModel studentFess)
        {
            try
            {
                List<StudentFessDetails> result = this._studentFessBusiness.GetStudentDetails(studentFess);
                if (result != null)
                {
                    return View("FindStudentList", result);
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

        #region GetStudentFessDetailsById
        [HttpGet]
        public ActionResult FessDeails(string StudentGuid)
        {
            try
            {
                Tuple<List<StudentFessDetails>, List<StudentFessEditModel>> result = this._studentFessBusiness.GetStudentFeesDetails(StudentGuid);
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

        #region GetStudentFess
        public ActionResult abc()
        {
            return View();
        }
        #endregion

        #endregion

        #region Put
        #region UpdateFess
        //[HttpGet]
        //public ActionResult UpdateFess()
        //{
        //    return View();
        //}
        //[HttpPost]
        public ActionResult UpdateFess(StudentFessEditModel studentFess)
        {
            try
            {
                bool result = this._studentFessBusiness.UpdateFess(studentFess);
                if (result == true)
                {
                    return RedirectToAction("FessDeails", "Fess", studentFess.StudentGuid);
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

        #region UpdateFine
        [HttpGet]
        public ActionResult SearchStudent()
        {
            List<DropDownListModel> result2 = this._newStudentAddmissionBusiness.StreamList();
            ViewBag.StreamList = result2;
            return View();
        }
        [HttpPost]
        public ActionResult UpdateFine(StudentFineModel data)
        {
            var result = this._studentFessBusiness.UpdateFine(data);
            if(result != null)
            {
                return View(result);
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult DepositFine(StudentFineModel SID)
        {
            bool result = this._studentFessBusiness.DepositFine(SID);
            if(result == true)
            {
                return View();
            }
            else
            {
                return View();
            }
        }
        #endregion
        #endregion
    }
}