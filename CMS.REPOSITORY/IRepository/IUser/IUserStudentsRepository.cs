using CMS.MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.REPOSITORY.IRepository.IUser
{
    public interface IUserStudentsRepository
    {
        #region Get
        #region GetStudentDetailsByStudentId
        UserStudenEdittModel GetSpecificStudentStudent(string StudentId);
        #endregion

        #region GetAllStudents
        #region GetStudensby Depeartment And Semester Wise
        List<StudentModel> GetStudentsList(StudentModel students); 
        #endregion
        #endregion
        #endregion
    }
}
