using CMS.MODEL.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.REPOSITORY.IRepository.IMaster.IExam
{
    public interface IExamRepository
    {
        #region Get
        #region ExamPaper
        #region GetExamPaperByExamAnd Year Id
        List<ExamPaperModel> GetExamPaper(DropDownListModel data);
        #endregion
        #endregion
        #endregion

        #region Post
        #region Exampaper
        #region UploadPaper
        bool UploadPreviousYearQuestionpaper(ExamPaperModel Paper);
        #endregion
        #endregion
        #endregion
    }
}
