using CMS.BUSINESS.IBusiness.IMaster.IAdminPortal;
using CMS.MODEL.Event;
using CMS.MODEL.Library;
using CMS.MODEL.Master;
using CMS.MODEL.User;
using CMS.REPOSITORY.IRepository.IMaster.AdminPortal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.BUSINESS.Business.Master.AdminPortal
{
    public class AdminPortalBusiness : IAdminPortalBusiness
    {
        #region Parameters And Constructors
        private readonly IAdminPortalRepository _adminPortalRepository;
        public AdminPortalBusiness(IAdminPortalRepository adminPortalRepository)
        {
            this._adminPortalRepository = adminPortalRepository;
        }
        #endregion

        #region Dropdowns
        public List<DropDownListModel> dropdown()
        {
            return this._adminPortalRepository.dropdown();
        }
        public List<DropDownListModel> RoleList()
        {
            return this._adminPortalRepository.RoleList();
        }
        public List<DropDownListModel> TeacherList()
        {
            return this._adminPortalRepository.TeacherList();
        }
        public List<DropDownListModel> SubjectList()
        {
            return this._adminPortalRepository.SubjectList();
        }
        public List<DropDownListModel> SemeList(string Id)
        {
            return this._adminPortalRepository.SemeList(Id);
        }

        #endregion

        #region Login And LogOut
        public TeachersEditModel AdminstrationLogin(ItemCode credentials)
        {
            return this._adminPortalRepository.AdminstrationLogin(credentials);
        }
        #endregion

        #region Get
        #region GetDetailsAboutCourse
        public Tuple<CourseEditModel, List<CourseSemesterModel>> GetSpecificCourse(string CourseId)
        {
            return this._adminPortalRepository.GetSpecificCourse(CourseId);
        }
        #endregion

        #region TeacherAllDetails
        public Tuple<CourseTeacherModel, List<DropDownListModel>> TeacherAllDetails(string TeacherId)
        {
            return this._adminPortalRepository.TeacherAllDetails(TeacherId);
        }
        #endregion

        #region CollegtEnrolledTeachersList
        public List<CourseTeacherModel> EnrolledTeachers()
        {
            return this._adminPortalRepository.EnrolledTeachers();
        }
        #endregion

        #region Student
        #region GetListOfPassedOutStudents
        public List<StudentModel> PassedOutStudents(DropDownListModel Year)
        {
            return this._adminPortalRepository.PassedOutStudents(Year);
        }
        #endregion
        #endregion

        #region JosnResults
        #region CheckCourseNameAlreadyAvailableOrNot
        public bool CheckCourseName(string name)
        {
            return this._adminPortalRepository.CheckCourseName(name);
        }
        #endregion
        #endregion

        #region ListOfDeletedCourses
        public List<CourseEditModel> DeletedCourseList()
        {
            return this._adminPortalRepository.DeletedCourseList();
        }
        #endregion

        #region Library
        #region GetAllLibraryCards
        public List<LibraryRecordsEditModel> LibraryCardsList(DropDownListModel data)
        {
            return this._adminPortalRepository.LibraryCardsList(data);
        }
        #endregion
        #endregion

        #region Reports
        #region CountReportAmount
        public int TotalReports()
        {
            return this._adminPortalRepository.TotalReports();
        }
        #endregion 

        #region GetAllReports
        public List<ContactUsModel> Reports()
        {
            return this._adminPortalRepository.Reports();
        }
        #endregion
        #endregion

        #endregion

        #region Post
        #region UplaodEventImages
        public bool UploadEventImages(EventGalleryModel Images)
        {
            return this._adminPortalRepository.UploadEventImages(Images);
        }
        #endregion

        #region TeacherZone
        #region AddNewTeacher
        public bool NewTeacher(TeachersEditModel addTeacher)
        {
            return this._adminPortalRepository.NewTeacher(addTeacher);
        }
        #endregion

        #region AppointTeacher
        public bool AppointTeacher(DropDownListModel addCourseTeacher)
        {
            return this._adminPortalRepository.AppointTeacher(addCourseTeacher);
        }
        #endregion
        #endregion

        #region Course
        #region AddNewCourse
        public bool AddCourse(CourseEditModel Course)
        {
            return this._adminPortalRepository.AddCourse(Course);
        }
        #endregion
        #endregion

        #endregion

        #region Put
        #region UpdateCourseData
        public bool UpdateCourse(CourseEditModel EditCourse)
        {
            return this._adminPortalRepository.UpdateCourse(EditCourse);
        }
        #endregion

        #region EnrollDeletedCourse
        public bool EnrollDeletedCourse(string CID)
        {
            return this._adminPortalRepository.EnrollDeletedCourse(CID);
        }
        #endregion

        #region Student
        #region UPdateStudentData
        public List<UserStudenEdittModel> StudentsDetailsByFilter(DropDownListModel data)
        {
            return this._adminPortalRepository.StudentsDetailsByFilter(data);
        }
        #endregion
        #endregion

        #region Report
        #region UpdateReportReadStatus
        public bool ReadReport(string RID)
        {
            return this._adminPortalRepository.ReadReport(RID);
        }
        #endregion
        #endregion

        #region Teacher
        #region UpdateTeacherData
        public bool UpdateTeacherData(TeachersEditModel TeacherData)
        {
            return this._adminPortalRepository.UpdateTeacherData(TeacherData);
        }
        #endregion
        #endregion
        #endregion

        #region Delete
        #region Teacher
        #region RemoveTeacherFromCourses
        public bool RemoveCourseTeacher(DropDownListModel details)
        {
            return this._adminPortalRepository.RemoveCourseTeacher(details);
        }
        #endregion

        #region Resignteacher
        public bool Resignteacher(ResignteacherModel TeacherID)
        {
            return this._adminPortalRepository.Resignteacher(TeacherID);
        }
        #endregion 
        #endregion

        #region Course
        #region DeleteCourse
        public bool DeleteCourse(string CID)
        {
            return this._adminPortalRepository.DeleteCourse(CID);
        }
        #endregion
        #endregion

        #region DeleteNotice
        public bool DeleteNotice(string NID)
        {
            return this._adminPortalRepository.DeleteNotice(NID);
        }
        #endregion

        #region LibraryCard
        public bool DeleteLibraryCard(string LCID)
        {
            return this._adminPortalRepository.DeleteLibraryCard(LCID);
        }
        #endregion

        #region deleteCourseSemester
        public bool deleteCourseSemester(string CRSID, string STRID)
        {
            return this._adminPortalRepository.deleteCourseSemester(CRSID, STRID);
        }
        #endregion
        #endregion
    }
}
