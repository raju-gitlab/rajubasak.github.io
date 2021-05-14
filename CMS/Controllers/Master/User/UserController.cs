using CMS.BUSINESS.IBusiness.IEvent;
using CMS.MODEL.User;
using CMS.UTILITIES.LogManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers.Master.User
{
    public class UserController : Controller
    {
        #region Parameters And Controller
        private readonly IUserStudentsBusiness _userStudentsBusiness;
        public UserController(IUserStudentsBusiness userStudentsBusiness)
        {
            this._userStudentsBusiness = userStudentsBusiness;
        }
        #endregion

        #region Get
        #region GetStudenstByStudenId
        public ActionResult StudentDetails(string StudentGuid)
        {
            try
            {
                UserStudenEdittModel result = this._userStudentsBusiness.GetSpecificStudentStudent(StudentGuid);
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
                throw new Exception("error");
            }
        }
        #endregion

        #region GetAllStudents
        #region GetStudensby Depeartment And Semester Wise
        public ActionResult GetStudentsList(StudentModel students)
        {
            List<StudentModel> result = this._userStudentsBusiness.GetStudentsList(students);
            if(result != null)
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
        #endregion

        #region Exception Views
        public ActionResult UnAuthrizedUser()
        {
            return View();
        }
        #endregion
    }
}