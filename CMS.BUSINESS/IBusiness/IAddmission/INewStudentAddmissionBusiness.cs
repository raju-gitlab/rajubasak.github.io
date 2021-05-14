using CMS.MODEL.Addmission;
using CMS.MODEL.Master;
using CMS.MODEL.User;
using CMS.UTILITIES.CreateExcelFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.BUSINESS.IBusiness.IAddmission
{
    public interface INewStudentAddmissionBusiness
    {
        #region Get
        #region GetListItemsForDropDownLists
        #region GetCountryList
        List<DropDownListModel> CountryList();
        #endregion

        #region GetStreamList
        List<DropDownListModel> StreamList();
        #endregion

        #region GetGenderList
        List<DropDownListModel> GenderList();
        #endregion

        #region GetStateList
        List<DropDownListModel> StateList();
        #endregion

        #region SemesterListByCourse
        List<DropDownListModel> SemesterList();
        #endregion

        #region GetCitylist
        List<DropDownListModel> CityList();
        #endregion

        #region Parameterized Dropdown Lists
        #region StatelistByCountryId
        List<DropDownListModel> StateListById(string StateGuid);
        #endregion

        #region CitylistByStateId
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

        #region GetNewStudentsAddmissionListByCurrentTimeByParameters(ByCurrentYear)
        /// <summary>
        /// Get All the List of the Newly admitted Students By Stream and Semester
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
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
