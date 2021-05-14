using CMS.MODEL.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.BUSINESS.IBusiness.IMaster.ICourse
{
    public interface ICourseBusiness
    {
        #region Get
        #region HomePageContents
        #region CourseSection
        List<CourseEditModel> HomePageCourseContents();
        #endregion
        #endregion

        #region GetListOffCourses
        List<CourseEditModel> courses();
        #endregion

        #region GetListOffCoursesById
        Tuple<List<CourseEditModel>, List<CourseTeacherModel>, List<CourseEditModel>> CourseDetails(string CourseId);
        #endregion
        #endregion
    }
}
