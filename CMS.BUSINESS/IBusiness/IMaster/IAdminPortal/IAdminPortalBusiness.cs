using CMS.MODEL.Event;
using CMS.MODEL.Library;
using CMS.MODEL.Master;
using CMS.MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.BUSINESS.IBusiness.IMaster.IAdminPortal
{
    public interface IAdminPortalBusiness
    {
        #region Dropdowns
        List<DropDownListModel> dropdown();
        List<DropDownListModel> RoleList();
        List<DropDownListModel> TeacherList();
        List<DropDownListModel> SubjectList();
        List<DropDownListModel> SemeList(string Id);


        #endregion

        #region Login And LogOut
        TeachersEditModel AdminstrationLogin(ItemCode credentials);
        #endregion

        #region Get
        #region GetDetailsAboutCourse
        Tuple<CourseEditModel, List<CourseSemesterModel>> GetSpecificCourse(string CourseId);
        #endregion

        #region ListOfDeletedCourses
        List<CourseEditModel> DeletedCourseList();
        #endregion

        #region TeacherAllDetails
        Tuple<CourseTeacherModel, List<DropDownListModel>> TeacherAllDetails(string TeacherId);
        #endregion

        #region CollegtEnrolledTeachersList
        List<CourseTeacherModel> EnrolledTeachers();
        #endregion

        #region Student
        #region GetListOfPassedOutStudents
        List<StudentModel> PassedOutStudents(DropDownListModel Year);
        #endregion
        #endregion

        #region Json Results
        #region CheckCourseNameAlreadyAvailableOrNot
        bool CheckCourseName(string name);
        #endregion
        #endregion

        #region Library
        #region GetAllLibraryCards
        List<LibraryRecordsEditModel> LibraryCardsList(DropDownListModel data);
        #endregion
        #endregion

        #region Report
        #region CountReportAmount
        int TotalReports();
        #endregion

        #region GetAllReports
        List<ContactUsModel> Reports();
        #endregion
        #endregion

        #endregion

        #region Post
        #region UplaodEventImages
        bool UploadEventImages(EventGalleryModel Images);
        #endregion

        #region TeacherZone
        #region AddNewTeacher
        bool NewTeacher(TeachersEditModel addTeacher);
        #endregion

        #region AppointTeacher
        bool AppointTeacher(DropDownListModel addCourseTeacher);
        #endregion
        #endregion

        #region Course
        #region AddNewCourse
        bool AddCourse(CourseEditModel Course);
        #endregion
        #endregion
        #endregion

        #region Put
        #region UpdateCourseData
        bool UpdateCourse(CourseEditModel EditCourse);
        #endregion

        #region EnrollDeletedCourse
        bool EnrollDeletedCourse(string CID);
        #endregion

        #region Student
        #region UPdateStudentData
        List<UserStudenEdittModel> StudentsDetailsByFilter(DropDownListModel data);
        #endregion
        #endregion

        #region Report
        #region UpdateReportReadStatus
        bool ReadReport(string RID);
        #endregion
        #endregion

        #region Teacher
        #region UpdateTeacherData
        bool UpdateTeacherData(TeachersEditModel TeacherData);
        #endregion
        #endregion
        #endregion

        #region Delete
        #region Teacher
        #region RemoveTeacherFromCourses
        bool RemoveCourseTeacher(DropDownListModel details);
        #endregion

        #region Resignteacher
        bool Resignteacher(ResignteacherModel TeacherID);
        #endregion
        #endregion

        #region Course
        #region DeleteCourse
        bool DeleteCourse(string CID);
        #endregion
        #endregion

        #region DeleteNotice
        bool DeleteNotice(string NID);
        #endregion

        #region LibraryCard
        bool DeleteLibraryCard(string LCID);
        #endregion

        #region deleteCourseSemester
        bool deleteCourseSemester(string CRSID, string STRID);
        #endregion

        #endregion
    }
}
