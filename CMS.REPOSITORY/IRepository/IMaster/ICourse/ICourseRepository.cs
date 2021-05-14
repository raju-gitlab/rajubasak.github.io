using CMS.MODEL.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.REPOSITORY.IRepository.IMaster.ICourse
{
    public interface ICourseRepository
    {
        #region Get
        #region HomePageContent
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
