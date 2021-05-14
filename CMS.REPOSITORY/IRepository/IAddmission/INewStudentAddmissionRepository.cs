using CMS.MODEL.Addmission;
using CMS.MODEL.Master;
using CMS.MODEL.User;
using CMS.UTILITIES.CreateExcelFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.REPOSITORY.IRepository.IAddmission
{
    public interface INewStudentAddmissionRepository
    {
        #region Get
        #region ListItemsForDropDownLists
        #region CountryList
        List<DropDownListModel> CountryList(); 
        #endregion

        #region StreamList
        List<DropDownListModel> StreamList(); 
        #endregion

        #region GenderList
        List<DropDownListModel> GenderList(); 
        #endregion

        #region Statelist
        List<DropDownListModel> StateList(); 
        #endregion

        #region Semesterlist
        List<DropDownListModel> SemesterList(); 
        #endregion

        #region CountryList
        List<DropDownListModel> CityList();
        #endregion

        #region Parameterized Dropdown Lists
        #region StatelistByCountryId
        List<DropDownListModel> StateListById(string StateGuid);
        #endregion

        #region StatelistByCountryId
        List<DropDownListModel> CityListById(string CityGuid);
        #endregion

        #region SemesterlistByCourseId
        List<DropDownListModel> SemesterListById(string CourseGuid);
        #endregion
        #endregion

        #region FetchGuidCodeDetails
        GetGuidDetailsModel GuidDetails(string Data1, string Data2);
        #endregion
        #endregion

        #region SearchStudentbyStudentName,Stream,Semester
        List<StudentCardModel> SearchStudent(StudentFessEditModel studentFess);
        #endregion

        #region GetNewStudentsAddmissionListByCurrentTime(ByCurrentYear)
        List<StudentModel> NewStudentsAddmissionList();
        #endregion

        #region GetNewStudentsAddmissionListByCurrentTime(ByCurrentYear)
        List<StudentModel> NewStudentsAddmissionListByFilterParameter(StudentModel student);
        #endregion
        #endregion

        #region Post
        #region NewStudentAddmission
        bool StudentAddmission(AddmissionEditModel student);
        #endregion
        #endregion
    }
}
