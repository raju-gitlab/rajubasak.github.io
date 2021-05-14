using CMS.BUSINESS.IBusiness.IMaster.ICourse;
using CMS.MODEL.Master;
using CMS.REPOSITORY.IRepository.IMaster.ICourse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.BUSINESS.Business.Master.Course
{
    public class CourseBusiness : ICourseBusiness
    {
        #region Constructor And Parameters
        private readonly ICourseRepository _courseRepository;
        public CourseBusiness(ICourseRepository courseRepository)
        {
            this._courseRepository = courseRepository;
        }
        #endregion

        #region Get
        #region HomePageContent
        #region CourseSection
        public List<CourseEditModel> HomePageCourseContents()
        {
            return this._courseRepository.HomePageCourseContents();
        } 
        #endregion
        #endregion

        #region GetListOffCourses
        public List<CourseEditModel> courses()
        {
            return this._courseRepository.courses();
        }
        #endregion

        #region GetListOffCoursesById
        public Tuple<List<CourseEditModel>, List<CourseTeacherModel>, List<CourseEditModel>> CourseDetails(string CourseId)
        {
            if(CourseId != null || CourseId != string.Empty)
            {
                return this._courseRepository.CourseDetails(CourseId);
            }
            else
            {
                return null;
            }
        }
        #endregion
        #endregion
    }
}
