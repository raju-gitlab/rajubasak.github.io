using CMS.BUSINESS.IBusiness.IAddmission;
using CMS.BUSINESS.IBusiness.IEvent;
using CMS.BUSINESS.IBusiness.ILibrary;
using CMS.BUSINESS.IBusiness.IMaster;
using CMS.BUSINESS.IBusiness.IMaster.IAdminPortal;
using CMS.BUSINESS.IBusiness.IMaster.ICourse;
using CMS.BUSINESS.IBusiness.INotice;
using CMS.BUSINESS.IBusiness.ITeachers;
using CMS.Filter;
using CMS.MODEL.Book;
using CMS.MODEL.Event;
using CMS.MODEL.Library;
using CMS.MODEL.Master;
using CMS.UTILITIES.LogManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers.Master.Admin
{
    public class AdminPortalController : Controller
    {
        #region Constructors And Parameters
        private readonly IStudentFessBusiness _studentFessBusiness;
        private readonly INewStudentAddmissionBusiness _newStudentAddmissionBusiness;
        private readonly ILibraryBusiness _libraryBusiness;
        private readonly IAdminPortalBusiness _adminPortalBusiness;
        private readonly IEventBusiness _eventBusiness;
        private readonly ICourseBusiness _courseBusiness;
        private readonly ITeachersBusiness _teachersBusiness;
        private readonly INoticeBusiness _noticeBusiness;
        private readonly IAccountBusiness _accountBusiness;
        public AdminPortalController(IStudentFessBusiness studentFessBusiness, INewStudentAddmissionBusiness newStudentAddmissionBusiness, ILibraryBusiness libraryBusiness, IAdminPortalBusiness adminPortalBusiness, IEventBusiness eventBusiness, ICourseBusiness courseBusiness, ITeachersBusiness teachersBusiness, INoticeBusiness noticeBusiness, IAccountBusiness accountBusiness)
        {
            this._studentFessBusiness = studentFessBusiness;
            this._newStudentAddmissionBusiness = newStudentAddmissionBusiness;
            this._libraryBusiness = libraryBusiness;
            this._adminPortalBusiness = adminPortalBusiness;
            this._eventBusiness = eventBusiness;
            this._courseBusiness = courseBusiness;
            this._teachersBusiness = teachersBusiness;
            this._noticeBusiness = noticeBusiness;
            this._accountBusiness = accountBusiness;
        }
        #endregion

        #region Dropdowns
        public ActionResult TeacherAppointment()
        {
            var result = this._newStudentAddmissionBusiness.SemesterList();
            var result2 = this._newStudentAddmissionBusiness.StreamList();
            var result3 = this._adminPortalBusiness.TeacherList();
            var result4 = this._adminPortalBusiness.SubjectList();
            ViewBag.SemesterList = result;
            ViewBag.StreamList = result2;
            ViewBag.TeacherNames = result3;
            ViewBag.SubjectNames = result4;
            return View();
        }
        #endregion

        #region Login And LogOut
        [HttpGet]
        public ActionResult AdminstrationLogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminstrationLogIn(ItemCode credentials)
        {
            TeachersEditModel result = this._adminPortalBusiness.AdminstrationLogin(credentials);
            if(result != null)
            {
                Session["LogedInStatus"] = "true";
                Session["StudentImagePath"] = result.ImagePath;
                Session["UserName"] = result.TeacherName;
                Session["Email"] = credentials.Value;
                Session["UserGuid"] = result.TeacherGuid;
                Session["UserRole"] = result.RoleName;
                return RedirectToAction("AdminstrationPortal", "AdminPortal");
            }
            else
            {
                ViewBag.IncorrentCredentials = true;
                return View();
            }
        }
        #endregion

        #region Get
        #region Dropdowns
        public ActionResult StreamAndSemesterDropDown()
        {
            List<DropDownListModel> result2 = this._newStudentAddmissionBusiness.StreamList();
            List<DropDownListModel> result6 = this._newStudentAddmissionBusiness.SemesterList();
            ViewBag.StreamList = result2;
            ViewBag.SemesterList = result6;
            return View();
        }
        public ActionResult Dropdowns()
        {
            var result = this._adminPortalBusiness.dropdown();
            if (result != null)
            {
                ViewBag.BookNames = result;
                return View();
            }
            else
            {
                return View();
            }
        }
        public ActionResult RoleList()
        {
            var result = this._adminPortalBusiness.RoleList();
            if (result != null)
            {
                ViewBag.RoleNames = result;
                return View();
            }
            else
            {
                return View();
            }
        }
        public ActionResult Teacherlist()
        {
            var result = this._adminPortalBusiness.TeacherList();
            if (result != null)
            {
                ViewBag.TeacherNames = result;
                return View();
            }
            else
            {
                return View();
            }
        }
        public ActionResult SubjectList()
        {
            var result = this._adminPortalBusiness.SubjectList();
            if (result != null)
            {
                ViewBag.SubjectNames = result;
                return View();
            }
            else
            {
                return View();
            }
        }
        public JsonResult SemeList(string StreamGuid)
        {
            List<DropDownListModel> result = this._adminPortalBusiness.SemeList(StreamGuid);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GetAllServices

        [UserAuthorizeAttribute(RoleUser = "Admin,Teacher,Vice-Admin,Librarian")]
        public ActionResult AdminstrationPortal(AdminPortalDataBindingModel adminPortalData)
        {
            var result1 = this._libraryBusiness.listNotReturnedBooks();
            var result2 = this._libraryBusiness.ListRequestBooks();
            var result3 = this._eventBusiness.AllEvents();
            var result4 = this._libraryBusiness.ListBooks();
            var result5 = this._libraryBusiness.ListBooks();
            var result = new Tuple<List<BookBusinessModel>, List<RequestBookModel>, List<EventCardModel>, List<BooksModel>>(result1, result2, result3, result4);
            ViewBag.ReportAmount = this._adminPortalBusiness.TotalReports();
            return View(result);
        }
        #endregion

        #region Courses
        #region List Courses
        [UserAuthorizeAttribute(RoleUser = "Admin,Teacher,Vice-Admin")]
        public ActionResult CourseControl()
        {
            var res = this._courseBusiness.courses();
            return View(res);
        }
        #endregion

        #region ListOfDeletedCourses
        [UserAuthorizeAttribute(RoleUser = "Admin,Teacher,Vice-Admin")]
        public ActionResult DeletedCourseList()
        {
            var result = this._adminPortalBusiness.DeletedCourseList();
            if(result != null)
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

        #region Teacher
        #region RetriveAllCollegeTeacherNames
        //[UserAuthorizeAttribute(RoleUser = "Admin,Vice-Admin")]
        public ActionResult CollegeEnrolledTeachers()
        {
            var result = this._adminPortalBusiness.EnrolledTeachers();
            if(result != null)
            {
                return View(result);
            }
            else
            {
                return View();
            }
        }
        #endregion

        #region TeacherCourseDetails
        [UserAuthorizeAttribute(RoleUser = "Admin,Vice-Admin")]
        public ActionResult TeacherCourseDetails(string TID)
        {
            var result = this._adminPortalBusiness.TeacherAllDetails(TID);
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

        #region TeacherListForResign
        [UserAuthorizeAttribute(RoleUser = "Admin,Vice-Admin")]
        public ActionResult ResignTeacherList()
        {
            var result = this._adminPortalBusiness.EnrolledTeachers();
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

        #region Student
        #region GetListOfPassedOutStudents
        [UserAuthorizeAttribute(RoleUser = "Admin,Teacher,Vice-Admin")]
        public ActionResult PassedOutStudents(DropDownListModel Year)
        {
            var result = this._adminPortalBusiness.PassedOutStudents(Year);
            if(result != null)
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

        #region JosnResults
        #region CheckCourseNameAlreadyAvailableOrNot
        public JsonResult CheckCourseName(string name)
        {
            bool result = this._adminPortalBusiness.CheckCourseName(name);
            if(result == true)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }
        #endregion
        #endregion

        #region Notice
        #region GetListOfNoticesForModification
        [UserAuthorizeAttribute(RoleUser = "Admin,Teacher,Vice-Admin")]
        public ActionResult ModifyNotices()
        {
            var result = this._noticeBusiness.GetNotices();
            if(result != null)
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

        #region Library
        #region GetAllLibraryCards
        [HttpGet]
        [UserAuthorizeAttribute(RoleUser = "Admin,Teacher,Librarian,Vice-Admin")]
        public ActionResult LibraryCardsList()
        {
            List<DropDownListModel> result2 = this._newStudentAddmissionBusiness.StreamList();
            List<DropDownListModel> result6 = this._newStudentAddmissionBusiness.SemesterList();
            ViewBag.StreamList = result2;
            ViewBag.SemesterList = result6;
            return View();
        }
        [HttpPost]
        [UserAuthorizeAttribute(RoleUser = "Admin,Teacher,Librarian,Vice-Admin")]
        public ActionResult LibraryCardsList(DropDownListModel data)
        {
            var result = this._adminPortalBusiness.LibraryCardsList(data);
            if(result != null)
            {
                return View("ListLibraryCards", result);
            }
            else
            {
                return View("NoLibraryCardFound");
            }
        }
        #endregion
        #endregion

        #region GetReports
        public ActionResult Reports()

        {
            var result = this._adminPortalBusiness.Reports();
            if(result.Count != 0)
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

        #region Post
        #region Uplaod EventImages
        [HttpGet]
        [UserAuthorizeAttribute(RoleUser = "Admin,Teacher,Vice-Admin")]
        public ActionResult UploadEventImages()
        {
            return View();
        }
        [HttpPost]
        [UserAuthorizeAttribute(RoleUser = "Admin,Teacher,Vice-Admin")]
        public ActionResult UploadEventImages(EventGalleryModel UploadImage)
        {
            UploadImage.CreatedBy = Session["UserGuid"]?.ToString();
            if(UploadImage.CreatedBy != null && UploadImage.CreatedBy != string.Empty)
            {
                bool result = this._adminPortalBusiness.UploadEventImages(UploadImage);
                if (result == true)
                {
                    return View();
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Login", "Accounts");
            }
        }
        #endregion

        #region TeacherZone
        #region AddNewTeacher

        [UserAuthorizeAttribute(RoleUser = "Admin,Vice-Admin")]
        public ActionResult AddTeacher(TeachersEditModel addTeacher)
        {
            try
            {
                bool result = this._adminPortalBusiness.NewTeacher(addTeacher);
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

        #region AddNewTeacher
        /// <summary>
        /// Appoint Teacher For Course
        /// </summary>
        /// <param name="addTeacher"></param>
        /// <returns></returns>

        [UserAuthorizeAttribute(RoleUser = "Admin,Vice-Admin")]
        public ActionResult AppointTeacher(DropDownListModel addTeacher)
        {
            try
            {
                bool result = this._adminPortalBusiness.AppointTeacher(addTeacher);
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

        #region Course
        #region AddNewCourse
        [HttpGet]

        [UserAuthorizeAttribute(RoleUser = "Admin")]
        public ActionResult AddCourse()
        {
            return View();
        }
        [HttpPost]

        [UserAuthorizeAttribute(RoleUser = "Admin")]
        public ActionResult AddCourse(CourseEditModel Course)
        {
            var result = this._adminPortalBusiness.AddCourse(Course);
            if(result == true)
            {
                return RedirectToAction("Courses", "Course");
            }
            else
            {
                return View();
            }
        }
        #endregion

        #endregion

        #region CreateTeacherAccount
        public ActionResult TeacherAc(ItemCode data)
        {
            try
            {
                if (this._accountBusiness.CheckTeacherExistence(data.Value))
                {
                    bool result = this._accountBusiness.CreateTeacherAccount(data);

                    if (result == true)
                    {
                        return RedirectToAction("AdminstrationLogIn", "AdminPortal");
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    ViewBag.UserNotFound = true;
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

        #region Put
        #region Update CourseData
        [HttpGet]

        [UserAuthorizeAttribute(RoleUser = "Admin,Teacher,Vice-Admin")]
        public ActionResult UpdateCourseData(string CrsID)
        {
            Tuple<CourseEditModel, List<CourseSemesterModel>> result = this._adminPortalBusiness.GetSpecificCourse(CrsID);
            if (result.Item1 != null && result.Item2 != null)
            {
                return View(result);
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        [UserAuthorizeAttribute(RoleUser = "Admin,Teacher,Vice-Admin")]
        public ActionResult UpdateCourseData(CourseEditModel EditCourse)
        {
            bool result = this._adminPortalBusiness.UpdateCourse(EditCourse);
            if(result == true)
            {
                ViewBag.SuccessUplaod = true;
                return RedirectToAction("UpdateCourseData", "AdminPortal", new { CrsID = EditCourse.Code });
            }
            else
            {
                ViewBag.SuccessUplaod = false;
                return View();
            }
            
        }
        #endregion

        #region EnrollDeletedCourse
        [UserAuthorizeAttribute(RoleUser = "Admin")]
        public ActionResult EnrollDeletedCourse(string CID)
        {
            var result = this._adminPortalBusiness.EnrollDeletedCourse(CID);
            if(result == true)
            {
                return RedirectToAction("Courses", "Course");
            }
            else
            {
                return View();
            }
        }
        #endregion

        #region Student
        #region Edit Student
        [HttpGet]
        [UserAuthorizeAttribute(RoleUser = "Admin,Teacher,Vice-Admin")]
        public ActionResult EditStudent()
        {
            List<DropDownListModel> result2 = this._newStudentAddmissionBusiness.StreamList();
            List<DropDownListModel> result6 = this._newStudentAddmissionBusiness.SemesterList();
            ViewBag.StreamList = result2;
            ViewBag.SemesterList = result6;
            return View();
        }
        [HttpPost]
        [UserAuthorizeAttribute(RoleUser = "Admin,Teacher,Vice-Admin")]
        public ActionResult EditStudent(DropDownListModel data)
        {
            var result = this._adminPortalBusiness.StudentsDetailsByFilter(data);
            if(result != null)
            {
                return View("EditStudentsList",result);
            }
            else
            {
                return View("NoStudentCardFound");
            }
        }
        #endregion
        #endregion

        #region Report
        #region UpdateReportReadStatus
        public ActionResult ReadReport(string RID)
        {
            bool result = this._adminPortalBusiness.ReadReport(RID);
            if (result == true)
            {
                return RedirectToAction("Reports", "AdminPortal");
            }
            else
            {
                return RedirectToAction("Reports", "AdminPortal");
            }
        }
        #endregion
        #endregion

        #region Teacher
        #region UpdateTeacherData
        [HttpGet]
        public ActionResult UpdateTeachersList()
        {
            var result = this._teachersBusiness.ListTeachers();
            return View(result);
        }
        [HttpGet]
        public ActionResult UpdateTeacherData(string TID)
        {
            var result = this._teachersBusiness.Teacher_SingleDetails(TID);
            return View(result);
        }
        [HttpPost]
        public ActionResult UpdateTeacherData(TeachersEditModel TID)
        {
            bool result = this._adminPortalBusiness.UpdateTeacherData(TID);
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
        #endregion

        #region Delete
        #region deleteCourseSemester
        [UserAuthorizeAttribute(RoleUser = "Admin")]
        [HttpPost]
        public JsonResult DeleteCourseSemester(string CrsId, string SemsId)
        {
            var result = this._adminPortalBusiness.deleteCourseSemester(CrsId, SemsId);
            if(result == true)
            {
                return Json(new { status = "Success" });
            }
            else
            {
                return Json(new { status = "Failed" });
            }
        }
        #endregion

        #region RemoveTeacherFromCourses
        [UserAuthorizeAttribute(RoleUser = "Admin,Vice-Admin")]
        public ActionResult RemoveCourseTeacher(DropDownListModel details)
        {
            var result = this._adminPortalBusiness.RemoveCourseTeacher(details);
            if(result == true)
            {
                return RedirectToAction("AdminstrationPortal", "AdminPortal");
            }
            else
            {
                return View();
            }
        }
        #endregion

        #region Resignteacher

        [UserAuthorizeAttribute(RoleUser = "Admin,Teacher,Vice-Admin")]
        public ActionResult Resignteacher(ResignteacherModel TID)
        {
            bool result = this._adminPortalBusiness.Resignteacher(TID);
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

        #region Course
        #region DeleteCourse
        [HttpGet]
        public ActionResult DeleteCourse()
        {
            var result = this._courseBusiness.courses();
            if(result != null)
            {
                return View(result);
            }
            else
            {
                return View();
            }
        }

        [UserAuthorizeAttribute(RoleUser = "Admin")]
        public ActionResult DeleteCourseById(string CID)
        {
            bool result = this._adminPortalBusiness.DeleteCourse(CID);
            if (result == true)
            {
                return RedirectToAction("Courses", "Course");
            }
            else
            {
                return View();
            }
        }
        #endregion
        #endregion

        #region DeleteNotice
        [UserAuthorizeAttribute(RoleUser = "Admin,Teacher,Vice-Admin")]
        public ActionResult DeleteNotice(string NID)
        {
            var result = this._adminPortalBusiness.DeleteNotice(NID);
            if (result == true)
            {
                return RedirectToAction("ModifyNotices", "AdminPortal");
            }
            else
            {
                return View();
            }
        }
        #endregion

        #region Librarycard
        [UserAuthorizeAttribute(RoleUser = "Admin,Teacher,Librarian,Vice-Admin")]
        public JsonResult DeleteLibraryCard(string LcID)
        {
            bool result = this._adminPortalBusiness.DeleteLibraryCard(LcID);
            if(result == true)
            {
                return Json(new { status = "Success" });
            }
            else
            {
                return Json(new { status = "Failed" });
            }
        }
        #endregion
        #endregion
    }
}