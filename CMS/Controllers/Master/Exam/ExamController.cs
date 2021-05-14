using CMS.BUSINESS.IBusiness.IAddmission;
using CMS.BUSINESS.IBusiness.IMaster.IExam;
using CMS.MODEL.Master;
using CMS.UTILITIES.LogManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers.Master.Exam
{
    public class ExamController : Controller
    {
        #region Constructor and Parameters
        private readonly IExamBusiness _iExamBusiness;
        private readonly INewStudentAddmissionBusiness _newStudentAddmissionBusiness;
        public ExamController(IExamBusiness iExamBusiness, INewStudentAddmissionBusiness newStudentAddmissionBusiness)
        {
            this._iExamBusiness = iExamBusiness;
            this._newStudentAddmissionBusiness = newStudentAddmissionBusiness;

        }
        #endregion

        #region Get
        #region ExamPaper
        #region GetExamPaperByExamAnd Year Id
        public ActionResult ExamPaper()
        {
            List<DropDownListModel> result2 = this._newStudentAddmissionBusiness.StreamList();
            List<DropDownListModel> result6 = this._newStudentAddmissionBusiness.SemesterList();
            ViewBag.StreamList = result2;
            ViewBag.SemesterList = result6;
            return View();
        }
        public ActionResult GetExamPaper(DropDownListModel data)
        {
            var result = this._iExamBusiness.GetExamPaper(data);
            if (result != null)
            {
                return View(result);
            }
            else
            {
                return View();
            }
        }
        #endregion
        #endregion
        #endregion

        #region Post
        #region ExamPaper
        #region UploadNewExamPaper
        [HttpGet]
        public ActionResult UploadPaper()
        {
            List<DropDownListModel> result2 = this._newStudentAddmissionBusiness.StreamList();
            List<DropDownListModel> result6 = this._newStudentAddmissionBusiness.SemesterList();
            ViewBag.StreamList = result2;
            ViewBag.SemesterList = result6;
            return View();
        }
        [HttpPost]
        public ActionResult UploadPaper(ExamPaperModel Paper)
        {
            try
            {
                Paper.CreatedBy = Session["UserGuid"]?.ToString().ToUpper();
                bool result = this._iExamBusiness.UploadPreviousYearQuestionpaper(Paper);
                if (result == true)
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
        #endregion
    }
}