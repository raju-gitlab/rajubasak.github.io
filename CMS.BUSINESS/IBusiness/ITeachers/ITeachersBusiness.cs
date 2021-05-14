using CMS.MODEL.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.BUSINESS.IBusiness.ITeachers
{
    public interface ITeachersBusiness
    {
        #region Get
        #region HomePageContents
        #region TeacherSection
        List<CourseTeacherModel> HomePageTeacherContents();
        #endregion
        #endregion

        #region GetAllTeachers
        Tuple<List<CourseTeacherModel>, List<TeachersEditModel>> ListTeachers();
        #endregion

        #region GetTeacherDetails
        Tuple<TeachersEditModel, List<ItemCode>, List<CourseEditModel>> Teacher_SingleDetails(string TeacherGuid);
        #endregion
        #endregion
    }
}
