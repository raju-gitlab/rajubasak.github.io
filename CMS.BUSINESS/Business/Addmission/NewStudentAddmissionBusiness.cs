using CMS.BUSINESS.IBusiness.IAddmission;
using CMS.MODEL.Addmission;
using CMS.MODEL.Master;
using CMS.MODEL.User;
using CMS.REPOSITORY.IRepository.IAddmission;
using CMS.UTILITIES.CreateExcelFile;
using CMS.UTILITIES.LogManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.BUSINESS.Business.Addmission
{
    public class NewStudentAddmissionBusiness : INewStudentAddmissionBusiness
    {
        #region Parameters And Constructor
        private readonly INewStudentAddmissionRepository _newStudentAddmissionRepository;
        private readonly IStudentFessRepository _studentFessRepository;
        public NewStudentAddmissionBusiness(INewStudentAddmissionRepository newStudentAddmissionRepository, IStudentFessRepository studentFessRepository)
        {
            this._newStudentAddmissionRepository = newStudentAddmissionRepository;
            this._studentFessRepository = studentFessRepository;
        }
        #endregion

        #region Get
        #region ListItemsForDropDownLists
        public List<DropDownListModel> CountryList()
        {
            return this._newStudentAddmissionRepository.CountryList();
        }
        public List<DropDownListModel> StreamList()
        {
            return this._newStudentAddmissionRepository.StreamList();
        }
        public List<DropDownListModel> GenderList()
        {
            return this._newStudentAddmissionRepository.GenderList();
        }
        public List<DropDownListModel> StateList()
        {
            return this._newStudentAddmissionRepository.StateList();
        }
        public List<DropDownListModel> SemesterList()
        {
            return this._newStudentAddmissionRepository.SemesterList();
        }
        public List<DropDownListModel> CityList()
        {
            return this._newStudentAddmissionRepository.CityList();
        }

        #region Parameterized Dropdown Lists
        #region StatelistByCountryId
        public List<DropDownListModel> StateListById(string StateGuid)
        {
            return this._newStudentAddmissionRepository.StateListById(StateGuid);
        }
        #endregion

        #region StatelistByCountryId
        public List<DropDownListModel> CityListById(string CityGuid)
        {
            return this._newStudentAddmissionRepository.CityListById(CityGuid);
        }
        #endregion

        #region SemesterlistByCourseId
        public List<DropDownListModel> SemesterListById(string CourseGuid)
        {
            return this._newStudentAddmissionRepository.SemesterListById(CourseGuid);
        }
        #endregion
        #endregion
        #endregion

        #region SearchStudentbyStudentName,Stream,Semester
        public List<StudentCardModel> SearchStudent(StudentFessEditModel studentFess)
        {
            try
            {
                if (studentFess != null)
                {
                    return this._newStudentAddmissionRepository.SearchStudent(studentFess);
                }
                else
                {
                    return null;
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
        public List<StudentModel> NewStudentsAddmissionList()
        {
            try
            {
                return this._newStudentAddmissionRepository.NewStudentsAddmissionList();
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion

        #region GetNewStudentsAddmissionListByCurrentTime(ByCurrentYear)
        /// <summary>
        /// Get All the List of the Newly admitted Students By Stream and Semester
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public List<StudentModel> NewStudentsAddmissionListByFilterParameter(StudentModel student)
        {
            try
            {
                return this._newStudentAddmissionRepository.NewStudentsAddmissionListByFilterParameter(student);
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion

        #region FetchGuidCodeDetails
        public GetGuidDetailsModel GuidDetails(string Data1, string Data2)
        {
            return this._newStudentAddmissionRepository.GuidDetails(Data1 , Data2);
        }
        #endregion
        #endregion

        #region Post
        #region NewStudentAddmission
        public bool StudentAddmission(AddmissionEditModel student)
        {
            try
            {
                if(student != null)
                {
                    return this._newStudentAddmissionRepository.StudentAddmission(student);
                }
                else
                {
                    return false;
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

       /* #region Put
        #region UpdateStudentFess
        public bool UpdateStudentFess(StudentFessEditModel studentFess)
        {
            try
            {
                if(this._newStudentAddmissionRepository.SearchStudent(studentFess))
                {
                     return this._studentFessRepository.GetStudentFeesDetails(studentFess.StudentGuid);
                }
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion
        #endregion*/
    }
}
