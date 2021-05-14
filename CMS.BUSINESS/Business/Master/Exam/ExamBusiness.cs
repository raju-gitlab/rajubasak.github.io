using CMS.BUSINESS.IBusiness.IMaster.IExam;
using CMS.MODEL.Master;
using CMS.REPOSITORY.IRepository.IMaster.IExam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.BUSINESS.Business.Master.Exam
{
    public class ExamBusiness : IExamBusiness
    {
        #region Parameter and constructor
        private readonly IExamRepository _iExamRepository;
        public ExamBusiness(IExamRepository iExamRepository)
        {
            this._iExamRepository = iExamRepository;
        }
        #endregion

        #region Get
        #region ExamPaper
        #region GetExamPaperByExamAnd Year Id
        public List<ExamPaperModel> GetExamPaper(DropDownListModel data)
        {
            return this._iExamRepository.GetExamPaper(data);
        }
        #endregion
        #endregion
        #endregion

        #region Post
        #region Exampaper
        #region UploadPaper
        public bool UploadPreviousYearQuestionpaper(ExamPaperModel Paper)
        {
            return this._iExamRepository.UploadPreviousYearQuestionpaper(Paper);
        }
        #endregion
        #endregion
        #endregion
    }
}
