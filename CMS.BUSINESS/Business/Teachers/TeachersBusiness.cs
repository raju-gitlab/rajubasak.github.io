using CMS.BUSINESS.IBusiness.ITeachers;
using CMS.MODEL.Master;
using CMS.REPOSITORY.IRepository.ITeachers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.BUSINESS.Business.Teachers
{
    public class TeachersBusiness : ITeachersBusiness
    {
        #region Parameters And Constructors
        private readonly ITeachersRepository _teachersRepository;
        public TeachersBusiness(ITeachersRepository teachersRepository)
        {
            this._teachersRepository = teachersRepository;
        }
        #endregion

        #region Get
        #region HomePageContents
        #region TeacherSection
        public List<CourseTeacherModel> HomePageTeacherContents()
        {
            return this._teachersRepository.HomePageTeacherContents();
        }
        #endregion
        #endregion

        #region GetAllTeachers
        public Tuple<List<CourseTeacherModel>, List<TeachersEditModel>> ListTeachers()
        {
            return this._teachersRepository.ListTeachers();
        }
        #endregion

        #region GetTeacherDetails
        public Tuple<TeachersEditModel, List<ItemCode>, List<CourseEditModel>> Teacher_SingleDetails(string TeacherGuid)
        {
            if(TeacherGuid != null || TeacherGuid != string.Empty)
            {
                return this._teachersRepository.Teacher_SingleDetails(TeacherGuid);
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
