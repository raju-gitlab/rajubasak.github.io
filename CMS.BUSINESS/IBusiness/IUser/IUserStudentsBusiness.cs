using CMS.MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.BUSINESS.IBusiness.IEvent
{
    public interface IUserStudentsBusiness
    {
        #region Get
        #region GetStudentDetailsByStudentId
        UserStudenEdittModel GetSpecificStudentStudent(string StudentId);
        #endregion

        #region GetAllStudents
        #region GetStudenstby Depeartment And Semester Wise
        List<StudentModel> GetStudentsList(StudentModel students);
        #endregion
        #endregion
        #endregion
    }
}
