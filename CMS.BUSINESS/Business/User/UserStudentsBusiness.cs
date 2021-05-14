using CMS.BUSINESS.IBusiness.IEvent;
using CMS.MODEL.User;
using CMS.REPOSITORY.IRepository.IUser;
using CMS.UTILITIES.LogManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.BUSINESS.Business.User
{
    public class UserStudentsBusiness : IUserStudentsBusiness
    {
        #region Parameters And Constructors
        private readonly IUserStudentsRepository _userStudentsRepository;
        public UserStudentsBusiness(IUserStudentsRepository userStudentsRepository)
        {
            this._userStudentsRepository = userStudentsRepository;
        }
        #endregion

        #region Get
        #region GetStudentDetailsByStudentId
        public UserStudenEdittModel GetSpecificStudentStudent(string StudentId)
        {
            try
            {
                if(StudentId != null || StudentId != string.Empty)
                {
                    return this._userStudentsRepository.GetSpecificStudentStudent(StudentId);
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

        #region GetAllStudents
        #region GetStudenstby Depeartment And Semester Wise
        public List<StudentModel> GetStudentsList(StudentModel students)
        {
            try
            {
                if (students != null)
                {
                    return this._userStudentsRepository.GetStudentsList(students);
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
        #endregion
        #endregion
    }
}
